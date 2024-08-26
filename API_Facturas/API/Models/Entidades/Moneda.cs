using System;
using System.Collections.Generic;

namespace API.Models.Entidades
{
    public partial class Moneda
    {
        public Moneda()
        {
            Encabezado = new HashSet<Encabezado>();
        }

        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; } = null!;

        public virtual ICollection<Encabezado> Encabezado { get; set; }
    }
}
