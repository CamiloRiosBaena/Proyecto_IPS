using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Examen
    {
        public Examen() { }

        public int Id { get; set; }
        public int Id_historia { get; set; }
        public string Nombre { get; set; }
        public string resultado { get; set; }
        public DateTime Fecha_solicitud { get; set; }
        public DateTime Fecha_resultado { get; set; }
        public string Estado { get; set; }
    }
}
