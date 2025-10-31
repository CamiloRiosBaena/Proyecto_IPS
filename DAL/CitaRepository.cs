using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CitaRepository : BaseConsultaRepository<Cita>, IPLSQLRepository<Cita>
    {
        protected override string NombreTabla
        {
            get { return "s_citas"; }
        }

        protected override string Id
        {
            get { return "cita_id"; }
        }

        protected override string Primer_Nombre
        {
            get { return "id"; }
        }

        protected override Cita MapearDesdeReader(OracleDataReader reader)
        {
            return new Cita
            {
                Id = Convert.ToInt32(reader["cita_id"]),
                Documento_paciente = reader["paciente_documentoid"].ToString(),
                Documento_doctor = reader["doctor_documentoid"].ToString(),
                Fecha = Convert.ToDateTime(reader["fecha"]),
                Hora = reader["hora"].ToString(),
                Especialidad_id = Convert.ToInt32(reader["especialidad_id"]),
                Estado = reader["estado"].ToString(),
            };
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return $"Cita #{reader["cita_id"]} - {Convert.ToDateTime(reader["fecha"]):dd/MM/yyyy}";
        }

        public bool Insertar(Cita c)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_CITA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = c.Id;
                        cmd.Parameters.Add("p_paciente_doc", OracleDbType.Varchar2).Value = c.Documento_paciente;
                        cmd.Parameters.Add("p_doctor_doc", OracleDbType.Varchar2).Value = c.Documento_doctor;
                        cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = c.Fecha;
                        cmd.Parameters.Add("p_hora", OracleDbType.Varchar2).Value = c.Hora;
                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = c.Estado;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = c.Especialidad_id;

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
                throw new Exception("Error al insertar cita: " + ex.Message);
            }
        }

        public bool Actualizar(Cita c)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_CITA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = c.Id;
                        cmd.Parameters.Add("p_paciente_doc", OracleDbType.Varchar2).Value = c.Documento_paciente;
                        cmd.Parameters.Add("p_doctor_doc", OracleDbType.Varchar2).Value = c.Documento_doctor;
                        cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = c.Fecha;
                        cmd.Parameters.Add("p_hora", OracleDbType.Varchar2).Value = c.Hora;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = c.Especialidad_id;
                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = c.Estado;

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
                throw new Exception("Error al actualizar cita: " + ex.Message);
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
                throw new Exception("Error al eliminar cita: " + ex.Message);
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
                    string query = "SELECT FN_SIGUIENTE_ID_CITA() FROM DUAL";
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

        public bool ExisteCitaEnHorario(string doctorDocumento, DateTime fecha, string hora)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_EXISTE_CITA_HORARIO(:p_doctor_doc, :p_fecha, :p_hora) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_doctor_doc", OracleDbType.Varchar2).Value = doctorDocumento;
                        cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = fecha;
                        cmd.Parameters.Add("p_hora", OracleDbType.Varchar2).Value = hora;

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar disponibilidad: " + ex.Message);
            }
        }

        public bool CambiarEstado(int idCita, string nuevoEstado)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_CAMBIAR_ESTADO_CITA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_cita_id", OracleDbType.Int32).Value = idCita;
                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = nuevoEstado;

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
                throw new Exception("Error al cambiar estado de cita: " + ex.Message);
            }
        }

        public List<Cita> ObtenerPorPaciente(string documentoPaciente)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_CITAS_PACIENTE", conn))
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
                throw new Exception("Error al obtener citas por paciente: " + ex.Message);
            }
            return lista;
        }

        public List<Cita> ObtenerPorDoctor(string documentoDoctor)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_CITAS_DOCTOR", conn))
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
                throw new Exception("Error al obtener citas por doctor: " + ex.Message);
            }
            return lista;
        }

        public List<Cita> ObtenerPorFecha(DateTime fecha)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_CITAS_FECHA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = fecha;

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
                throw new Exception("Error al obtener citas por fecha: " + ex.Message);
            }
            return lista;
        }

        public List<Cita> ObtenerPorEstado(string estado)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_CITAS_ESTADO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = estado;

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
                throw new Exception("Error al obtener citas por estado: " + ex.Message);
            }
            return lista;
        }

        public List<Cita> ObtenerCitasPendientesPaciente(string documentoPaciente)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_CITAS_PENDIENTES_PACIENTE", conn))
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
                throw new Exception("Error al obtener citas pendientes: " + ex.Message);
            }
            return lista;
        }

        protected override object ObtenerValorId(Cita c)
        {
            return c.Id;
        }
    }
}
