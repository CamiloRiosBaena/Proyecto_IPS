using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Consulta
    {
        public Consulta() { }

        public int Id { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public int Id_cita {  get; set; }
        public int Id_historiaClinica { get; set; }
    }
}
