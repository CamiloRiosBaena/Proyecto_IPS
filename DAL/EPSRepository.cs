using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EPSRepository : BaseDALRepository<EPS>
    {
        protected override string NombreTabla
        {
            get { return "EPS"; }
        }

        protected override string CampoId
        {
            get { return "eps_id"; }
        }

        protected override string CampoNombre
        {
            get { return "nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            string nombre = reader["nombre"].ToString();
            string regimen = reader["tipo_regimen"].ToString();
            return nombre + " (" + regimen + ")";
        }

        protected override EPS MapearDesdeReader(OracleDataReader reader)
        {
            return new EPS
            {
                Id = Convert.ToInt32(reader["eps_id"]),
                Nombre = reader["nombre"].ToString(),
                NIT = reader["nit"].ToString(),
                Telefono = reader["telefono"].ToString(),
                Correo = reader["correo"].ToString(),
                Direccion = reader["direccion"].ToString(),
                Regimen = reader["tipo_regimen"].ToString()
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return @"INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen) 
                     VALUES (:id, :nombre, :nit, :tel, :correo, :dir, :regimen)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, EPS eps)
        {
            cmd.Parameters.Add(new OracleParameter("id", eps.Id));
            cmd.Parameters.Add(new OracleParameter("nombre", eps.Nombre));
            cmd.Parameters.Add(new OracleParameter("nit", eps.NIT));
            cmd.Parameters.Add(new OracleParameter("tel", eps.Telefono));
            cmd.Parameters.Add(new OracleParameter("correo", eps.Correo));
            cmd.Parameters.Add(new OracleParameter("dir", eps.Direccion));
            cmd.Parameters.Add(new OracleParameter("regimen", eps.Regimen));
        }

        protected override string ObtenerQueryUpdate()
        {
            return @"UPDATE EPS 
                     SET nombre = :nombre, nit = :nit, telefono = :tel, 
                         correo = :correo, direccion = :dir, tipo_regimen = :regimen 
                     WHERE eps_id = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, EPS eps)
        {
            AgregarParametrosInsert(cmd, eps);
        }

        protected override object ObtenerValorId(EPS eps)
        {
            return eps.Id;
        }
    }
}
