using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ResponsableRepository : BaseDALRepository<Responsable>
    {
        protected override string NombreTabla
        {
            get { return "s_responsables"; }
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

        protected override Responsable MapearDesdeReader(OracleDataReader reader)
        {
            return new Responsable
            {
                DocumentoID = reader["documentoid"].ToString(),
                Primer_Nombre = reader["primer_nombre"].ToString(),
                Segundo_Nombre = reader["segundo_nombre"] != DBNull.Value ? reader["segundo_nombre"].ToString() : null,
                Primer_Apellido = reader["primer_apellido"].ToString(),
                Segundo_Apellido = reader["segundo_apellido"] != DBNull.Value ? reader["segundo_apellido"].ToString() : null,
                Parentesco = reader["parentesco"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Barrio = reader["barrio"].ToString(),
                Calle = reader["calle"].ToString(),
                Ciudad_id = Convert.ToInt32(reader["ciudad_id"]),
                Ocupacion = reader["ocupacion"].ToString()
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla} (documentoid, primer_nombre, segundo_nombre, 
                            primer_apellido, segundo_apellido, parentesco, telefono, correo, direccion, 
                            barrio, calle, ciudad_id, ocupacion) VALUES (:documentoid, :primer_nombre, :segundo_nombre, 
                            :primer_apellido, :segundo_apellido, :parentesco, :telefono, :correo, :direccion, :barrio, 
                            :calle, :ciudad_id, :ocupacion)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Responsable responsable)
        {
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
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla}
                     SET primer_nombre = :pnombre, segundo_nombre = :snombre,
                         primer_apellido = :papellido, segundo_apellido = :sapellido,
                         parentesco = :parentesco, telefono = :tel, correo = :correo,
                         direccion = :dir, barrio = :barrio, calle = :calle,
                         ciudad_id = :ciudad, ocupacion = :ocupacion
                     WHERE documentoid = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Responsable r)
        {
            AgregarParametrosInsert(cmd, r);
        }

        protected override object ObtenerValorId(Responsable r)
        {
            return r.DocumentoID;
        }
    }
}
