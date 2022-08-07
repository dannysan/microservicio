using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Persona
    {
        public int PersId { get; set; }
        public string? PersNombre { get; set; }
        public string? PersGenero { get; set; }
        public int? PersEdad { get; set; }
        public string? PersIdentificacion { get; set; }
        public string? PersDireccion { get; set; }
        public string? PersTelefono { get; set; }
    }
}
