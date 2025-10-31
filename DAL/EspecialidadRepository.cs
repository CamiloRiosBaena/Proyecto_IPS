using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EspecialidadRepository : BaseConsultaRepository<Especialidad>, IPLSQLRepository<Especialidad>
    {
        protected override string NombreTabla
        {
            get { return "s_especialidades"; }
        }

        protected override string Id
        {
            get { return "especialidad_id"; }
        }

        protected override string Primer_Nombre
        {
            get { return "nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return reader["nombre"].ToString();
        }

        protected override Especialidad MapearDesdeReader(OracleDataReader reader)
        {
            return new Especialidad
            {
                Id = Convert.ToInt32(reader["especialidad_id"]),
                Nombre = reader["nombre"].ToString(),
            };
        }

        protected override object ObtenerValorId(Especialidad especialidad)
        {
            return especialidad.Id;
        }

        public bool Insertar(Especialidad especialidad)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_ESPECIALIDAD", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = especialidad.Id;
                        cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = especialidad.Nombre;

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
                throw new Exception("Error al insertar especialidad: " + ex.Message);
            }
        }

        public bool Actualizar(Especialidad especialidad)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_ESPECIALIDAD", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = especialidad.Id;
                        cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = especialidad.Nombre;

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
                throw new Exception("Error al actualizar especialidad: " + ex.Message);
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
                throw new Exception("Error al eliminar especialidad: " + ex.Message);
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
