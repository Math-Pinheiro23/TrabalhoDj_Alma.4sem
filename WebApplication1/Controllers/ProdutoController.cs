using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto

        public ActionResult Index(Produto pro)
        {
            var lstMarca = new List<Marca>();
            using (var conexao = new Conexao())
            {
                string strMarca = "SELECT * FROM marca ;";
                using (var comando = new MySqlCommand(strMarca, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var marca = new Marca
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };

                            lstMarca.Add(marca);
                        }
                    ViewBag.ListaMarca = lstMarca;
                }
            }

            using (var conexao = new Conexao())
            {

                string strClientes = "SELECT * FROM produto " +
                "WHERE nome like @nome " +
                ";";

                using (var comando = new MySqlCommand(strClientes, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", pro.Nome + "%");

                    MySqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var lstProduto = new List<Produto>();

                        while (dr.Read())
                        {
                            var produto = new Produto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"]),
                                Marca = Convert.ToString(dr["marca"]),

                            };

                            lstProduto.Add(produto);
                        }
                        ViewBag.ListaProduto = lstProduto;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
            }
           
        }

        public ActionResult NovoProduto()
        {
            var lstMarca = new List<Marca>();
            using (var conexao = new Conexao())
            {
                string strVendedores = "SELECT * FROM marca;";
                using (var comando = new MySqlCommand(strVendedores, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var marca = new Marca
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };
                            lstMarca.Add(marca);
                        }
                    ViewBag.ListaMarca = lstMarca;
                }
            }
            return View();
        }

        public ActionResult EditarProduto()
        {

            return View();
        }
    }
}