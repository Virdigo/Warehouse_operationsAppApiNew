using AutoMapper;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Dto.ApiDto;
using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doljnosti, DoljnostiDto>();
            CreateMap<Product_type, Product_typeDto>();
            CreateMap<Suppliers, SuppliersDto>();
            CreateMap<Receipt_and_expense_documents, Receipt_and_expense_documentsDto>();
            CreateMap<Information_about_documents, Information_about_documentsDto>();
            CreateMap<Unit, UnitDto>();
            CreateMap<Ostatki, OstatkiDto>();
            CreateMap<Product, ProductsDto>();
            CreateMap<ProductsDto, Product>();
            CreateMap<Users, UsersDto>();
            CreateMap<Warehouses, WarehousesDto>();
            CreateMap<DoljnostiDto, Doljnosti>();
            CreateMap<SuppliersDto, Suppliers>();
            CreateMap<Information_about_documents, Information_about_documentsApi>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.Receipt_and_expense_documentsName, opt => opt.MapFrom(src => src.Receipt_and_expense_documents.ReceiptAndexpense_documents))
               .ForMember(dest => dest.SuppliersName, opt => opt.MapFrom(src => src.Suppliers.Name));
            CreateMap<Ostatki, OstatkiApi>()
    .ForMember(dest => dest.WarehousesName, opt => opt.MapFrom(src => src.Warehouses.Name))
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<Product_typeDto, Product_type>();
            CreateMap<Product, ProductAPI>()
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.Product_type.Name))
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name));
            CreateMap<Receipt_and_expense_documents, Receipt_and_expense_documentsApi>()
    .ForMember(dest => dest.UsersName, opt => opt.MapFrom(src => src.Users.FIO));
            CreateMap<UsersDto, Users>();
            CreateMap<Warehouses, WarehousesApi>()
            .ForMember(dest => dest.UsersName, opt => opt.MapFrom(src => src.Users.FIO));
            CreateMap<Information_about_documentsDto, Information_about_documents>();
            CreateMap<ProductsDto, Product>();
            CreateMap<OstatkiDto, Ostatki>();
            CreateMap<WarehousesDto, Warehouses>();
        }
    }
}
