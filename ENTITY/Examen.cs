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

        public int ID { get; set; }
        public string Nombre_examen { get; set; }
        public string resultado { get; set; }
        public int Id_consulta { get; set; }
    }
}
