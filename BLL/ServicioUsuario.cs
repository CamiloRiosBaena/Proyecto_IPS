using DAL;
using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioUsuario : ICrudUsuario
    {
        private CredencialesRepository usuarioRepository;

        public ServicioUsuario()
        {
            usuarioRepository = new CredencialesRepository();
        }
        public bool Login(string username, string password)
        {
            try
            {
                ValidarDatosLogin(username, password);
                return usuarioRepository.VerificarCredenciales(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error BLL en login: {ex.Message}", ex);
            }
        }

        public bool RegistrarPaciente(Paciente paciente, string username, string password)
        {
            try
            {
                ValidarDatosBasicos(username, password);
                ValidarPaciente(paciente);

                return usuarioRepository.InsertarUsuarioPaciente(paciente, username, password, "Paciente");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error BLL registrando paciente: {ex.Message}", ex);
            }
        }

        public bool RegistrarDoctor(Doctor doctor, string username, string password)
        {
            try
            {
                ValidarDatosBasicos(username, password);
                ValidarDoctor(doctor);

                return usuarioRepository.InsertarUsuarioDoctor(doctor, username, password, "Doctor");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error BLL registrando doctor: {ex.Message}", ex);
            }
        }

        public bool RegistrarResponsable(Responsable responsable)
        {
            try
            {
                ValidarResponsable(responsable);

                return usuarioRepository.InsertarUsuarioResponsable(responsable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error BLL registrando responsable: {ex.Message}", ex);
            }
        }
        private void ValidarDatosLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Usuario y contraseña son requeridos");
            }
        }
        private void ValidarDatosBasicos(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Usuario y contraseña son requeridos");
            }

            if (username.Length < 3)
            { 
                throw new ArgumentException("El usuario debe tener al menos 3 caracteres");
            }

            if (password.Length < 4)
            {
                throw new ArgumentException("La contraseña debe tener al menos 4 caracteres");
            }   

            if (usuarioRepository.ExisteUsuario(username))
            {
                throw new InvalidOperationException("El usuario ya existe en el sistema");
            }
        }

        private void ValidarPaciente(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentException("Datos del paciente son requeridos");
            }

            if (string.IsNullOrWhiteSpace(paciente.DocumentoID))
            {
                throw new ArgumentException("Documento del paciente es requerido");
            }

            if (string.IsNullOrWhiteSpace(paciente.Primer_Nombre))
            {
                throw new ArgumentException("Nombre del paciente es requerido");
            }

            if (string.IsNullOrWhiteSpace(paciente.Primer_Apellido))
            {
                throw new ArgumentException("Nombre del paciente es requerido");
            }

            if (paciente.Edad < 0)
            {
                throw new ArgumentException("Edad no válida");
            }
        }

        private void ValidarDoctor(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentException("Datos del doctor son requeridos");
            }

            if (string.IsNullOrWhiteSpace(doctor.DocumentoID))
            {
                throw new ArgumentException("Documento del doctor es requerido");
            }

            if (string.IsNullOrWhiteSpace(doctor.Primer_Nombre))
            {
                throw new ArgumentException("Nombre del doctor es requerido");
            }

            if (string.IsNullOrWhiteSpace(doctor.Primer_Apellido))
            {
                throw new ArgumentException("Nombre del doctor es requerido");
            }

            if (string.IsNullOrWhiteSpace(doctor.Especialidad_id.ToString()))
            {
                throw new ArgumentException("Especialidad del doctor es requerida");
            }
        }

        private void ValidarResponsable(Responsable responsable)
        {
            if (responsable == null)
            {
                throw new ArgumentException("Datos del responsable son requeridos");
            }    

            if (string.IsNullOrWhiteSpace(responsable.DocumentoID))
            {
                throw new ArgumentException("Documento del responsable es requerido");
            }

            if (string.IsNullOrWhiteSpace(responsable.Primer_Nombre))
            {
                throw new ArgumentException("Nombre del responsable es requerido");
            }

            if (string.IsNullOrWhiteSpace(responsable.Primer_Apellido))
            {
                throw new ArgumentException("Nombre del paciente es requerido");
            }

            if (string.IsNullOrWhiteSpace(responsable.Parentesco))
            {
                throw new ArgumentException("Parentesco es requerido");
            }
        }

        public string ObtenerRolUsuario(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Usuario es requerido");
                }

                return usuarioRepository.ObtenerRolUsuario(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error BLL al obtener rol: {ex.Message}", ex);
            }
        }
    }
}
