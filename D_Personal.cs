using Datos.Entidades;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Personal
    {
        private readonly EntradaSimpleEntities _db = new EntradaSimpleEntities();

        public List<E_Personal> obtenerPersonal()
        {
            List<E_Personal> listaManual = new List<E_Personal>();

            List<spObtenerPersonal_Result> listaEntity = _db.spObtenerPersonal().ToList();

            foreach (spObtenerPersonal_Result personalEnt in listaEntity)
            {
                E_Personal personalMan = new E_Personal
                {
                    IdPersonal = personalEnt.IdPersonal,
                    Nombre = personalEnt.Nombre,
                    Departamento = personalEnt.Departamento
                };

                listaManual.Add(personalMan);
            }
            return listaManual;
        }

        public void AgregarPersonal(E_Personal personal)
        {
            _db.spAgregarPersonal(personal.Nombre, personal.Departamento);
        }
    }
}
