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
        public string Nombre { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion {  get; set; }
        public string EPS { get; set; }
        public string Tipo_sangre { get; set; }
        public string Id_responsable { get; set; }
        public int Id_usuario { get; set; }
    }
}
