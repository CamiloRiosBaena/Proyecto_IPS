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
    public class ServicioCiudad
    {
        private CiudadRepository ciudadRepository;

        public ServicioCiudad()
        {
            ciudadRepository = new CiudadRepository();
        }

        public bool Insertar(Ciudad ciudad)
        {
            if (string.IsNullOrEmpty(ciudad.Nombre))
            {
                throw new Exception("El nombre de la ciudad es obligatorio");
            }

            if (string.IsNullOrEmpty(ciudad.Departamento))
            {
                throw new Exception("El departamento es obligatorio");
            }

            return ciudadRepository.Insertar(ciudad);
        }

        public bool Actualizar(Ciudad ciudad)
        {
            if (string.IsNullOrEmpty(ciudad.Id.ToString()))
            {
                throw new Exception("El ID de la ciudad es obligatorio");
            }

            if (!ciudadRepository.Existe(ciudad.Id.ToString()))
            {
                throw new Exception("La ciudad no existe");
            }

            return ciudadRepository.Actualizar(ciudad);
        }

        public bool Eliminar(string id)
        {
            if (!ciudadRepository.Existe(id))
            {
                throw new Exception("La ciudad no existe");
            }

            return ciudadRepository.Eliminar(id);
        }

        public Ciudad ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            return ciudadRepository.ObtenerPorId(id);
        }

        public List<Ciudad> ObtenerTodos()
        {
            return ciudadRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return ciudadRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return ciudadRepository.ObtenerParaCombo();
        }
    }
}
