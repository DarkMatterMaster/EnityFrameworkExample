using Datos.Entidades;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Acceso
    {
        private readonly EntradaSimpleEntities _db = new EntradaSimpleEntities();

        public List<E_Acceso> ObtenerAccesos()
        {
            List<E_Acceso> listaManual = new List<E_Acceso>();
            List<spObtenerAccesos_Result> listaEntity = _db.spObtenerAccesos().ToList();
            foreach (spObtenerAccesos_Result accesoEntity in listaEntity)
            {
                E_Acceso accesoManual = new E_Acceso
                {
                    IdAcceso = accesoEntity.IdAcceso,
                    Nombre = accesoEntity.Nombre,
                    Ubicacion = accesoEntity.Ubicacion
                };
                listaManual.Add(accesoManual);
            }
            return listaManual;
        }
    }
}
