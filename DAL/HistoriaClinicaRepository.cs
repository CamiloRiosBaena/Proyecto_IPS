using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HistoriaClinicaRepository : BaseDALRepository<HistoriaClinica>
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

        protected override string ObtenerQueryInsert()
        {
            return @"INSERT INTO historias_clinicas 
                    (historia_id, paciente_documentoid, doctor_documentoid, cita_id, 
                     especialidad_id, diagnostico, tratamiento, observaciones)
                    VALUES 
                    (:historia_id, :paciente_documentoid, :doctor_documentoid, :cita_id, 
                     :especialidad_id, :diagnostico, :tratamiento, :observaciones)";
        }

        protected override string ObtenerQueryUpdate()
        {
            return @"UPDATE historias_clinicas 
                    SET diagnostico = :diagnostico,
                        tratamiento = :tratamiento,
                        observaciones = :observaciones
                    WHERE historia_id = :historia_id";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, HistoriaClinica h)
        {
            cmd.Parameters.Add("historia_id", OracleDbType.Int32).Value = h.Historia_id;
            cmd.Parameters.Add("paciente_documentoid", OracleDbType.Varchar2).Value = h.Paciente_documentoid;
            cmd.Parameters.Add("doctor_documentoid", OracleDbType.Varchar2).Value = h.Doctor_documentoid;
            cmd.Parameters.Add("cita_id", OracleDbType.Int32).Value = h.Cita_id;
            cmd.Parameters.Add("especialidad_id", OracleDbType.Int32).Value = h.Especialidad_id;
            cmd.Parameters.Add("diagnostico", OracleDbType.Varchar2).Value = (object)h.Diagnostico ?? DBNull.Value;
            cmd.Parameters.Add("tratamiento", OracleDbType.Varchar2).Value = (object)h.Tratamiento ?? DBNull.Value;
            cmd.Parameters.Add("observaciones", OracleDbType.Varchar2).Value = (object)h.Observaciones ?? DBNull.Value;
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, HistoriaClinica h)
        {
            cmd.Parameters.Add("diagnostico", OracleDbType.Varchar2).Value = (object)h.Diagnostico ?? DBNull.Value;
            cmd.Parameters.Add("tratamiento", OracleDbType.Varchar2).Value = (object)h.Tratamiento ?? DBNull.Value;
            cmd.Parameters.Add("observaciones", OracleDbType.Varchar2).Value = (object)h.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("historia_id", OracleDbType.Int32).Value = h.Historia_id;
        }

        protected override object ObtenerValorId(HistoriaClinica historia)
        {
            return historia.Historia_id;
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return $"Historia #{reader["historia_id"]}";
        }
        public int ObtenerSiguienteId()
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT NVL(MAX(historia_id), 0) + 1 FROM {NombreTabla}";
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
        public List<HistoriaClinica> ObtenerPorPaciente(string documentoPaciente)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $@"SELECT h.* 
                                    FROM {NombreTabla} h
                                    INNER JOIN s_citas c ON h.cita_id = c.cita_id
                                    WHERE h.paciente_documentoid = :doc
                                    ORDER BY c.fecha DESC";
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
                throw new Exception("Error al obtener historias por paciente: " + ex.Message);
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
                    string query = $@"SELECT h.* 
                                    FROM {NombreTabla} h
                                    INNER JOIN s_citas c ON h.cita_id = c.cita_id
                                    WHERE h.paciente_documentoid = :doc 
                                    AND h.especialidad_id = :esp
                                    ORDER BY c.fecha DESC";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("doc", documentoPaciente));
                        cmd.Parameters.Add(new OracleParameter("esp", especialidadId));
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
        public List<HistoriaClinica> ObtenerPorDoctor(string documentoDoctor)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $@"SELECT h.* 
                                    FROM {NombreTabla} h
                                    INNER JOIN s_citas c ON h.cita_id = c.cita_id
                                    WHERE h.doctor_documentoid = :doc
                                    ORDER BY c.fecha DESC";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("doc", documentoDoctor));
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
        public bool ExistePorCita(int citaId)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT COUNT(*) FROM {NombreTabla} WHERE cita_id = :cita";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("cita", citaId));
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
        public HistoriaClinica ObtenerPorCita(int citaId)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} WHERE cita_id = :cita";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("cita", citaId));
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
    }
}
