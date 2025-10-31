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
    public class CredencialesRepository : BaseConsultaRepository<Credenciales>, IPLSQLRepository<Credenciales>
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
            return $"{usuario} ({tipo})";
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

        protected override object ObtenerValorId(Credenciales credenciales)
        {
            return credenciales.Id;
        }

        public bool Insertar(Credenciales credenciales)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_INSERTAR_CREDENCIALES", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_usuario_id", OracleDbType.Int32).Value = credenciales.Id;
                        cmd.Parameters.Add("p_nombre_usuario", OracleDbType.Varchar2).Value = credenciales.Nombre_usuario;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = credenciales.Password;
                        cmd.Parameters.Add("p_tipo_usuario", OracleDbType.Varchar2).Value = credenciales.Tipo_usuario;

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
                throw new Exception("Error al insertar credenciales: " + ex.Message);
            }
        }

        public bool Actualizar(Credenciales credenciales)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_ACTUALIZAR_CREDENCIALES", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_usuario_id", OracleDbType.Int32).Value = credenciales.Id;
                        cmd.Parameters.Add("p_nombre_usuario", OracleDbType.Varchar2).Value = credenciales.Nombre_usuario;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = credenciales.Password;
                        cmd.Parameters.Add("p_tipo_usuario", OracleDbType.Varchar2).Value = credenciales.Tipo_usuario;
                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = (object)credenciales.Estado ?? DBNull.Value;

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
                throw new Exception("Error al actualizar credenciales: " + ex.Message);
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
                throw new Exception("Error al eliminar credenciales: " + ex.Message);
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

        public bool VerificarCredenciales(string username, string password)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_VERIFICAR_CREDENCIALES(:p_username, :p_password) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = HashPassword(password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar credenciales: " + ex.Message);
            }
        }

        public bool ExisteUsuario(string username)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_EXISTE_USUARIO(:p_username) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar usuario: " + ex.Message);
            }
        }

        public int ObtenerSiguienteId()
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT COALESCE(MAX(usuario_id), 0) + 1 FROM {NombreTabla}";
                    using (var cmd = new OracleCommand(query, conn))
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

        public string ObtenerTipoUsuario(string username)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = "SELECT FN_OBTENER_TIPO_USUARIO(:p_username) FROM DUAL";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tipo de usuario: " + ex.Message);
            }
        }

        public Credenciales ObtenerPorNombreUsuario(string username)
        {
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
                                return MapearDesdeReader(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener credenciales: " + ex.Message);
            }
        }

        public bool CambiarEstado(string username, string nuevoEstado)
        {
            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_CAMBIAR_ESTADO_USUARIO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = nuevoEstado;

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
                throw new Exception("Error al cambiar estado: " + ex.Message);
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

                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_CAMBIAR_PASSWORD", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add("p_nueva_password", OracleDbType.Varchar2).Value = HashPassword(passwordNueva);

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
                throw new Exception("Error al cambiar contraseña: " + ex.Message);
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
                throw new Exception("Error al insertar credenciales: " + ex.Message);
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
