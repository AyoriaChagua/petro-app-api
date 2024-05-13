using ApiTestIIS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Contexts
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        // clave compuesta
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Margen>()
                .HasKey(c => new { c.Desc_almacen, c.Desc_lista_precio, c.Desc_tipo_descuento });

            modelBuilder.Entity<Solicitud_dscto_det_sel>()
                .HasKey(c => new { c.Id_solicitud_dscto, c.Id_solicitud_dscto_det });

            modelBuilder.Entity<Docs_Cliente_Mes>()
                .HasKey(c => new { c.Documento, c.Articulo});

            modelBuilder.Entity<Docs_Asesor>()
                .HasKey(c => new { c.Documento, c.Fecha });

            modelBuilder.Entity<Documento>()
                .HasKey(c => new { c.Cia, c.Id_planta, c.Id_tipo_doc, c.Serie, c.Nro_documento });

            modelBuilder.Entity<Punto_venta_asesor>()
                .HasKey(c => new { c.Cia, c.Id_cliente, c.Id_punto_venta, c.Id_asesor });
        }
        public DbSet<Margen> margen { get; set; }
        public DbSet<Solicitud_dscto_det_sel> solicitud_dscto_det_sel { get; set; }
        public DbSet<Docs_Cliente_Mes> docs_Cliente_Mess { get; set; }
        public DbSet<Docs_Asesor> docs_asesors { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Punto_venta_asesor> Punto_Venta_Asesor { get; set; }


        public DbSet<UsuarioInfo> UsuarioInfo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Planta> Planta { get; set; }
        public DbSet<CondiPago> CondiPago { get; set; }
        public DbSet<ArticuloSubclase> ArticuloSubclase { get; set; }
        public DbSet<Articulo> articulo { get; set; }
        public DbSet<Usuario_aprueba_dscto> usuario_aprueba_dscto { get; set; }
        public DbSet<Almacen> almacen { get; set; }
        public DbSet<Solicitud_dscto_sel> solicitud_Dscto_Sel { get; set; }
        public DbSet<Descuento> descuento { get; set; }
        public DbSet<EstadoVentasCliente> estadoVentasClientes { get; set; }
        public DbSet<VentasAlCliente> ventasAlClientes { get; set; }
        public DbSet<Docs_con_Saldo> docs_Con_Saldos { get; set; }
        public DbSet<TopClientes> topClientess { get; set; }
        public DbSet<VencimientoCubicacion> vencimientoCubicacions { get; set; }
        public DbSet<Punto_Venta> Punto_Venta { get; set; }
        public DbSet<Docs_con_Saldo_Credito> docs_con_Saldo_Creditos { get; set; }
        public DbSet<Info_Saldos_Cliente> info_Saldos_Clientes { get; set; }
        public DbSet<Asesor> Asesor { get; set; }
        public DbSet<TopAsesor> topAsesors { get; set; }
        public DbSet<Ventas_Det_Resumen> ventas_Det_Resumens { get; set; }
        public DbSet<Ventas_Resumen> ventas_Resumens { get; set; }
        public DbSet<Ingresos_Det_XVentas> ingresos_Det_XVentass { get; set; }
        public DbSet<Lista_Precio> lista_Precios  { get; set; }
        public DbSet<Simulacion_Venta> simulacion_ventas { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}
