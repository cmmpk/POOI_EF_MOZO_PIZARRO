using POOI_EF_MOZO_PIZARRO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_EF_MOZO_PIZARRO.Controllers
{
    public class ConsumidorController : Controller
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;
        IEnumerable<Consumidor> ObtenerConsumidor()
        {
            List<Consumidor> listaConsumidores = new List<Consumidor>();
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_listar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                listaConsumidores.Add(
                    new Consumidor
                    {
                        DniCont = sqlDataReader.GetString(0),
                        NomCont = sqlDataReader.GetString(1),
                        ApeCont = sqlDataReader.GetString(2),
                        EmailCont = sqlDataReader.GetString(3),
                        IdPais = sqlDataReader.GetString(4),
                    });
            }
            return listaConsumidores;
        }

        string agregarConsumidor(Consumidor consumidor)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_agrega_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Dni", consumidor.DniCont);
            sqlCommand.Parameters.AddWithValue("@Nombre", consumidor.NomCont);
            sqlCommand.Parameters.AddWithValue("@Apellido", consumidor.ApeCont);
            sqlCommand.Parameters.AddWithValue("@Email", consumidor.EmailCont);
            sqlCommand.Parameters.AddWithValue("@IdPais", consumidor.IdPais);
            int cantidadFilas = sqlCommand.ExecuteNonQuery();
            return cantidadFilas > 0 ? "Dato guardado" : "Error";
        }

        Consumidor obtenerConsumidorPorDni(string id)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_obtener_consumidor_dni", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Dni", id);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                return new Consumidor
                {
                    DniCont = sqlDataReader.GetString(0),
                    NomCont = sqlDataReader.GetString(1),
                    ApeCont = sqlDataReader.GetString(2),
                    EmailCont = sqlDataReader.GetString(3),
                    IdPais = sqlDataReader.GetString(4)
                };
            }
            ;
            return null;
        }
        string actualizarConsumidor(Consumidor consumidor)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_actualizar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Dni", consumidor.DniCont);
            sqlCommand.Parameters.AddWithValue("@Nombre", consumidor.NomCont);
            sqlCommand.Parameters.AddWithValue("@Apellido", consumidor.ApeCont);
            sqlCommand.Parameters.AddWithValue("@Email", consumidor.EmailCont);
            sqlCommand.Parameters.AddWithValue("@IdPais", consumidor.IdPais);

            int cantidadFilas = sqlCommand.ExecuteNonQuery();
            return cantidadFilas > 0 ? "Dato actualizado" : "Error al actualizar";
        }
        int eliminarConsumidor(string id)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_eliminar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Dni", id);

            return sqlCommand.ExecuteNonQuery();
        }
        public ActionResult Index()
        {
            return View(ObtenerConsumidor());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Consumidor consumidor)
        {
            ViewBag.Mensaje = agregarConsumidor(consumidor);
            return View(consumidor);
        }
        public ActionResult Details(string id)
        {
            Consumidor consumidorEncontrado = obtenerConsumidorPorDni(id);
            return View(consumidorEncontrado);
        }
        public ActionResult Edit(string id)
        {
            Consumidor consumidor = obtenerConsumidorPorDni(id);
            return View(consumidor);
        }
        [HttpPost]
        public ActionResult Edit(Consumidor consumidor)
        {
            ViewBag.Mensaje = actualizarConsumidor(consumidor);
            return View(consumidor);
        }
        public ActionResult Delete(string id)
        {
            Consumidor consumidorEncontrado = obtenerConsumidorPorDni(id);
            return View(consumidorEncontrado);
        }

        [HttpPost]
        public ActionResult Delete(Consumidor consumidor)
        {
            int cantidadCambios = eliminarConsumidor(consumidor.DniCont);
            if (cantidadCambios > 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}