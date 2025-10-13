using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Responsable
    {
        public Responsable() { }

        public string DocumentoID { get; set; } 
        public string Nombre { get; set; }
        public string Parentesco { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion {  get; set; }
        public string Ocupacion { get; set; }
    }
}
