using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Models.Presupuesto;
using WebApiSistema.Models.Configuraciones;
using WebApiSistema.Models.Usuario;
using WebApiSistema.Models.Compra;
using WebApiSistema.Models.Productos;
using WebApiSistema.Models.Transacciones;
using WebApiSistema.Models.Venta;
using WebApiSistema.Models.Produccion;

namespace WebApiSistema.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        // Configuraciones
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<UsuarioSucursal> UsuarioSucursal { get; set; }

        // Compras
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraDetalle> CompraDetalle { get; set; }

        // Ventas
        public DbSet<Venta> Venta { get; set; }
        public DbSet<VentaDetalle> VentaDetalle { get; set; }

        // Presupuesto
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Presupuesto> Presupuesto { get; set; }
        public DbSet<CuentaPresupuesto> CuentaPresupuesto { get; set; }

        // Productos
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoTipo> ProductoTipo { get; set; }
        public DbSet<FamiliaProducto> FamiliaProducto { get; set; }

        // Transacciones
        public DbSet<TransaccionContable> TransaccionContable { get; set; }
        public DbSet<TransaccionDetalleContable> TransaccionDetalleContable { get; set; }
        public DbSet<TransaccionInventario> TransaccionInventario { get; set; }
        public DbSet<TransaccionDetalleInventario> TransaccionDetalleInventario { get; set; }

        // Lista Materiales
        public DbSet<ListaMateriales> ListaMateriales { get; set; }
        public DbSet<Materiales> Materiales { get; set; }

        // Produccion
        public DbSet<Produccion> Produccion { get; set; }
        public DbSet<ProduccionDetalles> ProduccionDetalles { get; set; }

        public DbSet<SocioNegocio> SocioNegocio { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();


                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<RoleMenu>(roleMenu =>
            {
                roleMenu.HasKey(rm => new { rm.RoleId, rm.MenuId });

                roleMenu.HasOne(rm => rm.Role)
                    .WithMany(r => r.RoleMenus)
                    .HasForeignKey(rm => rm.RoleId)
                    .IsRequired();

                roleMenu.HasOne(rm => rm.Menu)
                    .WithMany(m => m.RoleMenus)
                    .HasForeignKey(rm => rm.MenuId)
                    .IsRequired();
            });
        }
    }
}
