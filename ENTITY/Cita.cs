using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Cita
    {
        public Cita() { }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
        public string Generalidad { get; set; }
        public int Documento_doctor { get; set; }
        public int Documento_paciente { get; set; }
    }
}
