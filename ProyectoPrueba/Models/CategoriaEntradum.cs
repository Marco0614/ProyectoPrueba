using System;
using System.Collections.Generic;

namespace ProyectoPrueba.Models
{
    public partial class CategoriaEntradum
    {
        public CategoriaEntradum()
        {
            Entrada = new HashSet<Entradum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Precio { get; set; }
        public int Cantidad { get; set; }

        public virtual ICollection<Entradum> Entrada { get; set; }
    }
}
