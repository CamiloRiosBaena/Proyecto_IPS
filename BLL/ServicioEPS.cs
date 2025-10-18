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
    public class ServicioEPS
    {
        private EPSRepository epsRepository;

        public ServicioEPS()
        {
            epsRepository = new EPSRepository();
        }

        public bool Insertar(EPS eps)
        {
            if (string.IsNullOrEmpty(eps.Nombre))
            {
                throw new Exception("El nombre de la EPS es obligatorio");
            }

            if (string.IsNullOrEmpty(eps.NIT))
            {
                throw new Exception("El NIT es obligatorio");
            }

            return epsRepository.Insertar(eps);
        }

        public bool Actualizar(EPS eps)
        {
            if (string.IsNullOrEmpty(eps.Id.ToString()))
            {
                throw new Exception("El ID de la EPS es obligatorio");
            }

            if (!epsRepository.Existe(eps.Id.ToString()))
            {
                throw new Exception("La EPS no existe");
            }

            return epsRepository.Actualizar(eps);
        }

        public bool Eliminar(string id)
        {
            if (!epsRepository.Existe(id))
            {
                throw new Exception("La EPS no existe");
            }

            return epsRepository.Eliminar(id);
        }

        public EPS ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El ID es obligatorio");
            }

            return epsRepository.ObtenerPorId(id);
        }

        public List<EPS> ObtenerTodos()
        {
            return epsRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return epsRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return epsRepository.ObtenerParaCombo();
        }
    }
}
