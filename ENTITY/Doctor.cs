using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Doctor
    {
        public Doctor() { }

        public string DocumentoID { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public List<DateTime> HorariosAtencion { get; set; } = new List<DateTime>();
        public int Id_usuario { get; set; }
        
    }
}
