using Datos.Entidades;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Datos.D_Registros;

namespace Datos
{
    public class D_Registros
    {
        private readonly EntradaSimpleEntities _db = new EntradaSimpleEntities();

        public List<E_RegistroJoin> ObtenerRegistros()
        {
            List<E_RegistroJoin> listaManual = new List<E_RegistroJoin>();
            List<spObtenerRegistros_Result> listaEntity = _db.spObtenerRegistros().ToList();

            foreach (spObtenerRegistros_Result resultadoEnt in listaEntity)
            {
                E_RegistroJoin registroManual = new E_RegistroJoin
                {
                    IdRegistro = resultadoEnt.IdRegistro,
                    Nombre = resultadoEnt.Nombre,
                    Acceso = resultadoEnt.Acceso,
                    Ubicacion = resultadoEnt.Ubicacion,
                    FechaHora = resultadoEnt.FechaHora,
                    TipoMovimiento = resultadoEnt.TipoMovimiento
                };
                listaManual.Add(registroManual);
            }
            return listaManual;
        }

        public List<E_RegistroJoin> ObtenerRegistrosPorPersonal(int idPersonal)
        {
            List<E_RegistroJoin> listaManual = new List<E_RegistroJoin>();
            List<spObtenerRegistrospPorPersonal_Result> listaEntity = _db.spObtenerRegistrospPorPersonal(idPersonal).ToList();

            foreach (spObtenerRegistrospPorPersonal_Result resultadoEnt in listaEntity)
            {
                E_RegistroJoin registroManual = new E_RegistroJoin
                {
                    IdRegistro = resultadoEnt.IdRegistro,
                    Nombre = resultadoEnt.Nombre,
                    Acceso = resultadoEnt.Acceso,
                    Ubicacion = resultadoEnt.Ubicacion,
                    FechaHora = resultadoEnt.FechaHora,
                    TipoMovimiento = resultadoEnt.TipoMovimiento
                };
                listaManual.Add(registroManual);
            }
            return listaManual;
        }

        public void AgregarRegistro(E_Registro registro)
        {
            _db.spAgregarRegistro(registro.IdPersonal, registro.IdAcceso, registro.FechaHora, registro.TipoMovimiento);
        }
    }
}

