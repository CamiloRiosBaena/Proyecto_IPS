using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioPaciente : ICrud<Paciente>
    {
        private PacienteRepository pacienteRepository;
        private ServicioCiudad servicioCiudad;
        private ServicioEPS servicioEPS;
        private ServicioResponsable servicioResponsable;

        public ServicioPaciente()
        {
            pacienteRepository = new PacienteRepository();
            servicioCiudad = new ServicioCiudad();
            servicioEPS = new ServicioEPS();
            servicioResponsable = new ServicioResponsable();
        }

        public bool Insertar(Paciente paciente)
        {
            if (string.IsNullOrEmpty(paciente.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (pacienteRepository.Existe(paciente.DocumentoID))
            {
                throw new Exception("Ya existe un paciente con ese documento");
            }

            if (string.IsNullOrEmpty(paciente.Primer_Nombre))
            {
                throw new Exception("El primer nombre es obligatorio");
            }

            if (string.IsNullOrEmpty(paciente.Primer_Apellido))
            {
                throw new Exception("El primer apellido es obligatorio");
            }

            if (string.IsNullOrEmpty(paciente.Ciudad_id.ToString()))
            {
                throw new Exception("Debe seleccionar una ciudad");
            }

            if (!servicioCiudad.Existe(paciente.Ciudad_id.ToString()))
            {
                throw new Exception("La ciudad seleccionada no existe");
            }

            if (string.IsNullOrEmpty(paciente.EPS_id.ToString()))
            {
                throw new Exception("Debe seleccionar una EPS");
            }

            if (!servicioEPS.Existe(paciente.EPS_id.ToString()))
            {
                throw new Exception("La EPS seleccionada no existe");
            }

            if (paciente.Edad < 0 || paciente.Edad > 150)
            {
                throw new Exception("La edad debe estar entre 0 y 150 años");
            }

            if (paciente.Edad < 18 && string.IsNullOrEmpty(paciente.Documento_responsable))
            {
                throw new Exception("Los menores de edad deben tener un responsable asignado");
            }

            if (!string.IsNullOrEmpty(paciente.Documento_responsable))
            {
                if (!servicioResponsable.Existe(paciente.Documento_responsable))
                {
                    throw new Exception("El responsable seleccionado no existe");
                }
            }

            if (paciente.Sexo != 'M' && paciente.Sexo != 'F')
            {
                throw new Exception("El sexo debe ser M (Masculino) o F (Femenino)");
            }

            if (paciente.RH != '+' && paciente.RH != '-')
            {
                throw new Exception("El RH debe ser + o -");
            }

            return pacienteRepository.Insertar(paciente);
        }

        public bool Actualizar(Paciente paciente)
        {
            if (string.IsNullOrEmpty(paciente.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (!pacienteRepository.Existe(paciente.DocumentoID))
            {
                throw new Exception("El paciente no existe");
            }

            if (!string.IsNullOrEmpty(paciente.Ciudad_id.ToString()) && !servicioCiudad.Existe(paciente.Ciudad_id.ToString()))
            {
                throw new Exception("La ciudad seleccionada no existe");
            }

            if (!string.IsNullOrEmpty(paciente.EPS_id.ToString()) && !servicioEPS.Existe(paciente.EPS_id.ToString()))
            {
                throw new Exception("La EPS seleccionada no existe");
            }

            if (!string.IsNullOrEmpty(paciente.Documento_responsable) &&
                !servicioResponsable.Existe(paciente.Documento_responsable))
            {
                throw new Exception("El responsable seleccionado no existe");
            }

            return pacienteRepository.Actualizar(paciente);
        }

        public bool Eliminar(string id)
        {
            if (!pacienteRepository.Existe(id))
            {
                throw new Exception("El paciente no existe");
            }

            return pacienteRepository.Eliminar(id);
        }

        public Paciente ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El documento es obligatorio");
            }

            return pacienteRepository.ObtenerPorId(id);
        }

        public List<Paciente> ObtenerTodos()
        {
            return pacienteRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return pacienteRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return pacienteRepository.ObtenerParaCombo();
        }

        public List<Paciente> BuscarPorNombre(string nombre)
        {
            List<Paciente> todos = pacienteRepository.ObtenerTodos();
            List<Paciente> filtrados = new List<Paciente>();

            foreach (Paciente p in todos)
            {
                string nombreCompleto = (p.Primer_Nombre + " " + p.Segundo_Nombre + " " +
                                         p.Primer_Apellido + " " + p.Segundo_Apellido).ToLower();

                if (nombreCompleto.Contains(nombre.ToLower()))
                {
                    filtrados.Add(p);
                }
            }

            return filtrados;
        }

        public List<Paciente> ObtenerMenoresDeEdad()
        {
            List<Paciente> todos = pacienteRepository.ObtenerTodos();
            List<Paciente> menores = new List<Paciente>();

            foreach (Paciente p in todos)
            {
                if (p.Edad < 18)
                {
                    menores.Add(p);
                }
            }

            return menores;
        }

        public List<Paciente> ObtenerPorEPS(int epsId)
        {
            List<Paciente> todos = pacienteRepository.ObtenerTodos();
            List<Paciente> filtrados = new List<Paciente>();

            foreach (Paciente p in todos)
            {
                if (p.EPS_id == epsId)
                {
                    filtrados.Add(p);
                }
            }

            return filtrados;
        }

        public List<Paciente> ObtenerPorCiudad(int ciudadId)
        {
            List<Paciente> todos = pacienteRepository.ObtenerTodos();
            List<Paciente> filtrados = new List<Paciente>();

            foreach (Paciente p in todos)
            {
                if (p.Ciudad_id == ciudadId)
                {
                    filtrados.Add(p);
                }
            }

            return filtrados;
        }
    }
}
