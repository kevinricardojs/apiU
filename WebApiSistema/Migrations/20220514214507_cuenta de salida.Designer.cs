﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiSistema.Data;

namespace WebApiSistema.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220514214507_cuenta de salida")]
    partial class cuentadesalida
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApiSistema.Models.Compra.Compra", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FacturaFecha")
                        .HasColumnType("datetime");

                    b.Property<string>("FacturaSerie")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("SocioNegocioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SocioNegocioID");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("WebApiSistema.Models.Compra.CompraDetalle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CompraID")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<int>("NoLinea")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompraID");

                    b.HasIndex("ProductoID");

                    b.ToTable("CompraDetalle");
                });

            modelBuilder.Entity("WebApiSistema.Models.Configuraciones.Empresa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nit")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("WebApiSistema.Models.Configuraciones.SocioNegocio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("text");

                    b.Property<string>("Nit")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("SocioNegocio");
                });

            modelBuilder.Entity("WebApiSistema.Models.Configuraciones.Sucursal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Direccion")
                        .HasColumnType("text");

                    b.Property<int>("EmpresaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmpresaID");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Presupuesto.Cuenta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CodigoCuenta")
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("WebApiSistema.Models.Presupuesto.CuentaPresupuesto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Anio")
                        .HasColumnType("int");

                    b.Property<int>("CuentaID")
                        .HasColumnType("int");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.Property<decimal>("Presupuesto")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SucursalID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CuentaID");

                    b.HasIndex("SucursalID");

                    b.ToTable("CuentaPresupuesto");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.FamiliaProducto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CuentaID")
                        .HasColumnType("int");

                    b.Property<int>("CuentaIDO")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CuentaID");

                    b.ToTable("FamiliaProducto");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.Producto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<int>("FamiliaProductoID")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductoTipoID")
                        .HasColumnType("int");

                    b.Property<int>("SucursalID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FamiliaProductoID");

                    b.HasIndex("ProductoTipoID");

                    b.HasIndex("SucursalID");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.ProductoTipo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<int>("SucursalID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SucursalID");

                    b.ToTable("ProductoTipo");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionContable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompraVentaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TransaccionContable");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionDetalleContable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CuentaID")
                        .HasColumnType("int");

                    b.Property<decimal>("Debe")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Haber")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Linea")
                        .HasColumnType("int");

                    b.Property<int>("SucursalID")
                        .HasColumnType("int");

                    b.Property<int?>("TransaccionContableID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CuentaID");

                    b.HasIndex("SucursalID");

                    b.HasIndex("TransaccionContableID");

                    b.ToTable("TransaccionDetalleContable");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionDetalleInventario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Entrada")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("Linea")
                        .HasColumnType("int");

                    b.Property<int>("ProductoID")
                        .HasColumnType("int");

                    b.Property<decimal>("Salida")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SucursalID")
                        .HasColumnType("int");

                    b.Property<int?>("TransaccionInventarioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductoID");

                    b.HasIndex("SucursalID");

                    b.HasIndex("TransaccionInventarioID");

                    b.ToTable("TransaccionDetalleInventario");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionInventario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompraVentaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TransaccionInventario");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.RoleMenu", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RoleMenu");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellidos")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Empresa")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp");

                    b.Property<string>("Nombres")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Sucursal")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Venta.Venta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FacturaFecha")
                        .HasColumnType("datetime");

                    b.Property<string>("FacturaSerie")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("SocioNegocioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SocioNegocioID");

                    b.ToTable("Venta");
                });

            modelBuilder.Entity("WebApiSistema.Models.Venta.VentaDetalle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CompraID")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<int>("NoLinea")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompraID");

                    b.HasIndex("ProductoID");

                    b.ToTable("VentaDetalle");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiSistema.Models.Compra.Compra", b =>
                {
                    b.HasOne("WebApiSistema.Models.Configuraciones.SocioNegocio", "SocioNegocio")
                        .WithMany()
                        .HasForeignKey("SocioNegocioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocioNegocio");
                });

            modelBuilder.Entity("WebApiSistema.Models.Compra.CompraDetalle", b =>
                {
                    b.HasOne("WebApiSistema.Models.Compra.Compra", "Compra")
                        .WithMany("Detalles")
                        .HasForeignKey("CompraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Productos.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("WebApiSistema.Models.Configuraciones.Sucursal", b =>
                {
                    b.HasOne("WebApiSistema.Models.Configuraciones.Empresa", "Empresa")
                        .WithMany("Sucursales")
                        .HasForeignKey("EmpresaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("WebApiSistema.Models.Presupuesto.CuentaPresupuesto", b =>
                {
                    b.HasOne("WebApiSistema.Models.Presupuesto.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Configuraciones.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuenta");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.FamiliaProducto", b =>
                {
                    b.HasOne("WebApiSistema.Models.Presupuesto.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuenta");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.Producto", b =>
                {
                    b.HasOne("WebApiSistema.Models.Productos.FamiliaProducto", "FamiliaProducto")
                        .WithMany()
                        .HasForeignKey("FamiliaProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Productos.ProductoTipo", "ProductoTipo")
                        .WithMany()
                        .HasForeignKey("ProductoTipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Configuraciones.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FamiliaProducto");

                    b.Navigation("ProductoTipo");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Productos.ProductoTipo", b =>
                {
                    b.HasOne("WebApiSistema.Models.Configuraciones.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionDetalleContable", b =>
                {
                    b.HasOne("WebApiSistema.Models.Presupuesto.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Configuraciones.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Transacciones.TransaccionContable", null)
                        .WithMany("Detalles")
                        .HasForeignKey("TransaccionContableID");

                    b.Navigation("Cuenta");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionDetalleInventario", b =>
                {
                    b.HasOne("WebApiSistema.Models.Productos.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Configuraciones.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Transacciones.TransaccionInventario", null)
                        .WithMany("Detalles")
                        .HasForeignKey("TransaccionInventarioID");

                    b.Navigation("Producto");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.RoleMenu", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.Menu", "Menu")
                        .WithMany("RoleMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Usuario.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.UserRole", b =>
                {
                    b.HasOne("WebApiSistema.Models.Usuario.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Usuario.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApiSistema.Models.Venta.Venta", b =>
                {
                    b.HasOne("WebApiSistema.Models.Configuraciones.SocioNegocio", "SocioNegocio")
                        .WithMany()
                        .HasForeignKey("SocioNegocioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocioNegocio");
                });

            modelBuilder.Entity("WebApiSistema.Models.Venta.VentaDetalle", b =>
                {
                    b.HasOne("WebApiSistema.Models.Venta.Venta", "Compra")
                        .WithMany("Detalles")
                        .HasForeignKey("CompraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiSistema.Models.Productos.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("WebApiSistema.Models.Compra.Compra", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Configuraciones.Empresa", b =>
                {
                    b.Navigation("Sucursales");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionContable", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Transacciones.TransaccionInventario", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.Menu", b =>
                {
                    b.Navigation("RoleMenus");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.Role", b =>
                {
                    b.Navigation("RoleMenus");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Usuario.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebApiSistema.Models.Venta.Venta", b =>
                {
                    b.Navigation("Detalles");
                });
#pragma warning restore 612, 618
        }
    }
}
