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
        public DateTime Fecha_apertura { get; set; }
        public string Estado {  get; set; }
        public int Id_paciente { get; set; }
    }
}
