using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CiudadRepository : BaseDALRepository<Ciudad>
    {
        protected override string NombreTabla
        {
            get { return "Ciudades"; }
        }

        protected override string CampoId
        {
            get { return "ciudad_id"; }
        }

        protected override string CampoNombre
        {
            get { return "nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            string nombre = reader["nombre"].ToString();
            string departamento = reader["departamento"].ToString();
            return nombre + " - " + departamento;
        }

        protected override Ciudad MapearDesdeReader(OracleDataReader reader)
        {
            return new Ciudad
            {
                Id = Convert.ToInt32(reader["ciudad_id"]),
                Nombre = reader["nombre"].ToString(),
                Departamento = reader["departamento"].ToString(),
                Pais = reader["pais"].ToString()
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return @"INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
                     VALUES (:id, :nombre, :departamento, :pais)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Ciudad ciudad)
        {
            cmd.Parameters.Add(new OracleParameter("id", ciudad.Id));
            cmd.Parameters.Add(new OracleParameter("nombre", ciudad.Nombre));
            cmd.Parameters.Add(new OracleParameter("departamento", ciudad.Departamento));
            cmd.Parameters.Add(new OracleParameter("pais", ciudad.Pais));
        }

        protected override string ObtenerQueryUpdate()
        {
            return @"UPDATE Ciudades 
                     SET nombre = :nombre, departamento = :departamento, pais = :pais 
                     WHERE ciudad_id = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Ciudad ciudad)
        {
            cmd.Parameters.Add(new OracleParameter("nombre", ciudad.Nombre));
            cmd.Parameters.Add(new OracleParameter("departamento", ciudad.Departamento));
            cmd.Parameters.Add(new OracleParameter("pais", ciudad.Pais));
        }

        protected override object ObtenerValorId(Ciudad ciudad)
        {
            return ciudad.Id;
        }
    }
}
