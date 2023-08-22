using System;
using System.Collections.Generic;

namespace ProyectoPrueba.Models
{
    public partial class Entradum
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int CategoriaEntradaId { get; set; }
        public int UsuariosId { get; set; }

        public virtual CategoriaEntradum CategoriaEntrada { get; set; } = null!;
        public virtual Evento Evento { get; set; } = null!;
        public virtual Usuario Usuarios { get; set; } = null!;
    }
}
