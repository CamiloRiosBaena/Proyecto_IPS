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
    public class ServicioResponsable : ICrud<Responsable>
    {
        private ResponsableRepository responsableRepository;

        public ServicioResponsable()
        {
            responsableRepository = new ResponsableRepository();
        }

        public bool Insertar(Responsable responsable)
        {
            if (string.IsNullOrEmpty(responsable.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (responsableRepository.Existe(responsable.DocumentoID))
            {
                throw new Exception("Ya existe un responsable con ese documento");
            }

            if (string.IsNullOrEmpty(responsable.Primer_Nombre))
            {
                throw new Exception("El primer nombre es obligatorio");
            }

            if (string.IsNullOrEmpty(responsable.Primer_Apellido))
            {
                throw new Exception("El primer apellido es obligatorio");
            }

            if (string.IsNullOrEmpty(responsable.Parentesco))
            {
                throw new Exception("El parentesco es obligatorio");
            }

            return responsableRepository.Insertar(responsable);
        }

        public bool Actualizar(Responsable responsable)
        {
            if (string.IsNullOrEmpty(responsable.DocumentoID))
            {
                throw new Exception("El documento es obligatorio");
            }

            if (!responsableRepository.Existe(responsable.DocumentoID))
            {
                throw new Exception("El responsable no existe");
            }

            return responsableRepository.Actualizar(responsable);
        }

        public bool Eliminar(string id)
        {
            if (!responsableRepository.Existe(id))
            {
                throw new Exception("El responsable no existe");
            }

            return responsableRepository.Eliminar(id);
        }

        public Responsable ObtenerPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El documento es obligatorio");
            }

            return responsableRepository.ObtenerPorId(id);
        }

        public List<Responsable> ObtenerTodos()
        {
            return responsableRepository.ObtenerTodos();
        }

        public bool Existe(string id)
        {
            return responsableRepository.Existe(id);
        }

        public DataTable ObtenerParaCombo()
        {
            return responsableRepository.ObtenerParaCombo();
        }
    }
}
