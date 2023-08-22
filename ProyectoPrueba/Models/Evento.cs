using System;
using System.Collections.Generic;

namespace ProyectoPrueba.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Entrada = new HashSet<Entradum>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Ubicacion { get; set; }

        public virtual ICollection<Entradum> Entrada { get; set; }
    }
}
