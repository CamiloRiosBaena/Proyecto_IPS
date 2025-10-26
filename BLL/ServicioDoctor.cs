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
    public class ServicioDoctor : ICrud<Doctor>
    {
        private DoctorRepository doctorRepository;
        private ServicioEspecialidad servicioEspecialidad;

        public ServicioDoctor()
        {
            doctorRepository = new DoctorRepository();
            servicioEspecialidad = new ServicioEspecialidad();
        }

        public bool Insertar(Doctor doctor)
        {
            if (string.IsNullOrEmpty(doctor.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (doctorRepository.Existe(doctor.DocumentoID))
            {
                throw new Exception("Ya existe un doctor con ese documento");
            }

            if (string.IsNullOrEmpty(doctor.NumeroLicencia))
            {
                throw new Exception("El número de licencia médica es obligatorio");
            }

            if (string.IsNullOrEmpty(doctor.Primer_Nombre))
            {
                throw new Exception("El primer nombre es obligatorio");
            }

            if (string.IsNullOrEmpty(doctor.Primer_Apellido))
            {
                throw new Exception("El primer apellido es obligatorio");
            }

            if (string.IsNullOrEmpty(doctor.Especialidad_id.ToString()))
            {
                throw new Exception("Debe seleccionar una especialidad");
            }

            if (!servicioEspecialidad.Existe(doctor.Especialidad_id.ToString()))
            {
                throw new Exception("La especialidad seleccionada no existe");
            }

            return doctorRepository.Insertar(doctor);
        }

        public bool Actualizar(Doctor doctor)
        {
            if (string.IsNullOrEmpty(doctor.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (!doctorRepository.Existe(doctor.DocumentoID))
            {
                throw new Exception("El doctor no existe");
            }

            if (!string.IsNullOrEmpty(doctor.Especialidad_id.ToString()) && !servicioEspecialidad.Existe(doctor.Especialidad_id.ToString()))
            {
                throw new Exception("La especialidad seleccionada no existe");
            }

            return doctorRepository.Actualizar(doctor);
        }

        public bool Eliminar(string id)
        {
            if (!doctorRepository.Existe(id))
            {
                throw new Exception("El doctor no existe");
            }

            return doctorRepository.Eliminar(id);
        }

        public Doctor ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El documento es obligatorio");
            }

            return doctorRepository.ObtenerPorId(id);
        }

        public List<Doctor> ObtenerTodos()
        {
            return doctorRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return doctorRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return doctorRepository.ObtenerParaCombo();
        }

        public List<Doctor> ObtenerPorEspecialidad(int especialidadId)
        {
            if (string.IsNullOrEmpty(especialidadId.ToString()))
            {
                throw new Exception("Debe especificar una especialidad");
            }

            return doctorRepository.ObtenerPorEspecialidad(especialidadId);
        }

        public List<Doctor> BuscarPorNombre(string nombre)
        {
            List<Doctor> todos = doctorRepository.ObtenerTodos();
            List<Doctor> filtrados = new List<Doctor>();

            foreach (Doctor d in todos)
            {
                string nombreCompleto = (d.Primer_Nombre + " " + d.Segundo_Nombre + " " +
                                        d.Primer_Apellido + " " + d.Segundo_Apellido).ToLower();

                if (nombreCompleto.Contains(nombre.ToLower()))
                {
                    filtrados.Add(d);
                }
            }

            return filtrados;
        }
    }
}

