using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Cuenta
    {
        public int CuenId { get; set; }
        public int? ClieId { get; set; }
        public string? CuenNumero { get; set; }
        public string? CuenTipo { get; set; }
        public double? CuenSaldoInicial { get; set; }
        public bool? CuenEstado { get; set; }
    }
}
