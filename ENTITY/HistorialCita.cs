using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class HistorialCita
    {
        public int TotalCitas { get; set; }
        public int CitasCompletadas { get; set; }
        public int CitasPendientes { get; set; }
        public int CitasCanceladas { get; set; }
        public int CitasHoy { get; set; }
        public int CitasEstaSemana { get; set; }
        public int CitasEsteMes { get; set; }
    }
}
