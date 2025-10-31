using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HistoriaClinicaRepository : BaseConsultaRepository<HistoriaClinica>, IPLSQLRepository<HistoriaClinica>
    {
        protected override string NombreTabla
        {
            get { return "s_historias_clinicas"; }
        }

        protected override string Id
        {
            get { return "historia_id"; }
        }

        protected override string Primer_Nombre
        {
            get { return "historia_id"; }
        }

        protected override HistoriaClinica MapearDesdeReader(OracleDataReader reader)
        {
            return new HistoriaClinica
            {
                Historia_id = Convert.ToInt32(reader["historia_id"]),
                Paciente_documentoid = reader["paciente_documentoid"].ToString(),
                Doctor_documentoid = reader["doctor_documentoid"].ToString(),
                Cita_id = Convert.ToInt32(reader["cita_id"]),
                Especialidad_id = Convert.ToInt32(reader["especialidad_id"]),
                Diagnostico = reader["diagnostico"] != DBNull.Value ? reader["diagnostico"].ToString() : null,
                Tratamiento = reader["tratamiento"] != DBNull.Value ? reader["tratamiento"].ToString() : null,
                Observaciones = reader["observaciones"] != DBNull.Value ? reader["observaciones"].ToString() : null
            };
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return $"Historia #{reader["historia_id"]}";
        }

        public bool Insertar(HistoriaClinica h)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_HISTORIA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_historia_id", OracleDbType.Int32).Value = h.Historia_id;
                        cmd.Parameters.Add("p_paciente_doc", OracleDbType.Varchar2).Value = h.Paciente_documentoid;
                        cmd.Parameters.Add("p_doctor_doc", OracleDbType.Varchar2).Value = h.Doctor_documentoid;
                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = h.Cita_id;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = h.Especialidad_id;
                        cmd.Parameters.Add("p_diagnostico", OracleDbType.Varchar2).Value = (object)h.Diagnostico ?? DBNull.Value;
                        cmd.Parameters.Add("p_tratamiento", OracleDbType.Varchar2).Value = (object)h.Tratamiento ?? DBNull.Value;
                        cmd.Parameters.Add("p_observaciones", OracleDbType.Varchar2).Value = (object)h.Observaciones ?? DBNull.Value;

                        OracleParameter resultParam = new OracleParameter("p_resultado", OracleDbType.Int32);
                        resultParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();
                        return Convert.ToInt32(resultParam.Value.ToString()) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar historia clínica: " + ex.Message);
            }
        }

        public bool Actualizar(HistoriaClinica h)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_HISTORIA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_historia_id", OracleDbType.Int32).Value = h.Historia_id;
                        cmd.Parameters.Add("p_diagnostico", OracleDbType.Varchar2).Value = (object)h.Diagnostico ?? DBNull.Value;
                        cmd.Parameters.Add("p_tratamiento", OracleDbType.Varchar2).Value = (object)h.Tratamiento ?? DBNull.Value;
                        cmd.Parameters.Add("p_observaciones", OracleDbType.Varchar2).Value = (object)h.Observaciones ?? DBNull.Value;

                        OracleParameter resultParam = new OracleParameter("p_resultado", OracleDbType.Int32);
                        resultParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();
                        return Convert.ToInt32(resultParam.Value.ToString()) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar historia clínica: " + ex.Message);
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ELIMINAR_GENERICO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_tabla", OracleDbType.Varchar2).Value = NombreTabla;
                        cmd.Parameters.Add("p_campo_id", OracleDbType.Varchar2).Value = Id;
                        cmd.Parameters.Add("p_valor_id", OracleDbType.Varchar2).Value = id;

                        OracleParameter resultParam = new OracleParameter("p_resultado", OracleDbType.Int32);
                        resultParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();
                        return Convert.ToInt32(resultParam.Value.ToString()) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar historia clínica: " + ex.Message);
            }
        }

        public bool Existe(string id)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_EXISTE_GENERICO(:p_tabla, :p_campo, :p_valor) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_tabla", OracleDbType.Varchar2).Value = NombreTabla;
                        cmd.Parameters.Add("p_campo", OracleDbType.Varchar2).Value = Id;
                        cmd.Parameters.Add("p_valor", OracleDbType.Varchar2).Value = id;

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia: " + ex.Message);
            }
        }

        public int ObtenerSiguienteId()
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_SIGUIENTE_ID_HISTORIA() FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener siguiente ID: " + ex.Message);
            }
        }

        public bool GuardarHistoria(int citaId, string diagnostico, string tratamiento, string observaciones)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GUARDAR_HISTORIA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = citaId;
                        cmd.Parameters.Add("p_diagnostico", OracleDbType.Varchar2).Value = (object)diagnostico ?? DBNull.Value;
                        cmd.Parameters.Add("p_tratamiento", OracleDbType.Varchar2).Value = (object)tratamiento ?? DBNull.Value;
                        cmd.Parameters.Add("p_observaciones", OracleDbType.Varchar2).Value = (object)observaciones ?? DBNull.Value;

                        OracleParameter resultParam = new OracleParameter("p_resultado", OracleDbType.Int32);
                        resultParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();
                        return Convert.ToInt32(resultParam.Value.ToString()) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar historia: " + ex.Message);
            }
        }

        public List<HistoriaClinica> ObtenerPorPaciente(string documentoPaciente)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_HISTORIAS_PACIENTE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_paciente_doc", OracleDbType.Varchar2).Value = documentoPaciente;

                        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                        cursorParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(cursorParam);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(MapearDesdeReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historias por paciente: " + ex.Message);
            }
            return lista;
        }

        public List<HistoriaClinica> ObtenerPorDoctor(string documentoDoctor)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_HISTORIAS_DOCTOR", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_doctor_doc", OracleDbType.Varchar2).Value = documentoDoctor;

                        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                        cursorParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(cursorParam);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(MapearDesdeReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historias por doctor: " + ex.Message);
            }
            return lista;
        }

        public List<HistoriaClinica> ObtenerPorPacienteYEspecialidad(string documentoPaciente, int especialidadId)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_HISTORIAS_ESP", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_paciente_doc", OracleDbType.Varchar2).Value = documentoPaciente;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = especialidadId;

                        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                        cursorParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(cursorParam);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(MapearDesdeReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historias por especialidad: " + ex.Message);
            }
            return lista;
        }

        public HistoriaClinica ObtenerPorCita(int citaId)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_HISTORIA_CITA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = citaId;

                        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                        cursorParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(cursorParam);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearDesdeReader(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historia por cita: " + ex.Message);
            }
        }

        public bool ExistePorCita(int citaId)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_EXISTE_HISTORIA_CITA(:p_cita_id) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = citaId;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar historia por cita: " + ex.Message);
            }
        }

        protected override object ObtenerValorId(HistoriaClinica historia)
        {
            return historia.Historia_id;
        }
    }
}
