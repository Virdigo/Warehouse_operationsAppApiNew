using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDoljnostiRepository _doljnostiRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult GetUsersList()
        {
            var UsersList = _mapper.Map<List<UsersDto>>(_usersRepository.GetUsersList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(UsersList);
        }

        [HttpGet("{UsersId}")]
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]

        public IActionResult GetUsersById(int UsersId)
        {
            if (!_usersRepository.UsersExists(UsersId))
                return NotFound();

            var UsersById = _mapper.Map<UsersDto>(_usersRepository.GetUsersById(UsersId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(UsersById);
        }

        [HttpGet("Doljnosti/{Users}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]

        public IActionResult GetDoljnostiByUsers(int id_doljnosti)
        {
            var doljnostiByUser = _mapper.Map<List<UsersDto>>(_usersRepository.GetDoljnostiByUsers(id_doljnosti));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(doljnostiByUser);
        }

        [HttpPost("POST")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUsers([FromQuery] int id_doljnosti, [FromBody] UsersDto Users_create)
        {
            if (Users_create == null)
                return BadRequest(ModelState);

            var Use = _usersRepository.GetUsersList()
                .Where(c => c.FIO.Trim().ToUpper() == Users_create.FIO.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Use != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UsersMap = _mapper.Map<Users>(Users_create);

            if (!_usersRepository.CreateUsers(id_doljnosti, UsersMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("PUT/{id_users}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsers(int id_users, [FromQuery] int id_doljnosti,
            [FromBody] UsersDto Users_update)
        {
            if (Users_update == null)
                return BadRequest(ModelState);

            if (id_users != Users_update.id_users)
                return BadRequest(ModelState);

            if (!_usersRepository.UsersExists(id_users))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var Users_aboutMap = _mapper.Map<Users>(Users_update);

            if (!_usersRepository.UpdateUsers(id_doljnosti, Users_aboutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating User");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DELETE/{id_users}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUsers(int id_users)
        {
            if (!_usersRepository.UsersExists(id_users))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var DeleteUsersById = _usersRepository.GetUsersById(id_users);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_usersRepository.DeleteUsers(DeleteUsersById))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
        [HttpPost("authenticate")]
        [ProducesResponseType(200, Type = typeof(UsersDto))]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest(ModelState);

            var user = _usersRepository.GetUsersList()
                .FirstOrDefault(u => u.Login == loginDto.Login && u.password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Неправильный login or password" });
            }

            var userDto = _mapper.Map<UsersDto>(user);
            return Ok(new
            {
                userDto.id_users,
                userDto.FIO,
                userDto.Login,
                userDto.id_doljnosti
            });
        }
    }
}