using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class HistoriaClinica
    {
        public HistoriaClinica() { }

        public int Historia_id { get; set; }
        public string Paciente_documentoid { get; set; }
        public string Doctor_documentoid { get; set; }
        public int Cita_id { get; set; }
        public int Especialidad_id { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
    }
}
