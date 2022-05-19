using AutoMapper;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.ListaMateriales;
using WebApiSistema.DTO.Produccion;
using WebApiSistema.DTO.Transaccion;
using WebApiSistema.DTO.User;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Compra;
using WebApiSistema.Models.Produccion;
using WebApiSistema.Models.Transacciones;
using WebApiSistema.Models.Usuario;
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

            // Devolver Materiales response
            CreateMap<ListaMateriales, ListaMaterialesResponse>();
            CreateMap<Materiales, ListaMaterialesDetailResponse>();

            // Devolver Produccion
            CreateMap<ProduccionCreate, Produccion>();
            CreateMap<ProduccionDetalleCreate, ProduccionDetalles>();

            CreateMap<Produccion, ProduccionCreateResponse>();
            CreateMap<ProduccionDetalles, ProduccionDetalleResponse>();

            // Transaccion contable
            CreateMap<TransaccionContableCreate, TransaccionContable>();
            CreateMap<TransaccionDetalleContableCreate, TransaccionDetalleContable>();

            CreateMap<TransaccionContable, TransaccionContableResponse>();
            CreateMap<TransaccionDetalleContable, TransaccionDetalleContableResponse>();

            // Usuario
            CreateMap<UserCreate, User>();
            CreateMap<User, UserCreateReponse>();

        }
    }
}
