using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Crud_cliente.Class_Crud
{
    public class CrudUpdate
    {

        public void UpdateCRUD(CrudProdutos pos)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-PCK3C0Q;
                Initial Catalog=master;User ID=sa;Password=12345678";

            cnn = new SqlConnection(connetionString);
            
            using (SqlCommand dbcomand = cnn.CreateCommand())
            {
                try
                {
                    cnn.Open();

                    using (var dbCommand = cnn.CreateCommand())
                    {


                        dbcomand.CommandTimeout = 60;

                        var conexao = "UPDATE CLIENTE SET NOME = @NOME, ENDERECO = @ENDERECO, PRODUTO = @PRODUTO WHERE ID=@ID";

                        dbcomand.CommandText = conexao;

                        dbcomand.Parameters.AddWithValue("@NOME", pos.Nome);
                        dbcomand.Parameters.AddWithValue("@ENDERECO", pos.Endereco);
                        dbcomand.Parameters.AddWithValue("@PRODUTO", pos.Produto);
                        dbcomand.Parameters.AddWithValue("@ID", pos.Id);

                        dbcomand.ExecuteNonQuery();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

        }
    }
}
