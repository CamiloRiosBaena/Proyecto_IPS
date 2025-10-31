using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EPSRepository : BaseConsultaRepository<EPS>, IPLSQLRepository<EPS>
    {
        protected override string NombreTabla
        {
            get { return "s_EPS"; }
        }

        protected override string Id
        {
            get { return "eps_id"; }
        }

        protected override string Primer_Nombre
        {
            get { return "nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            string nombre = reader["nombre"].ToString();
            return nombre;
        }

        protected override EPS MapearDesdeReader(OracleDataReader reader)
        {
            return new EPS
            {
                Id = Convert.ToInt32(reader["eps_id"]),
                Nombre = reader["nombre"].ToString(),
                NIT = reader["nit"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Regimen = reader["tipo_regimen"].ToString()
            };
        }

        protected override object ObtenerValorId(EPS eps)
        {
            return eps.Id;
        }

        public bool Insertar(EPS eps)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_EPS", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = eps.Id;
                        cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = eps.Nombre;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = eps.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = (object)eps.Correo ?? DBNull.Value;

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
                throw new Exception("Error al insertar EPS: " + ex.Message);
            }
        }

        public bool Actualizar(EPS eps)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_EPS", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = eps.Id;
                        cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = eps.Nombre;
                        cmd.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = eps.Telefono;
                        cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = (object)eps.Correo ?? DBNull.Value;

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
                throw new Exception("Error al actualizar EPS: " + ex.Message);
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
                throw new Exception("Error al eliminar EPS: " + ex.Message);
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
