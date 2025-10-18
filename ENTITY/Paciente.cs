using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Paciente
    {
        public Paciente() { }

        public string DocumentoID { get; set; }
        public string Primer_Nombre { get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string Calle {  get; set; }
        public int Ciudad_id { get; set; }
        public string Telefono { get; set; }
        public int EPS_id { get; set; }
        public string Tipo_sangre { get; set; }
        public char RH { get; set; }
        public string Documento_responsable { get; set; }
        public int Usuario_id { get; set; }
    }
}
