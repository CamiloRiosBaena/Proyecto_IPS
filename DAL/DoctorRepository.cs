using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DoctorRepository : BaseDALRepository<Doctor>
    {
        protected override string NombreTabla
        {
            get { return "s_doctores"; }
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
            return nombre + " " + apellido;
        }

        protected override Doctor MapearDesdeReader(OracleDataReader reader)
        {
            return new Doctor
            {
                DocumentoID = reader["documentoid"].ToString(),
                NumeroLicencia = reader["numero_licencia"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"] != DBNull.Value ? reader["segundo_nombre"].ToString() : null,
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"] != DBNull.Value ? reader["segundo_apellido"].ToString() : null,
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Especialidad_id = Convert.ToInt32(reader["especialidad_id"]),
                HoraAtencion = reader["horaatencion"] != DBNull.Value ? reader["horaatencion"].ToString() : null,
                Usuario_id = Convert.ToInt32(reader["usuario_id"])
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla}
                             (documentoid, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, 
                             especialidad_id, telefono, correo, horaatencion, usuario_id, numero_licencia) 
                             VALUES (:documentoid, :primer_nombre, :segundo_nombre,:primer_apellido, :segundo_apellido, 
                             :especialidad_id, :telefono, :correo, :horaatencion, :usuario_id, :numero_licencia)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Doctor doctor)
        {
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
            cmd.Parameters.Add("usuario_id", OracleDbType.Int32).Value = doctor.Usuario_id;
            cmd.Parameters.Add("numero_licencia", OracleDbType.Varchar2).Value = doctor.NumeroLicencia;
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla} 
                     SET numero_licencia = :licencia, primer_nombre = :pnombre, segundo_nombre = :snombre,
                         primer_apellido = :papellido, segundo_apellido = :sapellido,
                         telefono = :tel, correo = :correo, especialidad_id = :especialidad,
                         horaatencion = :hora
                     WHERE documentoid = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Doctor d)
        {
            AgregarParametrosInsert(cmd, d);
        }

        protected override object ObtenerValorId(Doctor d)
        {
            return d.DocumentoID;
        }

        public List<Doctor> ObtenerPorEspecialidad(int especialidadId)
        {
            List<Doctor> lista = new List<Doctor>();

            try
            {
                using (OracleConnection conn = conexionOracle.ObtenerConexion())
                {
                    string query = $"SELECT * FROM {NombreTabla} WHERE especialidad_id = :esp ORDER BY primer_nombre";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("esp", especialidadId));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Doctor doctor = MapearDesdeReader(reader);
                                lista.Add(doctor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener doctores por especialidad: " + ex.Message);
            }

            return lista;
        }
    }

}
