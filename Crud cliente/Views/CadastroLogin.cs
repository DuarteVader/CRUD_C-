using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Crud_cliente
{
    public partial class CadastroLogin : Form
    {
        SqlConnection Conexao = new SqlConnection(@"Data Source=DESKTOP-PCK3C0Q;Initial Catalog=LoginCSharp;Persist Security Info=True;User ID=sa;Password=12345678");
        public CadastroLogin()
        {
            InitializeComponent();
            txtUsuario.Select();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {
                using (SqlCommand dbcomand = Conexao.CreateCommand())
                {
                    Conexao.Open();
                    var insert = "INSERT INTO Usuario (USERNAME, PASSWORD, EMAIL) VALUES (@USERNAME, @PASSWORD, @EMAIL)";

                    dbcomand.CommandText = insert;

                    dbcomand.Parameters.AddWithValue("@USERNAME", txtUsuario.Text);
                    dbcomand.Parameters.AddWithValue("@PASSWORD", txtSenha.Text);
                    dbcomand.Parameters.AddWithValue("@EMAIL", txtEmail.Text);

                    dbcomand.ExecuteNonQuery();
                }
                MessageBox.Show("Cadastrado com Sucesso");
                Conexao.Close();
                txtUsuario.Clear();
                txtSenha.Clear();
                txtEmail.Clear();
            }
            else
            {
                MessageBox.Show("Preencha os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Clear();
                txtSenha.Clear();
                txtEmail.Clear();
                txtUsuario.Select();

            }
            Conexao.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            this.Hide();
            login.Show();
        }
    }
}

