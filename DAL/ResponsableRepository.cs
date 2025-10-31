using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ResponsableRepository : BaseConsultaRepository<Responsable>, IPLSQLRepository<Responsable>
    {
        protected override string NombreTabla
        {
            get { return "s_responsables"; }
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
            string doc = reader["documentoid"].ToString();
            return nombre + " " + apellido + " - CC: " + doc;
        }

        protected override Responsable MapearDesdeReader(OracleDataReader reader)
        {
            return new Responsable
            {
                DocumentoID = reader["documentoid"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"] != DBNull.Value ? reader["segundo_nombre"].ToString() : null,
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"] != DBNull.Value ? reader["segundo_apellido"].ToString() : null,
                Parentesco = reader["parentesco"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Barrio = reader["barrio"].ToString(),
                Calle = reader["calle"].ToString(),
                Ciudad_id = Convert.ToInt32(reader["ciudad_id"]),
                Ocupacion = reader["ocupacion"].ToString()
            };
        }

        protected override object ObtenerValorId(Responsable r)
        {
            return r.DocumentoID;
        }

        public bool Insertar(Responsable responsable)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_RESPONSABLE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = responsable.DocumentoID;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = responsable.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = responsable.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = responsable.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = responsable.Correo;
                        cmd.Parameters.Add("p_parentesco", OracleDbType.Varchar2).Value = responsable.Parentesco;
                        cmd.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = responsable.Direccion;
                        cmd.Parameters.Add("p_barrio", OracleDbType.Varchar2).Value = responsable.Barrio;
                        cmd.Parameters.Add("p_calle", OracleDbType.Varchar2).Value = responsable.Calle;
                        cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = responsable.Ciudad_id;
                        cmd.Parameters.Add("p_ocupacion", OracleDbType.Varchar2).Value = responsable.Ocupacion;

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
                throw new Exception("Error al insertar responsable: " + ex.Message);
            }
        }

        public bool Actualizar(Responsable responsable)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_RESPONSABLE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_documento", OracleDbType.Varchar2).Value = responsable.DocumentoID;
                        cmd.Parameters.Add("p_primer_nombre", OracleDbType.Varchar2).Value = responsable.Primer_Nombre;
                        cmd.Parameters.Add("p_segundo_nombre", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Nombre ?? DBNull.Value;
                        cmd.Parameters.Add("p_primer_apellido", OracleDbType.Varchar2).Value = responsable.Primer_Apellido;
                        cmd.Parameters.Add("p_segundo_apellido", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Apellido ?? DBNull.Value;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = responsable.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = responsable.Correo;
                        cmd.Parameters.Add("p_parentesco", OracleDbType.Varchar2).Value = responsable.Parentesco;
                        cmd.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = responsable.Direccion;
                        cmd.Parameters.Add("p_barrio", OracleDbType.Varchar2).Value = responsable.Barrio;
                        cmd.Parameters.Add("p_calle", OracleDbType.Varchar2).Value = responsable.Calle;
                        cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = responsable.Ciudad_id;
                        cmd.Parameters.Add("p_ocupacion", OracleDbType.Varchar2).Value = responsable.Ocupacion;

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
                throw new Exception("Error al actualizar responsable: " + ex.Message);
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
                throw new Exception("Error al eliminar responsable: " + ex.Message);
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
    }
}
