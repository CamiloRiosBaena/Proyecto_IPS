using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Credenciales
    {
        public Credenciales() { }

        public string Id { get; set; } 
        public string Nombre_usuario { get; set; }
        public string Password { get; set; }    
        public string Tipo_usuario {  get; set; }
        public string Estado {  get; set; }
    }
}
