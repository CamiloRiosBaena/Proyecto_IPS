using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICrudUsuario
    {
        bool Login(string username, string password);
        bool RegistrarPaciente(Paciente paciente, string username, string password);
        bool RegistrarDoctor(Doctor doctor, string username, string password);
        bool RegistrarResponsable(Responsable responsable);
        string ObtenerRolUsuario(string username);
    }
}
