using AutoMapper;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.ListaMateriales;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Compra;
using WebApiSistema.Models.ListaMateriales;
using WebApiSistema.Models.Venta;

namespace WebApiSistema.Mapper
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            // Ventas
            CreateMap<VentaCreate, Venta>();
            CreateMap<VentaCreateDetail, VentaDetalle>();

            // Devolver Venta
            CreateMap<Venta, VentaCreateResponse>();
            CreateMap<VentaDetalle, VentaCreateResponseDetail>();

            // Crear Compra
            CreateMap<CompraCreate, Compra>();
            CreateMap<CompraCreateDetail, CompraDetalle>();

            // Devolver Compra
            CreateMap<Compra, CompraCreateResponse>();
            CreateMap<CompraDetalle, CompraCreateResponseDetail>();

            // Lista Materiales
            CreateMap<ListaMaterialesCreate, ListaMateriales>();
            CreateMap<ListaMaterialesDetailCreate, Materiales>();

            // Devolver Venta
            CreateMap<ListaMateriales, ListaMaterialesResponse>();
            CreateMap<Materiales, ListaMaterialesDetailResponse>();

        }
    }
}
