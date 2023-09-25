using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MarcaController : Controller
    {
        public ActionResult Index()
        {
            using (var conexao = new Conexao())
            {
                string strMarca = "SELECT * FROM marca;";
                using (var comando = new MySqlCommand(strMarca, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                    {
                        var lstMarca = new List<Marca>();

                        while (dr.Read())
                        {
                            var marca = new Marca
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"]),
                               
                            };

                            lstMarca.Add(marca);
                        }
                        return View(lstMarca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }

        }


        public ActionResult Menu()
        {
            return View();
        }


        public ActionResult NovoMarca()
        {

            return View();
        }

        public ActionResult Edit(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strMarcas = "SELECT * FROM marca " +
                                  "WHERE Id = @Id;";

                using (var comando = new MySqlCommand(strMarcas, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["nome"]),
                         
                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        public ActionResult Visualizar(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strMarcas = "SELECT * FROM marca " +
                                  "WHERE Id = @Id;";

                using (var comando = new MySqlCommand(strMarcas, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["nome"]),
                         
                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        public ActionResult Excluir(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "SELECT * FROM marca " +
                                  "WHERE Id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["nome"]),
                           
                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Excluir(Marca marca)
        {
            // DELETE DE FATO

            using (var conexao = new Conexao())
            {
                string strMarcas = "DELETE FROM marca " +
                                    "where id = @Id;";

                using (var comando = new MySqlCommand(strMarcas, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@id", marca.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
            }

            // SOFT DELETE


           /* using (var conexao = new Conexao())
            {
                string strLogin = "UPDATE usuarios SET isExcluido = true " +
                                    "where id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@id", usuario.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
            }*/


        }

        [HttpPost]
        public ActionResult SalvarAlteracoes(Marca marca)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "UPDATE marca SET " +
                                    
                                    "nome = @nome, " +
                                   
                                    "where id = @Id;";


                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    
                    comando.Parameters.AddWithValue("@nome", marca.Nome);                  
                    comando.Parameters.AddWithValue("@id", marca.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public ActionResult SalvarMarca(Marca marca)
        {
            using (var conexao = new Conexao())
            {
                string strMarcas = "INSERT INTO marca ( nome) " +
                                  "values (" +
                                  " @nome);";

                using (var comando = new MySqlCommand(strMarcas, conexao.conn))
                {
                  
                    comando.Parameters.AddWithValue("@nome", marca.Nome);                  
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
            }
        }


       
    }
}