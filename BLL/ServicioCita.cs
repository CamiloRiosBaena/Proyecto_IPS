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
    public class ServicioCita : ICrud<Cita>
    {
        private CitaRepository citaRepository;
        private ServicioPaciente servicioPaciente;
        private ServicioDoctor servicioDoctor;
        private ServicioEspecialidad servicioEspecialidad;

        public ServicioCita()
        {
            citaRepository = new CitaRepository();
            servicioPaciente = new ServicioPaciente();
            servicioDoctor = new ServicioDoctor();
            servicioEspecialidad = new ServicioEspecialidad();
        }

        public bool Insertar(Cita cita)
        {
            if (string.IsNullOrEmpty(cita.Documento_paciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            if (!servicioPaciente.Existe(cita.Documento_paciente))
            {
                throw new Exception("El paciente no existe en el sistema");
            }

            if (string.IsNullOrEmpty(cita.Documento_doctor))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            if (!servicioDoctor.Existe(cita.Documento_doctor))
            {
                throw new Exception("El doctor no existe en el sistema");
            }

            if (cita.Especialidad_id <= 0)
            {
                throw new Exception("Debe seleccionar una especialidad");
            }

            if (!servicioEspecialidad.Existe(cita.Especialidad_id.ToString()))
            {
                throw new Exception("La especialidad seleccionada no existe");
            }

            if (cita.Fecha < DateTime.Today)
            {
                throw new Exception("No se pueden agendar citas en fechas pasadas");
            }

            if (string.IsNullOrEmpty(cita.Hora))
            {
                throw new Exception("Debe seleccionar una hora");
            }

            if (citaRepository.ExisteCitaEnHorario(cita.Documento_doctor, cita.Fecha, cita.Hora))
            {
                throw new Exception("El doctor ya tiene una cita agendada en ese horario");
            }

            if (string.IsNullOrEmpty(cita.Estado))
            {
                cita.Estado = "Pendiente"; 
            }

            cita.Id = citaRepository.ObtenerSiguienteId();

            return citaRepository.Insertar(cita);
        }

        public bool Actualizar(Cita cita)
        {
            if (cita.Id <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            if (!citaRepository.Existe(cita.Id.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            if (!string.IsNullOrEmpty(cita.Documento_doctor) &&
                !servicioDoctor.Existe(cita.Documento_doctor))
            {
                throw new Exception("El doctor seleccionado no existe");
            }

            Cita citaOriginal = citaRepository.ObtenerPorId(cita.Id.ToString());

            if (citaOriginal.Fecha != cita.Fecha ||
                citaOriginal.Hora != cita.Hora ||
                citaOriginal.Documento_doctor != cita.Documento_doctor)
            {
                if (citaRepository.ExisteCitaEnHorario(cita.Documento_doctor, cita.Fecha, cita.Hora))
                {
                    throw new Exception("El doctor ya tiene una cita agendada en ese horario");
                }
            }

            return citaRepository.Actualizar(cita);
        }

        public bool Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                throw new Exception("El ID es obligatorio");
            }

            if (!citaRepository.Existe(id.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            return citaRepository.Eliminar(id.ToString());
        }

        public Cita ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            return citaRepository.ObtenerPorId(id);
        }

        public List<Cita> ObtenerTodos()
        {
            return citaRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return citaRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return citaRepository.ObtenerParaCombo();
        }

        public List<Cita> ObtenerCitasPorPaciente(string documentoPaciente)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return citaRepository.ObtenerPorPaciente(documentoPaciente);
        }

        public List<Cita> ObtenerCitasPorDoctor(string documentoDoctor)
        {
            if (string.IsNullOrEmpty(documentoDoctor))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            return citaRepository.ObtenerPorDoctor(documentoDoctor);
        }

        public List<Cita> ObtenerCitasPorFecha(DateTime fecha)
        {
            return citaRepository.ObtenerPorFecha(fecha);
        }

        public List<Cita> ObtenerCitasPorEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
            {
                throw new Exception("El estado es obligatorio");
            }

            return citaRepository.ObtenerPorEstado(estado);
        }

        public List<Cita> ObtenerCitasPendientesPaciente(string documentoPaciente)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return citaRepository.ObtenerCitasPendientesPaciente(documentoPaciente);
        }

        public bool CancelarCita(int idCita)
        {
            if (idCita <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            if (!citaRepository.Existe(idCita.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            Cita cita = citaRepository.ObtenerPorId(idCita.ToString());

            if (cita.Estado == "Cancelada")
            {
                throw new Exception("La cita ya está cancelada");
            }

            if (cita.Estado == "Completada")
            {
                throw new Exception("No se puede cancelar una cita completada");
            }

            return citaRepository.CambiarEstado(idCita, "Cancelada");
        }

        public bool CompletarCita(int idCita)
        {
            if (idCita <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            if (!citaRepository.Existe(idCita.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            Cita cita = citaRepository.ObtenerPorId(idCita.ToString());

            if (cita.Estado == "Cancelada")
            {
                throw new Exception("No se puede completar una cita cancelada");
            }

            if (cita.Estado == "Completada")
            {
                throw new Exception("La cita ya está completada");
            }

            return citaRepository.CambiarEstado(idCita, "Completada");
        }

        public bool ReactivarCita(int idCita)
        {
            if (idCita <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            if (!citaRepository.Existe(idCita.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            Cita cita = citaRepository.ObtenerPorId(idCita.ToString());

            if (cita.Estado == "Pendiente")
            {
                throw new Exception("La cita ya está activa");
            }

            if (citaRepository.ExisteCitaEnHorario(cita.Documento_doctor, cita.Fecha, cita.Hora))
            {
                throw new Exception("El horario ya no está disponible");
            }

            return citaRepository.CambiarEstado(idCita, "Pendiente");
        }

        public bool VerificarDisponibilidad(string documentoDoctor, DateTime fecha, string hora)
        {
            if (string.IsNullOrEmpty(documentoDoctor))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            if (string.IsNullOrEmpty(hora))
            {
                throw new Exception("La hora es obligatoria");
            }

            return !citaRepository.ExisteCitaEnHorario(documentoDoctor, fecha, hora);
        }

        public int ContarCitasPorPaciente(string documentoPaciente)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return citaRepository.ObtenerPorPaciente(documentoPaciente).Count;
        }

        public int ContarCitasPorDoctor(string documentoDoctor)
        {
            if (string.IsNullOrEmpty(documentoDoctor))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            return citaRepository.ObtenerPorDoctor(documentoDoctor).Count;
        }

        public List<Cita> ObtenerCitasDelDia()
        {
            return citaRepository.ObtenerPorFecha(DateTime.Today);
        }

        public List<Cita> BuscarCitasPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Cita> todasCitas = citaRepository.ObtenerTodos();
            List<Cita> citasFiltradas = new List<Cita>();

            foreach (Cita cita in todasCitas)
            {
                if (cita.Fecha >= fechaInicio && cita.Fecha <= fechaFin)
                {
                    citasFiltradas.Add(cita);
                }
            }

            return citasFiltradas;
        }
    }
}
