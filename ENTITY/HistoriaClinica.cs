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

        public int Id { get; set; }
        public int Documento_paciente { get; set; }
        public int Documento_doctor { get; set; }
        public int Cita_id { get; set; }
        public DateTime Fecha_apertura { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
    }
}
