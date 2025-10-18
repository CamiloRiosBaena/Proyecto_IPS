using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDALRepository<T>
    {
        protected abstract string NombreTabla { get; }
        protected abstract string Id { get; }
        protected abstract string Primer_Nombre { get; }

        protected abstract string ObtenerTextoMostrar(OracleDataReader reader);
        protected abstract T MapearDesdeReader(OracleDataReader reader);
        protected abstract void AgregarParametrosInsert(OracleCommand cmd, T entidad);
        protected abstract void AgregarParametrosUpdate(OracleCommand cmd, T entidad);
        protected abstract string ObtenerQueryInsert();
        protected abstract string ObtenerQueryUpdate();
        protected abstract object ObtenerValorId(T entidad);

        public ConexionOracle conexionOracle = new ConexionOracle();

        public DataTable ObtenerParaCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Texto", typeof(string));

            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} ORDER BY {Primer_Nombre}";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader[Id].ToString();
                                string texto = ObtenerTextoMostrar(reader);
                                dt.Rows.Add(id, texto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener {NombreTabla} para combo: ");
            }

            return dt;
        }
        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();

            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} ORDER BY {Primer_Nombre}";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T entidad = MapearDesdeReader(reader);
                                lista.Add(entidad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener {NombreTabla}: " + ex.Message);
            }

            return lista;
        }
        public T ObtenerPorId(string id)
        {
            T entidad = default(T);

            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} WHERE {Id} = :id";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entidad = MapearDesdeReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener {NombreTabla} por ID: ");
            }

            return entidad;
        }
        public bool Insertar(T entidad)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = ObtenerQueryInsert();

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        AgregarParametrosInsert(cmd, entidad);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar en {NombreTabla}: " + ex.Message);
            }
        }
        public bool Actualizar(T entidad)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = ObtenerQueryUpdate();

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        AgregarParametrosUpdate(cmd, entidad);
                        cmd.Parameters.Add(new OracleParameter("id", ObtenerValorId(entidad)));
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar {NombreTabla}: " + ex.Message);
            }
        }
        public bool Eliminar(string id)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"DELETE FROM {NombreTabla} WHERE {Id} = :id";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar de {NombreTabla}: ");
            }
        }
        public bool Existe(string id)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT COUNT(*) FROM {NombreTabla} WHERE {Id} = :id";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia en {NombreTabla}: ");
            }
        }
    }
}
