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
    public class DoctorRepository : BaseConsultaRepository<Doctor>, IPLSQLRepository<Doctor>
    {
        protected override string NombreTabla
        {
            get { return "s_doctores"; }
        }

        protected override string Id
        {
            get { return "documentoid"; }
        }

        protected override string Primer_Nombre
        {
            get { return "primer_nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            string nombre = reader["primer_nombre"].ToString();
            string apellido = reader["primer_apellido"].ToString();
            return $"{nombre} {apellido}";
        }

        protected override Doctor MapearDesdeReader(OracleDataReader reader)
        {
            return new Doctor
            {
                DocumentoID = reader["documentoid"].ToString(),
                NumeroLicencia = reader["numero_licencia"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"] != DBNull.Value ? reader["segundo_nombre"].ToString() : null,
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"] != DBNull.Value ? reader["segundo_apellido"].ToString() : null,
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Especialidad_id = Convert.ToInt32(reader["especialidad_id"]),
                HoraAtencion = reader["horaatencion"] != DBNull.Value ? reader["horaatencion"].ToString() : null,
                Usuario_id = Convert.ToInt32(reader["usuario_id"])
            };
        }

        protected override object ObtenerValorId(Doctor d)
        {
            return d.DocumentoID;
        }

        public bool Insertar(Doctor doctor)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_DOCTOR", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = doctor.DocumentoID;
                        cmd.Parameters.Add("p_numero_licencia", OracleDbType.Varchar2).Value = doctor.NumeroLicencia;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = doctor.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = doctor.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = doctor.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = doctor.Correo;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = doctor.Especialidad_id;
                        cmd.Parameters.Add("p_hora_atencion", OracleDbType.Varchar2).Value = (object)doctor.HoraAtencion ?? DBNull.Value;
                        cmd.Parameters.Add("p_usuario_id", OracleDbType.Int32).Value = doctor.Usuario_id;

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
                throw new Exception("Error al insertar doctor: " + ex.Message);
            }
        }

        public bool Actualizar(Doctor doctor)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_DOCTOR", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = doctor.DocumentoID;
                        cmd.Parameters.Add("p_numero_licencia", OracleDbType.Varchar2).Value = doctor.NumeroLicencia;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = doctor.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = doctor.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = doctor.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = doctor.Correo;
                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = doctor.Especialidad_id;
                        cmd.Parameters.Add("p_hora_atencion", OracleDbType.Varchar2).Value = (object)doctor.HoraAtencion ?? DBNull.Value;

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
                throw new Exception("Error al actualizar doctor: " + ex.Message);
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
                throw new Exception("Error al eliminar doctor: " + ex.Message);
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

        public List<Doctor> ObtenerPorEspecialidad(int especialidadId)
        {
            List<Doctor> lista = new List<Doctor>();
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_OBTENER_DOCTORES_ESP", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_especialidad_id", OracleDbType.Int32).Value = especialidadId;

                        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                        cursorParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(cursorParam);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Doctor doctor = MapearDesdeReader(reader);
                                lista.Add(doctor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener doctores por especialidad: " + ex.Message);
            }
            return lista;
        }
    }
}
