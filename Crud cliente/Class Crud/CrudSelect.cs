using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;

namespace Crud_cliente
{
    public class CrudSelect
    {
        public List<CrudProdutos> BuscarIDProdutos(int Id)
        {

            List<CrudProdutos> verificaUsuario = new List<CrudProdutos>();
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-PCK3C0Q;
                Initial Catalog=master;User ID=sa;Password=12345678";

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            using (SqlCommand dbcomand = cnn.CreateCommand())
            {
                try
                {
                

                    using (var dbCommand = cnn.CreateCommand())
                    {
                        dbCommand.CommandTimeout = 60;

                        dbCommand.CommandText = "SELECT * FROM CLIENTES WHERE ID = @ID";

                        dbCommand.Parameters.AddWithValue("@ID", Id);

                        using (var dbDataReader = dbCommand.ExecuteReader())
                        {

                            if (dbDataReader.HasRows)
                            {
                                while (dbDataReader.Read())
                                {

                                    var dados = new CrudProdutos();


                                    dados.Nome = dbDataReader.GetString(1);
                                    dados.Endereco = dbDataReader.GetString(2);
                                    dados.Produto = dbDataReader.GetString(3);

                                    verificaUsuario.Add(dados);
                                }
                            }
                            else
                            {
                                verificaUsuario = null;
                            }
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return verificaUsuario;
        }
    }
}
