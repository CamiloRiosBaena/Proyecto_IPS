using DAL;
using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioCredenciales : ICrud<Credenciales>
    {
        private CredencialesRepository credencialesRepository;
        private ServicioPaciente servicioPaciente;
        private ServicioDoctor servicoDoctor;

        public ServicioCredenciales()
        {
            credencialesRepository = new CredencialesRepository();
            servicioPaciente = new ServicioPaciente();
            servicoDoctor = new ServicioDoctor();
        }

        public bool Insertar(Credenciales credencial)
        {
            if (credencial == null)
                throw new Exception("La credencial no puede ser nula");

            if (credencialesRepository.ExisteUsuario(credencial.Nombre_usuario))
                throw new Exception("El nombre de usuario ya existe");

            return credencialesRepository.Insertar(credencial);
        }

        public bool Actualizar(Credenciales credencial)
        {
            if (credencial == null)
                throw new Exception("La credencial no puede ser nula");

            return credencialesRepository.Actualizar(credencial);
        }

        public bool Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("El ID es obligatorio");

            return credencialesRepository.Eliminar(id);
        }

        public Credenciales ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("El ID es obligatorio");

            return credencialesRepository.ObtenerPorId(id);
        }

        public List<Credenciales> ObtenerTodos()
        {
            return credencialesRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("El ID es obligatorio");

            return credencialesRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return credencialesRepository.ObtenerParaCombo();
        }

        public bool IniciarSesion(string username, string password, out string tipoUsuario)
        {
            tipoUsuario = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("El nombre de usuario es obligatorio");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("La contraseña es obligatoria");
            }

            bool credencialesValidas = credencialesRepository.VerificarCredenciales(username, password);

            if (credencialesValidas)
            {
                tipoUsuario = credencialesRepository.ObtenerTipoUsuario(username);

                return true;
            }

            return false;
        }

        public bool RegistrarPaciente(Paciente paciente, string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("El nombre de usuario es obligatorio");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("La contraseña es obligatoria");
            }

            if (password.Length < 6)
            {
                throw new Exception("La contraseña debe tener al menos 6 caracteres");
            }

            if (credencialesRepository.ExisteUsuario(username))
            {
                throw new Exception("El nombre de usuario ya existe");
            }

            if (servicioPaciente.Existe(paciente.DocumentoID))
            {
                throw new Exception("Ya existe un paciente con ese documento");
            }

            Credenciales credencial = new Credenciales
            {
                Id = credencialesRepository.ObtenerSiguienteId(),
                Nombre_usuario = username,
                Tipo_usuario = "PACIENTE",
                Estado = "Activo",
            };

            bool credencialInsertada = credencialesRepository.InsertarConHash(credencial, password);

            paciente.Usuario_id = credencial.Id;

            bool pacienteInsertado = servicioPaciente.Insertar(paciente);

            if (!pacienteInsertado)
            {
                credencialesRepository.Eliminar(credencial.Id.ToString());
                throw new Exception("Error al registrar el paciente");
            }

            return true;
        }

        public bool RegistrarDoctor(Doctor doctor, string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("El nombre de usuario es obligatorio");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("La contraseña es obligatoria");
            }

            if (password.Length < 6)
            {
                throw new Exception("La contraseña debe tener al menos 6 caracteres");
            }

            if (credencialesRepository.ExisteUsuario(username))
            {
                throw new Exception("El nombre de usuario ya existe");
            }

            if (servicoDoctor.Existe(doctor.DocumentoID))
            {
                throw new Exception("Ya existe un doctor con ese documento");
            }

            Credenciales credencial = new Credenciales
            {
                Id = credencialesRepository.ObtenerSiguienteId(),
                Nombre_usuario = username,
                Tipo_usuario = "DOCTOR",
                Estado = "Activo",
            };

            bool credencialInsertada = credencialesRepository.InsertarConHash(credencial, password);

            if (!credencialInsertada)
            {
                throw new Exception("Error al crear las credenciales del doctor");
            }

            doctor.Usuario_id = credencial.Id;

            bool doctorInsertado = servicoDoctor.Insertar(doctor);

            if (!doctorInsertado)
            {
                credencialesRepository.Eliminar(credencial.Id.ToString());
                throw new Exception("Error al registrar el doctor");
            }

            return true;
        }

        public bool CambiarContrasena(string username, string passwordActual, string passwordNueva)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("El nombre de usuario es obligatorio");
            }

            if (string.IsNullOrEmpty(passwordNueva))
            {
                throw new Exception("La nueva contraseña es obligatoria");
            }

            if (passwordNueva.Length < 6)
            {
                throw new Exception("La nueva contraseña debe tener al menos 6 caracteres");
            }

            if (passwordActual == passwordNueva)
            {
                throw new Exception("La nueva contraseña debe ser diferente a la actual");
            }

            return credencialesRepository.CambiarPassword(username, passwordActual, passwordNueva);
        }

        public bool DesactivarUsuario(string username)
        {
            if (!credencialesRepository.ExisteUsuario(username))
            {
                throw new Exception("El usuario no existe");
            }

            return credencialesRepository.CambiarEstado(username, "Inactivo");
        }

        public bool ActivarUsuario(string username)
        {
            if (!credencialesRepository.ExisteUsuario(username))
            {
                throw new Exception("El usuario no existe");
            }

            return credencialesRepository.CambiarEstado(username, "Activo");
        }

        public object ObtenerDatosUsuario(string username)
        {
            return credencialesRepository.ObtenerPorNombreUsuario(username);
        }

        public bool ExisteUsuario(string username)
        {
            return credencialesRepository.ExisteUsuario(username);
        }
    }
}
