using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseConsultaRepository<T>
    {
        protected abstract string NombreTabla { get; }
        protected abstract string Id { get; }
        protected abstract string Primer_Nombre { get; }

        protected abstract T MapearDesdeReader(OracleDataReader reader);
        protected abstract string ObtenerTextoMostrar(OracleDataReader reader);
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
                throw new Exception($"Error al obtener {NombreTabla} para combo: {ex.Message}");
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
                throw new Exception($"Error al obtener {NombreTabla}: {ex.Message}");
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
                                Console.WriteLine(entidad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener {NombreTabla} por ID: {ex.Message}");
            }

            return entidad;
        }
    }
}
