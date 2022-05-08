﻿using System;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Presupuesto
{
    public class CuentaPresupuesto
    {
        [Key]
        public int ID { get; set; }
        public int CuentaID { get; set; }
        public Cuenta Cuenta { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public Decimal Presupuesto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
