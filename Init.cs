using Datos.Entidades;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace WebApiAzteca2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<E_Personal> lista = new List<E_Personal>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.GetAsync("api/Personal/Obtener").Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = respuesta.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<E_Personal>>(json);
                        return View("Index2", lista);

                    }
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index2", lista);
            }

            /*List<E_Nota> lista = new List<E_Nota>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.GetAsync("api/notas/Obtener").Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = respuesta.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<E_Nota>>(json);
                        return View("Index", lista);
                    }
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index", lista);
            }*/

        }
        public ActionResult IrAccesoPorPersonal(int id)
        {
            List<E_RegistroJoin> lista = new List<E_RegistroJoin>();

            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.GetAsync($"api/Registros/ObtenerPorPersonal?idPersonal={id}").Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = respuesta.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<E_RegistroJoin>>(json);
                        return View("RegistrosPorPersonal", lista);

                    }
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult IrAgregarPersonal()
        {
            return View("AgrPersonal");
        }

        public ActionResult AgregarPersonal(E_Personal personal)
        {

            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.PostAsJsonAsync("api/Personal/Agregar", personal).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Pedido agregado correctamente.";
                        return RedirectToAction("Index");

                    }
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("AgrPersonal");
            }
        }

        public ActionResult IrAgregarRegistro()
        {
            List<E_Acceso> listaAcceso = new List<E_Acceso>();
            List<E_Personal> listaPersonal = new List<E_Personal>();


            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");

                    var respuesta2 = cliente.GetAsync("api/Personal/Obtener").Result;

                    if (respuesta2.IsSuccessStatusCode)
                    {
                        string json = respuesta2.Content.ReadAsStringAsync().Result;
                        listaPersonal = JsonConvert.DeserializeObject<List<E_Personal>>(json);
                        ViewBag.IdPersonal = new SelectList(listaPersonal, "IdPersonal", "Nombre");
                        
                    }


                    var respuesta = cliente.GetAsync("api/Accesos/Obtener").Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = respuesta.Content.ReadAsStringAsync().Result;
                        listaAcceso = JsonConvert.DeserializeObject<List<E_Acceso>>(json);
                        ViewBag.IdAcceso = new SelectList(listaAcceso, "IdAcceso", "Nombre");
                        return View("AgregarRegistro");
                    }

                    
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult AgregarRegistro(E_Registro registro)
        {

            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.PostAsJsonAsync("api/Registros/Agregar", registro).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Registro agregado correctamente.";
                        return RedirectToAction("Index");

                    }
                    throw new Exception("Error al obtener la lista");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("IrAgregarRegistro");
            }
        }



        public ActionResult IrDesglose(int notaId)
        {
            List<E_Desglose> lista = new List<E_Desglose>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61715/");
                    var respuesta = cliente.GetAsync($"api/Desgloses/ObtenerPorId?notaId={notaId}").Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = respuesta.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<E_Desglose>>(json);
                        return View("Desglose", lista);
                    }
                    throw new Exception("Error al obtener la lista: " + respuesta.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Desglose", lista);
            }
        }
    }
}
