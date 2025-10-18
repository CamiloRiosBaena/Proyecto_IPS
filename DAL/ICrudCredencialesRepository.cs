using System;
using ENTITY;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICrudCredencialesRepository
    {
        bool VerificarCredenciales(string username, string password);
        bool InsertarUsuarioPaciente(Paciente paciente, string username, string password, string rol);
        bool InsertarUsuarioDoctor(Doctor doctor, string username, string password, string rol);
        bool InsertarUsuarioResponsable(Responsable responsable);
        bool ExisteUsuario(string username);
        string ObtenerRolUsuario(string username);
        int ObtenerSiguienteId();
    }
}
