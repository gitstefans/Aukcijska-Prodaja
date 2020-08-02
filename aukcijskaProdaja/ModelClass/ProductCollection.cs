using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class ProductCollection : ObservableCollection<Product>
    {

        public static ProductCollection GetAllProducts()
        {
            ProductCollection products = new ProductCollection();
            Product product = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT ProductID, ProductName, ProductPrice, LastBid, LastBidder FROM Products WHERE is_deleted = 0", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = Product.GetProductsFromResultSet(reader);

                        products.Add(product);
                    }
                }

            }
            return products;
        }
    }

}
