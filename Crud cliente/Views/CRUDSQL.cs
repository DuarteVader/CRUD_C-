using Crud_cliente.Class_Crud;
using Crud_cliente.Class_Rotinas_Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_cliente
{
    public partial class CRUDSQL : Form
    {
        public CRUDSQL()
        {
            InitializeComponent();
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=DESKTOP-PCK3C0Q;
                Initial Catalog=master;User ID=sa;Password=12345678";
                
                           cnn = new SqlConnection(connetionString);
                cnn.Open();
                MessageBox.Show("Connection Open !");
                cnn.Close();
            }
            catch (SqlException erro)
            {
                MessageBox.Show("Erro ao se conectar no banco de dados \n" +
                "Verifique os dados informados" + erro);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxNome.Text) && !string.IsNullOrEmpty(txtEnde.Text) && !string.IsNullOrEmpty(cmbProduto.Text))
            {
                try
                {
                    string connetionString;
                    SqlConnection cnn;
                    connetionString = @"Data Source=DESKTOP-PCK3C0Q;
                Initial Catalog=master;User ID=sa;Password=12345678";

                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    using (SqlCommand dbcomand = cnn.CreateCommand())
                    {
                        var insert = "INSERT INTO Clientes (Nome, ENDERECO, Produto) VALUES (@NOME, @ENDERECO, @PRODUTO)";

                        dbcomand.CommandText = insert;

                        dbcomand.Parameters.AddWithValue("@NOME", txtBoxNome.Text);
                        dbcomand.Parameters.AddWithValue("@ENDERECO", txtEnde.Text);
                        dbcomand.Parameters.AddWithValue("@PRODUTO", cmbProduto.Text);

                        dbcomand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Cadastrado com Sucesso");
                    cnn.Close();
                    txtBoxNome.Clear();
                    txtEnde.Clear();
                    cmbProduto.ResetText();
                    txtId.Clear();
                    txtId.Text = "Pesquisar";
                }

                catch (SqlException erro)
                {
                    MessageBox.Show("Erro ao se conectar no banco de dados \n" +
                    "Verifique os dados informados" + erro);

                }
            }
            else
            {
                MessageBox.Show("Preencha os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBoxNome.Clear();
                txtEnde.Clear();
                cmbProduto.ResetText();
                txtBoxNome.Select();
            }
            
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId.Text) || txtId.Text != "Pesquisa")
                {
                    List<CrudProdutos> produtos = new CrudSelect().BuscarIDProdutos(Convert.ToInt32(txtId.Text));

                    if (produtos != null)
                    {
                        foreach (var item in produtos)
                        {

                            txtBoxNome.Text = item.Nome;
                            txtEnde.Text = item.Endereco;
                            cmbProduto.Text = item.Produto;

                        }
                    }

                }
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
                txtBoxNome.Clear();
                txtEnde.Clear();
                txtId.Clear();
                txtId.Text = "Pesquisar";
            }
            

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "Pesquisar")
                {
                    new CrudDelete().DeletaCRUD(Convert.ToInt32(txtId.Text));

                    MessageBox.Show("Cadastro excluído com sucesso!");
                    txtBoxNome.Clear();
                    txtEnde.Clear();
                    cmbProduto.ResetText();
                    txtId.Clear();
                    txtId.Enabled = false;
                    txtId.Text = "Pesquisar";
                }
                else
                {
                    MessageBox.Show("Necessário declarar o ID da rotina", "Atenção!");
                }


            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                txtBoxNome.Clear();
                txtEnde.Clear();
                txtId.Clear();
                txtId.Text = "Pesquisar";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            txtId.ResetText();

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBoxNome.Clear();
            txtEnde.Clear();
            cmbProduto.ResetText();
            txtId.Clear();
            txtId.Enabled = false;
            txtId.Text = "Pesquisar";
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxNome.Text) && !string.IsNullOrEmpty(txtEnde.Text) && !string.IsNullOrEmpty(txtId.Text)
                && !string.IsNullOrEmpty(cmbProduto.Text) && txtId.Text != "Pesquisar")
            {

                CrudProdutos dados = new CrudProdutos();

                dados.Nome = txtBoxNome.Text;
                dados.Endereco = txtEnde.Text;
                dados.Produto = cmbProduto.Text;
                dados.Id = Convert.ToInt32(txtId.Text);


                CrudUpdate update = new CrudUpdate();

                update.UpdateCRUD(dados);

                MessageBox.Show("Informações da Empresa atualizadas com Sucesso");
                txtBoxNome.Clear();
                txtEnde.Clear();
                cmbProduto.ResetText();
                txtId.Clear();
                txtId.Text = "Pesquisar";
                txtId.Enabled = false;

            }
            else
            {
                MessageBox.Show("Obrigatório o Preenchimento dos campos 'Nome', 'Endereço', 'Produto' e 'ID' para ser possível editar as informações!", "Atenção!");
                txtBoxNome.Clear();
                txtEnde.Clear();
                cmbProduto.ResetText();
                txtId.Clear();
                txtId.Text = "Pesquisar";
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            this.Hide();
            login.Show();
        }
    }
}
