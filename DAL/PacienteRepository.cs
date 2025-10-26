using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PacienteRepository : BaseDALRepository<Paciente>
    {
        protected override string NombreTabla
        {
            get { return "s_pacientes"; }
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

        protected override Paciente MapearDesdeReader(OracleDataReader reader)
        {
            return new Paciente
            {
                DocumentoID = reader["documentoid"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"].ToString(),
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Barrio = reader["barrio"].ToString(),
                Calle = reader["calle"].ToString(),
                Ciudad_id = Convert.ToInt32(reader["ciudad_id"]),
                Edad = Convert.ToInt32(reader["edad"]),
                Sexo = Convert.ToChar(reader["sexo"]),
                EPS_id = Convert.ToInt32(reader["eps_id"]),
                Tipo_sangre = reader["tipo_sangre"].ToString(),
                RH = Convert.ToChar(reader["rh"]),
                Documento_responsable = reader["documento_responsable"].ToString(),
                Usuario_id = Convert.ToInt32(reader["usuario_id"])
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla} (documentoid, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido,
                     sexo, edad, correo, direccion, barrio, calle, ciudad_id, telefono, eps_id, tipo_sangre, rh, documento_responsable, usuario_id) 
                     VALUES (:documentoid, :primer_nombre, :segundo_nombre,:primer_apellido, :segundo_apellido, 
                     :sexo, :edad, :correo, :direccion, :barrio, :calle, :ciudad_id, :telefono, :eps_id, :tipo_sangre, :rh, 
                     :documento_responsable, :usuario_id)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Paciente paciente)
        {
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
            cmd.Parameters.Add("rh", OracleDbType.Char).Value = paciente.RH;
            cmd.Parameters.Add("documento_responsable", OracleDbType.Varchar2).Value = paciente.Documento_responsable;
            cmd.Parameters.Add("usuario_id", OracleDbType.Int32).Value = paciente.Usuario_id;
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla} 
                     SET primer_nombre = :pnombre, segundo_nombre = :snombre, 
                         primer_apellido = :papellido, segundo_apellido = :sapellido,
                         telefono = :tel, correo = :correo, direccion = :dir, 
                         barrio = :barrio, calle = :calle, ciudad_id = :ciudad, 
                         edad = :edad, sexo = :sexo, eps_id = :eps, tipo_sangre = :tipo_sangre, 
                         rh = :rh, responsable_documentoid = :resp
                     WHERE documentoid = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Paciente p)
        {
            AgregarParametrosInsert(cmd, p);
        }

        protected override object ObtenerValorId(Paciente p)
        {
            return p.DocumentoID;
        }
    }
}

