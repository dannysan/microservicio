using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Movimiento
    {
        public int MoviId { get; set; }
        public int? CuenId { get; set; }
        public DateTime? MoviFecha { get; set; }
        public double? MoviValor { get; set; }
        public double? MoviSaldo { get; set; }
    }


    public partial class MovimientoCuenta    {

        public string NumeroCuenta { get; set; } = string.Empty;
        public DateTime? Fecha { get; set; }
        public double? Valor { get; set; }
        
    }

    public partial class Reporte
    {
        public DateTime? Fecha { get; set; }
        public string? Cliente { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? Tipo { get; set; }
        public double SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public double Movimiento { get; set; }
        public double SaldoDisponible { get; set; }

    }
}
