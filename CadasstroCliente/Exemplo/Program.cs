using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Validation;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void ConsultarClientes3()
        {
            var db = new CADASTROEntities();
            var qry = from c in db.CONTATOS
                      select new { Nome = c.CLIENTE.NomeCliente, Contato = c.Contato1 };
            foreach (var cli in qry)
            {
                Console.WriteLine(cli.Nome + " - " + cli.Contato);
            }
            Console.ReadKey();
        }

        private static void ConsultarClientes2()
        {
            var db = new CADASTROEntities();
            var qry = from cli in db.CLIENTES
                      where cli.NomeCliente.Contains("a")
                      orderby cli.IdCliente descending
                      select cli;
            foreach (var c in qry)
            {
                Console.WriteLine(c.IdCliente.ToString() + " - " + c.NomeCliente + " - " + c.Email);
                Console.WriteLine("------------------------------------------------");

            }
        }

        private static void ConsultarClientes1()
        {
            var db = new CADASTROEntities();
            foreach (var cli in db.CLIENTES)
            {
                Console.WriteLine(cli.NomeCliente);

                foreach (var cont in cli.CONTATOS)
                {
                    Console.WriteLine(cont.Tipo + " : " + cont.Contato1);
                }

                Console.WriteLine("-------------------------------");
            }

            Console.ReadLine();
        }

        private static void AddEntyFramwork()
        {
            var db = new CADASTROEntities();
            var cli = new CLIENTE()
            {
                NomeCliente = "Anderson Fabiano Antonio",
                Telefone = "11 98119-3235",
                Email = "ren.braga@hotmail.com"
            };
            var cont1 = new CONTATO()
            {
                Tipo = "EMAIL",
                Contato1 = "ren.braga@hotmail.com",
                CLIENTE = cli

            };
            var cont2 = new CONTATO()
            {
                Tipo = "TELEFONE",
                Contato1 = "11 98119-3235",
                CLIENTE = cli
            };
            db.CONTATOS.Add(cont1);
            db.CONTATOS.Add(cont2);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            Console.WriteLine("Registro inserido com sucesso");
            Console.ReadKey();
        }

        private static void Relacionamento()
        {
            var conStr = ConfigurationManager.ConnectionStrings["CADASTRO"].ConnectionString;
            var con = new SqlConnection(conStr);
            var SQL1 = "select * from CLIENTES";
            var SQL2 = "select * from CONTATOS";
            var cmd1 = new SqlCommand(SQL1, con);
            var cmd2 = new SqlCommand(SQL2, con);
            var ds = new DataSet("CLIENTES");
            var da1 = new SqlDataAdapter(cmd1);
            var da2 = new SqlDataAdapter(cmd2);
            da1.Fill(ds, "CLIENTES");
            da2.Fill(ds, "CONTATOS");
            var dtClientes = ds.Tables[0];
            var dtContatos = ds.Tables[1];
            DataRelation relation = ds.Relations.Add("ClienteContatos", dtClientes.Columns["IdCliente"], dtContatos.Columns["IdCliente"]);
            foreach (DataRow cli in dtClientes.Rows)
            {
                Console.WriteLine("Nome Cliente : " + cli[1].ToString());
                foreach (DataRow cont in cli.GetChildRows(relation))
                {
                    Console.WriteLine(cont[1].ToString() + " - " + cont[2].ToString());
                }
                Console.WriteLine("------------------------------------------------");
            }
            Console.ReadKey();
        }

        private static void ListaPorAdapater()
        {
            var conStr = ConfigurationManager.ConnectionStrings["CADASTRO"].ConnectionString;
            var con = new SqlConnection(conStr);
            var SQL = "select * from CLIENTES";
            var cmd = new SqlCommand(SQL, con);
            var ds = new DataSet("CADASTRO");
            var da = new SqlDataAdapter(cmd);
            da.Fill(ds, "CLIENTES");
            var dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("Id Cliente    : " + row[0].ToString());
                Console.WriteLine("Nome Cliente  : " + row[1].ToString());
                Console.WriteLine("Tel. Cliente  : " + row[2].ToString());
                Console.WriteLine("Email Cliente : " + row[3].ToString());
                Console.WriteLine("------------------------------------------------");
            }
            dt.WriteXml("dados.xml");
            Console.ReadKey();
        }
    }
}
