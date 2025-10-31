using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PacienteRepository : BaseConsultaRepository<Paciente>, IPLSQLRepository<Paciente>
    {
        protected override string NombreTabla
        {
            get { return "s_pacientes"; }
        }

        protected override string Id
        {
            get { return "documentoid"; }
        }

        protected override string Primer_Nombre
        {
            get { return "primer_nombre"; }
        }

        protected override Paciente MapearDesdeReader(OracleDataReader reader)
        {
            return new Paciente
            {
                DocumentoID = reader["documentoid"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"].ToString(),
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Barrio = reader["barrio"].ToString(),
                Calle = reader["calle"].ToString(),
                Ciudad_id = Convert.ToInt32(reader["ciudad_id"]),
                Edad = Convert.ToInt32(reader["edad"]),
                Sexo = Convert.ToChar(reader["sexo"]),
                EPS_id = Convert.ToInt32(reader["eps_id"]),
                Tipo_sangre = reader["tipo_sangre"].ToString(),
                RH = Convert.ToChar(reader["rh"]),
                Documento_responsable = reader["documento_responsable"].ToString(),
                Usuario_id = Convert.ToInt32(reader["usuario_id"])
            };
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return $"{reader["primer_nombre"]} {reader["primer_apellido"]} - {reader["documentoid"]}";
        }

        public bool Insertar(Paciente p)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_PACIENTE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = p.DocumentoID;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = p.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)p.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = p.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)p.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = p.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = p.Correo;
                        cmd.Parameters.Add("p_edad", OracleDbType.Int32).Value = p.Edad;
                        cmd.Parameters.Add("p_sexo", OracleDbType.Char).Value = p.Sexo.ToString();
                        cmd.Parameters.Add("p_tipo_sangre", OracleDbType.Varchar2).Value = p.Tipo_sangre;
                        cmd.Parameters.Add("p_rh", OracleDbType.Char).Value = p.RH.ToString();
                        cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = p.Ciudad_id;
                        cmd.Parameters.Add("p_eps_id", OracleDbType.Int32).Value = p.EPS_id;
                        cmd.Parameters.Add("p_documento_responsable", OracleDbType.Varchar2).Value = (object)p.Documento_responsable ?? DBNull.Value;
                        cmd.Parameters.Add("p_usuario_id", OracleDbType.Int32).Value = p.Usuario_id;

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
                throw new Exception("Error al insertar paciente: " + ex.Message);
            }
        }

        public bool Actualizar(Paciente p)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_PACIENTE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = p.DocumentoID;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = p.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)p.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = p.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)p.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = p.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = p.Correo;
                        cmd.Parameters.Add("p_edad", OracleDbType.Int32).Value = p.Edad;
                        cmd.Parameters.Add("p_sexo", OracleDbType.Char).Value = p.Sexo.ToString();
                        cmd.Parameters.Add("p_tipo_sangre", OracleDbType.Varchar2).Value = p.Tipo_sangre;
                        cmd.Parameters.Add("p_rh", OracleDbType.Char).Value = p.RH.ToString();
                        cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = p.Ciudad_id;
                        cmd.Parameters.Add("p_eps_id", OracleDbType.Int32).Value = p.EPS_id;
                        cmd.Parameters.Add("p_documento_responsable", OracleDbType.Varchar2).Value = (object)p.Documento_responsable ?? DBNull.Value;

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
                throw new Exception("Error al actualizar paciente: " + ex.Message);
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
                throw new Exception("Error al eliminar paciente: " + ex.Message);
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

        protected override object ObtenerValorId(Paciente p)
        {
            return p.DocumentoID;
        }
    }
}

