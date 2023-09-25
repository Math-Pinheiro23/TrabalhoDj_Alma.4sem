using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(Cliente cli)
        {
            var lstVendedores = new List<Usuario>();
            using (var conexao = new Conexao())
            {
                string strVendedores = "SELECT * FROM usuarios where isExcluido = false order by nome;";
                using (var comando = new MySqlCommand(strVendedores, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var usario = new Usuario
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };

                            lstVendedores.Add(usario);
                        }
                    ViewBag.ListaVendedores = lstVendedores;
                }
            }

            using (var conexao = new Conexao())
            {

                string strClientes = "SELECT * FROM clientes " +
                "WHERE nome like @nome and " +
                "isExcluido = false;";

                using (var comando = new MySqlCommand(strClientes, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", cli.Nome + "%");

                    MySqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var lstClientes = new List<Cliente>();

                        while (dr.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"]),
                                Telefone = Convert.ToString(dr["telefone"]),
                                EMail = Convert.ToString(dr["email"]),
                                // Para levar pra view, traz do banco de dados
                                // em formato DateTime e converte
                                // para string para formatar para o usuário
                                DataNasc = Convert.ToDateTime(dr["dataNasc"]).ToString("dd/MM/yyyy")
                            };

                            lstClientes.Add(cliente);
                        }
                        ViewBag.ListaClientes = lstClientes;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
            }
        }

        public ActionResult NovoCliente()
        {
            var lstVendedores = new List<Usuario>();
            using (var conexao = new Conexao())
            {
                string strVendedores = "SELECT * FROM usuarios where isExcluido = false order by nome;";
                using (var comando = new MySqlCommand(strVendedores, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var usario = new Usuario
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };
                            lstVendedores.Add(usario);
                        }
                    ViewBag.ListaVendedores = lstVendedores;
                }
            }
            return View();
        }

        public ActionResult EditarCliente()
        {

            return View();
        }
    }
}