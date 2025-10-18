using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioEspecialidad
    {
        private EspecialidadRepository especialidadRespository;

        public ServicioEspecialidad()
        {
            especialidadRespository = new EspecialidadRepository();
        }

        public bool Insertar(Especialidad especialidad)
        {
            if (string.IsNullOrEmpty(especialidad.Nombre))
            {
                throw new Exception("El nombre de la especialidad es obligatorio");
            }

            return especialidadRespository.Insertar(especialidad);
        }

        public bool Actualizar(Especialidad especialidad)
        {
            if (string.IsNullOrEmpty(especialidad.Id.ToString()))
            {
                throw new Exception("El ID es obligatorio");
            }

            if (!especialidadRespository.Existe(especialidad.Id.ToString()))
            {
                throw new Exception("La especialidad no existe");
            }

            return especialidadRespository.Actualizar(especialidad);
        }

        public bool Eliminar(string id)
        {
            if (!especialidadRespository.Existe(id))
            {
                throw new Exception("La especialidad no existe");
            }

            return especialidadRespository.Eliminar(id);
        }

        public Especialidad ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            return especialidadRespository.ObtenerPorId(id);
        }

        public List<Especialidad> ObtenerTodos()
        {
            return especialidadRespository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return especialidadRespository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return especialidadRespository.ObtenerParaCombo();
        }
    }
}
