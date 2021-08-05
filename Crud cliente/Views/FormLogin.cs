using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Crud_cliente
{
    public partial class FormLogin : Form
    {
        //Referencia de conexao
        SqlConnection Conexao = new SqlConnection(@"Data Source=DESKTOP-PCK3C0Q;Initial Catalog=LoginCSharp;Persist Security Info=True;User ID=sa;Password=12345678");

        public FormLogin()
        {
            InitializeComponent();
            txtUsuario.Select();
        }

        //Botão Entrar Login
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {
                Conexao.Open();
                string query = "SELECT * FROM Usuario WHERE Username = '" + txtUsuario.Text + "' AND Password = '" + txtSenha.Text + "'";
                SqlDataAdapter dp = new SqlDataAdapter(query, Conexao);
                DataTable dt = new DataTable();

                dp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    CRUDSQL menu = new CRUDSQL();
                    this.Hide();
                    menu.Show();

                }

                else
                {
                    MessageBox.Show("Usuario ou Senha incorretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Clear();
                    txtSenha.Clear();
                    txtUsuario.Select();
                }
                Conexao.Close();
            }
            else
            {
                MessageBox.Show("Preencha os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Clear();
                txtSenha.Clear();
                txtUsuario.Select();
                
            }
        }

        //Encerrar a aplicação
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblCadastro_Click(object sender, EventArgs e)
        {
            CadastroLogin login = new CadastroLogin();
            this.Hide();
            login.Show();

        }
    }
}
