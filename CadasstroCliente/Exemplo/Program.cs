using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
