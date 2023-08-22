using System;
using System.Collections.Generic;

namespace ProyectoPrueba.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Entrada = new HashSet<Entradum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string Contraseña { get; set; } = null!;

        public virtual ICollection<Entradum> Entrada { get; set; }
    }
}
