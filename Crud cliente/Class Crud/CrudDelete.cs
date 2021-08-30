using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_cliente.Class_Rotinas_Crud
{
    public class CrudDelete
    {
        public void DeletaCRUD(int Id)
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

                        var deletar = "DELETE FROM CLIENTES WHERE ID=@ID";


                        dbcomand.CommandText = deletar;
                        dbcomand.Parameters.AddWithValue("@ID", Id);

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
