using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Cliente
    {
        public int ClieId { get; set; }
        public int? PersId { get; set; }
        public string? ClieContrasenia { get; set; }
        public bool? ClieEstado { get; set; }
    }

    public partial class PersonaCliente
    {
        public int ClieId { get; set; }
        public int? PersId { get; set; }
        public string? ClieContrasenia { get; set; }
        public bool? ClieEstado { get; set; }
        public string? PersNombre { get; set; }
        public string? PersGenero { get; set; }
        public int? PersEdad { get; set; }
        public string? PersIdentificacion { get; set; }
        public string? PersDireccion { get; set; }
        public string? PersTelefono { get; set; }
    }
}
