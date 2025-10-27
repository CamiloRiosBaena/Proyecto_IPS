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
    public class ServicioHistoriaClinica : ICrud<HistoriaClinica>
    {
        private HistoriaClinicaRepository historiaRepository;
        private ServicioPaciente servicioPaciente;
        private ServicioDoctor servicioDoctor;
        private ServicioCita servicioCita;
        private ServicioEspecialidad servicioEspecialidad;

        public ServicioHistoriaClinica()
        {
            historiaRepository = new HistoriaClinicaRepository();
            servicioPaciente = new ServicioPaciente();
            servicioDoctor = new ServicioDoctor();
            servicioCita = new ServicioCita();
            servicioEspecialidad = new ServicioEspecialidad();
        }

        public bool Insertar(HistoriaClinica historia)
        {
            if (string.IsNullOrEmpty(historia.Paciente_documentoid))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            if (!servicioPaciente.Existe(historia.Paciente_documentoid))
            {
                throw new Exception("El paciente no existe");
            }

            if (string.IsNullOrEmpty(historia.Doctor_documentoid))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            if (!servicioDoctor.Existe(historia.Doctor_documentoid))
            {
                throw new Exception("El doctor no existe");
            }

            if (historia.Cita_id <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            if (!servicioCita.Existe(historia.Cita_id.ToString()))
            {
                throw new Exception("La cita no existe");
            }

            if (historiaRepository.ExistePorCita(historia.Cita_id))
            {
                throw new Exception("Ya existe una historia clínica para esta cita");
            }

            if (historia.Especialidad_id <= 0)
            {
                throw new Exception("La especialidad es obligatoria");
            }

            if (!servicioEspecialidad.Existe(historia.Especialidad_id.ToString()))
            {
                throw new Exception("La especialidad no existe");
            }

            Cita cita = servicioCita.ObtenerPorId(historia.Cita_id.ToString());
            if (cita.Documento_doctor != historia.Doctor_documentoid)
            {
                throw new Exception("El doctor debe ser el mismo que atendió la cita");
            }

            if (cita.Documento_paciente != historia.Paciente_documentoid)
            {
                throw new Exception("El paciente debe ser el mismo de la cita");
            }

            if (cita.Especialidad_id != historia.Especialidad_id)
            {
                throw new Exception("La especialidad debe coincidir con la de la cita");
            }

            if (string.IsNullOrWhiteSpace(historia.Diagnostico) &&
                string.IsNullOrWhiteSpace(historia.Tratamiento) &&
                string.IsNullOrWhiteSpace(historia.Observaciones))
            {
                throw new Exception("Debe ingresar al menos un diagnóstico, tratamiento u observación");
            }

            historia.Historia_id = historiaRepository.ObtenerSiguienteId();

            return historiaRepository.Insertar(historia);
        }

        public bool Actualizar(HistoriaClinica historia)
        {
            if (historia.Historia_id <= 0)
            {
                throw new Exception("El ID de la historia es obligatorio");
            }

            if (!historiaRepository.Existe(historia.Historia_id.ToString()))
            {
                throw new Exception("La historia clínica no existe");
            }

            if (string.IsNullOrWhiteSpace(historia.Diagnostico) &&
                string.IsNullOrWhiteSpace(historia.Tratamiento) &&
                string.IsNullOrWhiteSpace(historia.Observaciones))
            {
                throw new Exception("Debe ingresar al menos un diagnóstico, tratamiento u observación");
            }

            return historiaRepository.Actualizar(historia);
        }

        public bool Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            if (!historiaRepository.Existe(id))
            {
                throw new Exception("La historia clínica no existe");
            }

            return historiaRepository.Eliminar(id);
        }

        public HistoriaClinica ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            return historiaRepository.ObtenerPorId(id);
        }

        public List<HistoriaClinica> ObtenerTodos()
        {
            return historiaRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return historiaRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return historiaRepository.ObtenerParaCombo();
        }

        public List<HistoriaClinica> ObtenerHistorialCompletoPaciente(string documentoPaciente)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return historiaRepository.ObtenerPorPaciente(documentoPaciente);
        }

        public List<HistoriaClinica> ObtenerHistorialPorEspecialidad(string documentoPaciente, int especialidadId)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            if (especialidadId <= 0)
            {
                throw new Exception("La especialidad es obligatoria");
            }

            return historiaRepository.ObtenerPorPacienteYEspecialidad(documentoPaciente, especialidadId);
        }

        public List<HistoriaClinica> ObtenerHistoriasDoctor(string documentoDoctor)
        {
            if (string.IsNullOrEmpty(documentoDoctor))
            {
                throw new Exception("El documento del doctor es obligatorio");
            }

            return historiaRepository.ObtenerPorDoctor(documentoDoctor);
        }

        public HistoriaClinica ObtenerPorCita(int citaId)
        {
            if (citaId <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            return historiaRepository.ObtenerPorCita(citaId);
        }

        public bool ExisteHistoriaParaCita(int citaId)
        {
            if (citaId <= 0)
            {
                throw new Exception("El ID de la cita es obligatorio");
            }

            return historiaRepository.ExistePorCita(citaId);
        }

        public bool GuardarHistoriaDesdeConsulta(int citaId, string diagnostico, string tratamiento, string observaciones)
        {
            try
            {
                Cita cita = servicioCita.ObtenerPorId(citaId.ToString());
                if (cita == null)
                {
                    throw new Exception("La cita no existe");
                }

                Doctor doctor = servicioDoctor.ObtenerPorId(cita.Documento_doctor);
                if (doctor == null)
                {
                    throw new Exception("El doctor no existe");
                }

                if (historiaRepository.ExistePorCita(citaId))
                {
                    HistoriaClinica historiaExistente = historiaRepository.ObtenerPorCita(citaId);
                    historiaExistente.Diagnostico = diagnostico;
                    historiaExistente.Tratamiento = tratamiento;
                    historiaExistente.Observaciones = observaciones;

                    return Actualizar(historiaExistente);
                }
                else
                {
                    HistoriaClinica nuevaHistoria = new HistoriaClinica
                    {
                        Paciente_documentoid = cita.Documento_paciente,
                        Doctor_documentoid = cita.Documento_doctor,
                        Cita_id = citaId,
                        Especialidad_id = doctor.Especialidad_id,
                        Diagnostico = diagnostico,
                        Tratamiento = tratamiento,
                        Observaciones = observaciones
                    };

                    return Insertar(nuevaHistoria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar historia: {ex.Message}");
            }
        }

        public int ContarHistoriasPorPaciente(string documentoPaciente)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return historiaRepository.ObtenerPorPaciente(documentoPaciente).Count;
        }

        public int ContarHistoriasPorEspecialidad(string documentoPaciente, int especialidadId)
        {
            if (string.IsNullOrEmpty(documentoPaciente))
            {
                throw new Exception("El documento del paciente es obligatorio");
            }

            return historiaRepository.ObtenerPorPacienteYEspecialidad(documentoPaciente, especialidadId).Count;
        }
    }
}
