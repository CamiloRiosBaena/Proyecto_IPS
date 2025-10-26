using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CredencialesRepository : BaseDALRepository<Credenciales>
    {
        protected override string NombreTabla
        {
            get { return "s_credenciales"; }
        }

        protected override string Id
        {
            get { return "usuario_id"; }
        }

        protected override string Primer_Nombre   
        {
            get { return "nombre_usuario"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            string usuario = reader["nombre_usuario"].ToString();
            string tipo = reader["tipo_usuario"].ToString();
            return usuario + " (" + tipo + ")";
        }

        protected override Credenciales MapearDesdeReader(OracleDataReader reader)
        {
            return new Credenciales
            {
                Id = Convert.ToInt32(reader["usuario_id"]),
                Nombre_usuario = reader["nombre_usuario"].ToString(),
                Password = reader["password"].ToString(),
                Tipo_usuario = reader["tipo_usuario"].ToString(),
                Estado = reader["estado"] != DBNull.Value ? reader["estado"].ToString() : "Activo",
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla} (usuario_id, nombre_usuario, password, tipo_usuario) 
                        VALUES (:id, :username, :password, :rol)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Credenciales credenciales)
        {
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = credenciales.Id;
            cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = credenciales.Nombre_usuario;
            cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = credenciales.Password;
            cmd.Parameters.Add("tipo_usuario", OracleDbType.Varchar2).Value = credenciales.Tipo_usuario;
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla}
                     SET nombre_usuario = :username, password = :password, 
                     tipo_usuario = :tipo, estado = :estado
                     WHERE usuario_id = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Credenciales credenciales)
        {
            AgregarParametrosInsert(cmd, credenciales);
        }

        protected override object ObtenerValorId(Credenciales credenciales)
        {
            return credenciales.Id;
        }
        public bool VerificarCredenciales(string username, string password)
        {
            try
            {
                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    string query = $@"SELECT COUNT(*) 
                                   FROM {NombreTabla} 
                                   WHERE nombre_usuario = :username 
                                   AND password = :password
                                   AND estado = 'Activo'";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        command.Parameters.Add("password", OracleDbType.Varchar2).Value = HashPassword(password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar credenciales: {ex.Message}");
            }
        }
        public bool ExisteUsuario(string username)
        {
            try
            {
                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT COUNT(*) FROM {NombreTabla} WHERE nombre_usuario = :username";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar usuario: {ex.Message}", ex);
            }
        }
        public int ObtenerSiguienteId()
        {
            try
            {
                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = $"SELECT COALESCE(MAX(usuario_id), 0) + 1 FROM {NombreTabla}";

                    using (var command = new OracleCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DAL al obtener siguiente ID: {ex.Message}", ex);
            }
        }
        public string ObtenerTipoUsuario(string username)
        {
            try
            {
                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT tipo_usuario FROM {NombreTabla} WHERE nombre_usuario = :username";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        object result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipo de usuario: {ex.Message}", ex);
            }
        }
        public Credenciales ObtenerPorNombreUsuario(string username)
        {
            Credenciales credencial = null;

            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} WHERE nombre_usuario = :username";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("username", username));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                credencial = MapearDesdeReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener credenciales: {ex.Message}");
            }

            return credencial;
        }
        public bool CambiarEstado(string username, string nuevoEstado)
        {
            try
            {
                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    string query = $@"UPDATE {NombreTabla} 
                                   SET estado = :estado 
                                   WHERE nombre_usuario = :username";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("estado", OracleDbType.Varchar2).Value = nuevoEstado;
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar estado: {ex.Message}");
            }
        }
        public bool CambiarPassword(string username, string passwordActual, string passwordNueva)
        {
            try
            {
                if (!VerificarCredenciales(username, passwordActual))
                {
                    throw new Exception("La contraseña actual es incorrecta");
                }

                using (OracleConnection connection = conexionOracle.ObtenerConexion())
                {
                    string query = $@"UPDATE {NombreTabla} 
                                   SET password = :nuevaPass 
                                   WHERE nombre_usuario = :username";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("nuevaPass", OracleDbType.Varchar2).Value = HashPassword(passwordNueva);
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar contraseña: {ex.Message}");
            }
        }
        public bool InsertarConHash(Credenciales credencial, string passwordSinHash)
        {
            try
            {
                if (ExisteUsuario(credencial.Nombre_usuario))
                {
                    throw new Exception("El nombre de usuario ya existe");
                }

                Credenciales credencialHash = new Credenciales
                {
                    Id = credencial.Id,
                    Nombre_usuario = credencial.Nombre_usuario,
                    Password = HashPassword(passwordSinHash),
                    Tipo_usuario = credencial.Tipo_usuario,
                    Estado = credencial.Estado ?? "Activo",
                };

                return Insertar(credencialHash);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar credenciales: {ex.Message}");
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
