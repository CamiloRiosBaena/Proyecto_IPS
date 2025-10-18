using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Admin
    {
        public Admin() { }

        public string DocumentoID { get; set; }
        public string Primer_Nombre { get; set;}
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public int Usuario_id { get; set; }
    }
}
