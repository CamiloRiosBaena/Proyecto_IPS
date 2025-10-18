using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CredencialesRepository : ICrudCredencialesRepository
    {
        private static string connectionString = "User Id=usuario_ips;Password=usuario_ips123;Data Source=localhost:1521/XEPDB1";

        public bool VerificarCredenciales(string username, string password)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT COUNT(*) 
                                   FROM s_credenciales 
                                   WHERE nombre_usuario = :username 
                                   AND password = :password";

                    using (var command = new OracleCommand(query, connection))
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

        public bool InsertarUsuarioPaciente(Paciente paciente, string username, string password, string rol)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int usuarioId = ObtenerSiguienteId();
                        InsertarUsuario(connection, transaction, usuarioId, username, password, rol);

                        InsertarPaciente(connection, transaction, usuarioId, paciente);

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool InsertarUsuarioDoctor(Doctor doctor, string username, string password, string rol)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int usuarioId = ObtenerSiguienteId();
                        InsertarUsuario(connection, transaction, usuarioId, username, password, rol);

                        InsertarDoctor(connection, transaction, usuarioId, doctor);

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool InsertarUsuarioResponsable(Responsable responsable)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        InsertarResponsable(connection, transaction, responsable);

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void InsertarUsuario(OracleConnection connection, OracleTransaction transaction,
                               int usuarioId, string username, string password, string rol)
        {
            string query = @"INSERT INTO s_credenciales (usuario_id, nombre_usuario, password, tipo_usuario) 
                        VALUES (:id, :username, :password, :rol)";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.Transaction = transaction;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = usuarioId;
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = HashPassword(password);
                cmd.Parameters.Add("tipo_usuario", OracleDbType.Varchar2).Value = rol;
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarPaciente(OracleConnection connection, OracleTransaction transaction,
                   int usuarioId, Paciente paciente)
        {
            string query = @"INSERT INTO s_pacientes (documentoid, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido,
                     sexo, edad, correo, direccion, barrio, calle, ciudad_id, telefono, eps_id, tipo_sangre, rh, documento_responsable, usuario_id) 
                     VALUES (:documentoid, :primer_nombre, :segundo_nombre,:primer_apellido, :segundo_apellido, 
                     :sexo, :edad, :correo, :direccion, :barrio, :calle, :ciudad_id, :telefono, :eps_id, :tipo_sangre, :rh, 
                     :documento_responsable, :usuario_id)";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.Transaction = transaction;
                cmd.Parameters.Add("documentoid", OracleDbType.Varchar2).Value = paciente.DocumentoID;
                cmd.Parameters.Add("primer_nombre", OracleDbType.Varchar2).Value = paciente.Primer_Nombre;
                cmd.Parameters.Add("segundo_nombre", OracleDbType.Varchar2).Value = (object)paciente.Segundo_Nombre ?? DBNull.Value;
                cmd.Parameters.Add("primer_apellido", OracleDbType.Varchar2).Value = paciente.Primer_Apellido;
                cmd.Parameters.Add("segundo_apellido", OracleDbType.Varchar2).Value = (object)paciente.Segundo_Apellido ?? DBNull.Value;
                cmd.Parameters.Add("sexo", OracleDbType.Char).Value = paciente.Sexo;
                cmd.Parameters.Add("edad", OracleDbType.Int32).Value = paciente.Edad;
                cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = paciente.Correo;
                cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = paciente.Direccion;
                cmd.Parameters.Add("barrio", OracleDbType.Varchar2).Value = paciente.Barrio;
                cmd.Parameters.Add("calle", OracleDbType.Varchar2).Value = paciente.Calle;
                cmd.Parameters.Add("ciudad_id", OracleDbType.Int32).Value = paciente.Ciudad_id;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = paciente.Telefono;
                cmd.Parameters.Add("eps_id", OracleDbType.Varchar2).Value = paciente.EPS_id;
                cmd.Parameters.Add("tipo_sangre", OracleDbType.Varchar2).Value = paciente.Tipo_sangre;
                cmd.Parameters.Add("rh", OracleDbType.Char).Value=paciente.RH;
                cmd.Parameters.Add("documento_responsable", OracleDbType.Varchar2).Value = paciente.Documento_responsable;
                cmd.Parameters.Add("usuario_id", OracleDbType.Int32).Value = usuarioId;

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarDoctor(OracleConnection connection, OracleTransaction transaction,
                              int usuarioId, Doctor doctor)
        {
            string query = @"INSERT INTO admin_ips.doctores 
                             (documentoid, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, 
                             especialidad_id, telefono, correo, horaatencion, usuario_id, numero_licencia) 
                             VALUES (:documentoid, :primer_nombre, :segundo_nombre,:primer_apellido, :segundo_apellido, 
                             :especialidad_id, :telefono, :correo, :horaatencion, :usuario_id, :numero_licencia)";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.Transaction = transaction;
                cmd.Parameters.Add("documentoid", OracleDbType.Varchar2).Value = doctor.DocumentoID;
                cmd.Parameters.Add("primer_nombre", OracleDbType.Varchar2).Value = doctor.Primer_Nombre;
                cmd.Parameters.Add("segundo_nombre", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Nombre ?? DBNull.Value;
                cmd.Parameters.Add("primer_apellido", OracleDbType.Varchar2).Value = doctor.Primer_Apellido;
                cmd.Parameters.Add("segundo_apellido", OracleDbType.Varchar2).Value = (object)doctor.Segundo_Apellido ?? DBNull.Value;
                cmd.Parameters.Add("especialidad_id", OracleDbType.Int32).Value = doctor.Especialidad_id;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = doctor.Telefono;
                cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = doctor.Correo;
                //cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = doctor.Estado;
                cmd.Parameters.Add("horaatencion", OracleDbType.Varchar2).Value = (object)doctor.HoraAtencion ?? DBNull.Value;
                cmd.Parameters.Add("usuario_id", OracleDbType.Int32).Value = usuarioId;
                cmd.Parameters.Add("numero_licencia", OracleDbType.Varchar2).Value = doctor.NumeroLicencia;

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarResponsable(OracleConnection connection, OracleTransaction transaction, Responsable responsable)
        {
            string query = @"INSERT INTO s_responsables (documentoid, primer_nombre, segundo_nombre, 
                            primer_apellido, segundo_apellido, parentesco, telefono, correo, direccion, 
                            barrio, calle, ciudad_id, ocupacion) VALUES (:documentoid, :primer_nombre, :segundo_nombre, 
                            :primer_apellido, :segundo_apellido, :parentesco, :telefono, :correo, :direccion, :barrio, 
                            :calle, :ciudad_id, :ocupacion)";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.Transaction = transaction;
                cmd.Parameters.Add("documentoid", OracleDbType.Varchar2).Value = responsable.DocumentoID;
                cmd.Parameters.Add("primer_nombre", OracleDbType.Varchar2).Value = responsable.Primer_Nombre;
                cmd.Parameters.Add("segundo_nombre", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Nombre ?? DBNull.Value;
                cmd.Parameters.Add("primer_apellido", OracleDbType.Varchar2).Value = responsable.Primer_Apellido;
                cmd.Parameters.Add("segundo_apellido", OracleDbType.Varchar2).Value = (object)responsable.Segundo_Apellido ?? DBNull.Value;
                cmd.Parameters.Add("parentesco", OracleDbType.Varchar2).Value = responsable.Parentesco;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = responsable.Telefono;
                cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = responsable.Correo;
                cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = responsable.Direccion;
                cmd.Parameters.Add("barrio", OracleDbType.Varchar2).Value = responsable.Barrio;
                cmd.Parameters.Add("calle", OracleDbType.Varchar2).Value = responsable.Calle;
                cmd.Parameters.Add("ciudad_id", OracleDbType.Int32).Value = responsable.Ciudad_id;
                cmd.Parameters.Add("ocupacion", OracleDbType.Varchar2).Value = responsable.Ocupacion;
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteUsuario(string username)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM s_credenciales WHERE nombre_usuario = :username";

                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DAL al verificar usuario: {ex.Message}", ex);
            }
        }

        public int ObtenerSiguienteId()
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COALESCE(MAX(usuario_id), 0) + 1 FROM s_credenciales";

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

        public string ObtenerRolUsuario(string username)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT rol FROM s_credenciales WHERE nombre_usuario = :username";

                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DAL al obtener rol: {ex.Message}", ex);
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
