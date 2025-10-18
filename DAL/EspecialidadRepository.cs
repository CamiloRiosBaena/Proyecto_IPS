using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EspecialidadRepository : BaseDALRepository<Especialidad>
    {
        protected override string NombreTabla
        {
            get { return "s_especialidades"; }
        }

        protected override string Id
        {
            get { return "especialidad_id"; }
        }

        protected override string Primer_Nombre
        {
            get { return "nombre"; }
        }

        protected override string ObtenerTextoMostrar(OracleDataReader reader)
        {
            return reader["nombre"].ToString();
        }

        protected override Especialidad MapearDesdeReader(OracleDataReader reader)
        {
            return new Especialidad
            {
                Id = Convert.ToInt32(reader["especialidad_id"]),
                Nombre = reader["nombre"].ToString(),
            };
        }

        protected override string ObtenerQueryInsert()
        {
            return $@"INSERT INTO {NombreTabla} (especialidad_id, nombre) 
                     VALUES (:id, :nombre)";
        }

        protected override void AgregarParametrosInsert(OracleCommand cmd, Especialidad especialidad)
        {
            cmd.Parameters.Add(new OracleParameter("id", especialidad.Id));
            cmd.Parameters.Add(new OracleParameter("nombre", especialidad.Nombre));
        }

        protected override string ObtenerQueryUpdate()
        {
            return $@"UPDATE {NombreTabla} 
                     SET nombre = :nombre 
                     WHERE especialidad_id = :id";
        }

        protected override void AgregarParametrosUpdate(OracleCommand cmd, Especialidad especialidad)
        {
            AgregarParametrosInsert(cmd, especialidad);
        }

        protected override object ObtenerValorId(Especialidad especialidad)
        {
            return especialidad.Id;
        }
    }
}
