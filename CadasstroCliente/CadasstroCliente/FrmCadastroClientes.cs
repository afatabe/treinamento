using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace CadasstroCliente
{
    public partial class FrmCadastroClientes : Form
    {
        public FrmCadastroClientes()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            DataSet dataset = new DataSet("DadosCliente");
            DataTable tabela = CriarEstruturaTabela();

            // adiciona tabela ao dataset
            dataset.Tables.Add(tabela);

            DataRow registro = CriarRegistro(tabela);
            tabela.Rows.Add(registro);

            dataset.WriteXml(@"c:\bk\xml\cliente_" + txtCodigo.Text + ".xml");
        }

        private DataRow CriarRegistro(DataTable tabela)
        {
            // cria os registro
            DataRow registro = tabela.NewRow();
            registro["Codigo"] = txtCodigo.Text;
            registro["Nome"] = txtNome.Text;
            registro["Telefone"] = txtTelefone.Text;
            registro["Email"] = txtEmail.Text;
            return registro;
        }

        private static DataTable CriarEstruturaTabela()
        {
            DataTable tabela = new DataTable("Clientes");
            // Cria coluna na tabela
            tabela.Columns.Add(new DataColumn("Codigo"));
            tabela.Columns.Add(new DataColumn("Nome"));
            tabela.Columns.Add(new DataColumn("Telefone"));
            tabela.Columns.Add(new DataColumn("Email"));
            return tabela;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            DataSet dataset = new DataSet();
            dataset.ReadXml(@"c:\bk\xml\cliente_" + txtCodigo.Text + ".xml");
            DataTable tabela = dataset.Tables[0];
            DataRow registro = tabela.Rows[0];
            MostrarDadosTela(registro);

        }

        private void MostrarDadosTela(DataRow registro)
        {
            txtCodigo.Text = registro["Codigo"].ToString();
            txtNome.Text = registro["Nome"].ToString();
            txtTelefone.Text = registro["Telefone"].ToString();
            txtEmail.Text = registro["Email"].ToString();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            foreach (Control txt in Controls)
            {
                if (txt is TextBox)
                    (txt as TextBox).Clear();

            }
        }
    }
}
