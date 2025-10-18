using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConexionOracle
    {
        private static string connectionString = "User Id=admin_ips;Password=admin_ips123;Data Source=localhost:1521/XEPdb1;";

        public OracleConnection ObtenerConexion()
        {
            OracleConnection conexion = new OracleConnection(connectionString);

            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }
    }
}
