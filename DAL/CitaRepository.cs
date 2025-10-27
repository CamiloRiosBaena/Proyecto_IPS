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
    public class CitaRepository : BaseDALRepository<Cita>
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

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla} 
                    (cita_id, paciente_documentoid, doctor_documentoid, fecha, hora, 
                     especialidad_id, estado)
                    VALUES 
                    (:cita_id, :paciente_documentoid, :doctor_documentoid, TO_DATE(:fecha, 'DD/MM/YYYY'), :hora, 
                     :especialidad_id, :estado)";
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla} 
                    SET paciente_documentoid = :paciente_documentoid,
                    doctor_documentoid = :doctor_documentoid,
                    fecha = TO_DATE(:fecha, 'DD/MM/YYYY'),
                    hora = :hora,
                    especialidad_id = :especialidad_id,
                    estado = :estado,
                    WHERE cita_id = :cita_id";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Cita c)
        {
            cmd.Parameters.Add("cita_id", OracleDbType.Int32).Value =  c.Id;
            cmd.Parameters.Add("paciente_documentoid", OracleDbType.Varchar2).Value = c.Documento_paciente;
            cmd.Parameters.Add("doctor_documentoid", OracleDbType.Varchar2).Value = c.Documento_doctor;

            DateTime Fecha = new DateTime(c.Fecha.Year, c.Fecha.Month, c.Fecha.Day);
            cmd.Parameters.Add("fecha", OracleDbType.Date).Value = Fecha;

            cmd.Parameters.Add("hora", OracleDbType.Varchar2).Value = c.Hora;
            cmd.Parameters.Add("especialidad_id", OracleDbType.Int32).Value = c.Especialidad_id;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = c.Estado;
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Cita c)
        {
            cmd.Parameters.Add("cita_id", OracleDbType.Int32).Value = c.Id;
            cmd.Parameters.Add("paciente_documentoid", OracleDbType.Varchar2).Value = c.Documento_paciente;
            cmd.Parameters.Add("doctor_documentoid", OracleDbType.Varchar2).Value = c.Documento_doctor;
            cmd.Parameters.Add("fecha", OracleDbType.Date).Value = c.Fecha.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("hora", OracleDbType.Varchar2).Value = c.Hora;
            cmd.Parameters.Add("especialidad_id", OracleDbType.Int32).Value = c.Especialidad_id;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = c.Estado;
        }

        protected override object ObtenerValorId(Cita c)
        {
            return c.Id;
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return $"Cita #{reader["cita_id"]} - {Convert.ToDateTime(reader["fecha"]):dd/MM/yyyy}";
        }

        public int ObtenerSiguienteId()
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT NVL(MAX(cita_id), 0) + 1 FROM s_citas";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener siguiente ID: " + ex.Message);
            }
        }

        public List<Cita> ObtenerPorPaciente(string documentoPaciente)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} WHERE paciente_documentoid = :doc ORDER BY fecha DESC, hora DESC";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("doc", OracleDbType.Varchar2).Value = documentoPaciente;
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
                    string query = $"SELECT * FROM {NombreTabla} WHERE doctor_documentoid = :doc ORDER BY fecha DESC, hora DESC";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("doc", OracleDbType.Varchar2).Value = documentoDoctor;
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
                    string query = $"SELECT * FROM {NombreTabla} WHERE TRUNC(fecha) = TRUNC(:fecha) ORDER BY hora";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("fecha", fecha));
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
                    string query = $"SELECT * FROM {NombreTabla} WHERE estado = :estado ORDER BY fecha DESC, hora DESC";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("estado", estado));
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

        public bool ExisteCitaEnHorario(string doctorDocumento, DateTime fecha, string hora)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $@"SELECT COUNT(*) FROM {NombreTabla} 
                                    WHERE doctor_documentoid = :doc 
                                    AND TRUNC(fecha) = TRUNC(:fecha) 
                                    AND hora = :hora
                                    AND estado != 'Cancelada'";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("doc", doctorDocumento));
                        cmd.Parameters.Add(new OracleParameter("fecha", fecha));
                        cmd.Parameters.Add(new OracleParameter("hora", hora));

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
                    string query = $"UPDATE {NombreTabla} SET estado = :estado WHERE cita_id = :id";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("estado", nuevoEstado));
                        cmd.Parameters.Add(new OracleParameter("cita_id", idCita));

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de cita: " + ex.Message);
            }
        }

        public List<Cita> ObtenerCitasPendientesPaciente(string documentoPaciente)
        {
            List<Cita> lista = new List<Cita>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $@"SELECT * FROM {NombreTabla}
                                    WHERE paciente_documentoid = :doc 
                                    AND estado = 'Pendiente'
                                    AND fecha >= TRUNC(SYSDATE)
                                    ORDER BY fecha, hora";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("doc", documentoPaciente));
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
    }
}
