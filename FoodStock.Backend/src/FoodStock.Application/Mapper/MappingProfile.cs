using AutoMapper;
using FoodStock.Application.Functions.AuthFunctions;
using FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;
using FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetInactiveUserList;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetUsersList;
using FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;
using FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;
using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;
using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;
using FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;
using FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;
using FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderDetail;
using FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderList;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItem;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;
using FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemDetail;
using FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemList;
using FoodStock.Application.Functions.OrganizationFunctions.Commands.CreateOrganization;
using FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationDetail;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationList;
using FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;
using FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;
using FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentDetail;
using FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentListQuery;
using FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;
using FoodStock.Application.Functions.ProductFunctions.Commands.UpdateProduct;
using FoodStock.Application.Functions.ProductFunctions.Queries;
using FoodStock.Application.Functions.ProductFunctions.Queries.GetProductDetail;
using FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;
using FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;
using FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;
using FoodStock.Application.Functions.RoleFunctions.Queries.GetRoleDetail;
using FoodStock.Application.Functions.RoleFunctions.Queries.GetRolesList;
using FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;
using FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;
using FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierDetail;
using FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierList;
using FoodStock.Core.Entities;

namespace FoodStock.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Product Mapper
        CreateMap<Product, ProductListViewModel>()
            .ForMember(dest => dest.CategoryName, 
                opt=> opt.MapFrom(src => src.Category.CategoryName));
        CreateMap<Product, ProductListByCategoryIdViewModel>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName));
        CreateMap<Product, ProductDetailViewModel>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName));
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();

        //Producent Mapper
        CreateMap<Producent, CreateProducentCommand>().ReverseMap();
        CreateMap<Producent, UpdateProducentCommand>().ReverseMap();
        CreateMap<Producent, ProducentListViewModel>();
        CreateMap<Producent, ProducentDetailViewModel>();
        
        //Supplier Mapper
        CreateMap<Supplier, UpdateSupplierCommand>().ReverseMap();
        CreateMap<Supplier, CreateSupplierCommand>().ReverseMap();
        CreateMap<Supplier, SupplierDetailViewModel>();
        CreateMap<Supplier, SupplierListViewModel>();
        
        //Category Mapper
        CreateMap<Category, CategoryListViewModel>();
        CreateMap<Category, CategoryDetailViewModel>();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        
        //Role Mapper
        CreateMap<Role, RoleListViewModel>();
        CreateMap<Role, RoleDetailViewModel>();
        CreateMap<Role, CreateRoleCommand>().ReverseMap();
        CreateMap<Role, UpdateRoleCommand>().ReverseMap();
        
        //Auth Mapper
        CreateMap<User, RegisterUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatePasswordCommand>().ReverseMap();
        CreateMap<User, UserDetailViewModel>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<User, UserDetailViewModelByEmail>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<User, UserListViewModel>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<User, InactiveUserListViewModel>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name));
        
        //Organization Mapper 
        CreateMap<Organization, GetOrganizationDetailViewModel>();
        CreateMap<Organization, GetOrganizationListViewModel>();
        CreateMap<Organization, CreateOrganizationCommand>().ReverseMap();
        CreateMap<Organization, UpdateOrganizationCommand>().ReverseMap();
        
        //Order Mapper
        CreateMap<Order, GetOrderListViewModel>();
        CreateMap<Order, OrderDetailViewModel>();
        CreateMap<Order, CreateOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        
        //OrderItem Mapper
        CreateMap<OrderItem, OrderItemListViewModel>()
            .ForMember(dest => dest.CategoryName,
            opt => opt.MapFrom(
                src => src.Category.CategoryName))
            .ForMember(dest => dest.OrderName,
                opt => opt.MapFrom(
                    src => src.Order.OrderName));
        CreateMap<OrderItem, GetOrderItemViewModel>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(
                    src => src.Category.CategoryName));
        CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();
        CreateMap<OrderItem, CreateOrderItemByProductCommand>().ReverseMap();
        CreateMap<OrderItem, UpdateOrderItemCommand>().ReverseMap();
    }
}
