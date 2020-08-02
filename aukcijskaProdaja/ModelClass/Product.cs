using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class Product : INotifyPropertyChanged
    {
        

        public int productID;
        public string productName;
        public decimal productPrice;
        public decimal lastBid;
        public string lastBidder;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ProductID
        {
            get { return productID; }
            set
            {
                if (productID == value)
                {
                    return;
                }
                productID = value;
            }
        }
        public string ProductName
        {
            get { return productName; }
            set
            {
                if (productName == value)
                {
                    return;
                }
                productName = value;
            }
        }
        public decimal ProductPrice
        {
            get { return productPrice; }
            set
            {
                if (productPrice == value)
                { return; }
                productPrice = value;
            }
        }
        public decimal LastBid
        {
            get { return lastBid; }
            set
            {
                if (lastBid == value)
                { return; }
                lastBid = value;
            }
        }
        public string LastBidder
        {
            get { return lastBidder; }
            set
            {
                if (lastBidder == value)
                {
                    return;
                }
                lastBidder = value;
            }
        }

        public Product()
        {
            ProductName = "";
            ProductName = this.ProductName;
        }
        public Product(int ProductID, string ProductName, decimal ProductPrice)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            

        }
        public Product(string ProductName, decimal ProductPrice)
        {
            
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;

        }
        public static Product GetProductsFromResultSet(SqlDataReader reader)
        {
            Product product = new Product((int)reader["ProductID"], (string)reader["ProductName"], (decimal)reader["ProductPrice"]);
            return product;
        }

   

        public void InsertProduct()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Products(ProductName, ProductPrice, Is_Deleted)" +
                    " VALUES(@ProductName, @ProductPrice, 0); SELECT IDENT_CURRENT('Products');", conn);

                SqlParameter productNameParam = new SqlParameter("@ProductName", SqlDbType.NVarChar);
                productNameParam.Value = this.ProductName;

                SqlParameter productPriceParam = new SqlParameter("@ProductPrice", SqlDbType.Decimal);
                productPriceParam.Value = this.ProductPrice;

                

                command.Parameters.Add(productNameParam);
                command.Parameters.Add(productPriceParam);
                


                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.ProductID = Convert.ToInt32(id);
                }

            }
        }
        public void DeleteProduct()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Products SET is_deleted=1 WHERE ProductID=@ProductID", conn);

                SqlParameter myProduct = new SqlParameter("@ProductID", SqlDbType.Int, 11);
                myProduct.Value = this.ProductID;

                command.Parameters.Add(myProduct);

                int rows = command.ExecuteNonQuery();

            }
        }
        public void UpdateProduct()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Products SET ProductName=@ProductName, ProductPrice=@ProductPrice, LastBid=@LastBid, LastBidder=@LastBidder WHERE ProductID=@ProductID", conn);

                SqlParameter productNameParam = new SqlParameter("@ProductName", SqlDbType.NVarChar);
                productNameParam.Value = this.ProductName;

                SqlParameter productPriceParam = new SqlParameter("@ProductPrice", SqlDbType.Decimal);
                productPriceParam.Value = this.ProductPrice;

                
                SqlParameter productLastBid = new SqlParameter("@LastBid", SqlDbType.Decimal);
                productLastBid.Value = this.LastBid;

                SqlParameter productLastBidder = new SqlParameter("@LastBidder", SqlDbType.NVarChar);
                productLastBidder.Value = this.LastBidder;


                SqlParameter IDParam = new SqlParameter("@ProductID", SqlDbType.Int, 11);
                IDParam.Value = this.ProductID;

                command.Parameters.Add(productNameParam);
                command.Parameters.Add(productPriceParam);
                command.Parameters.Add(productLastBid);
                command.Parameters.Add(productLastBidder);
                

                command.Parameters.Add(IDParam);

                int rows = command.ExecuteNonQuery();
                

            }
        }
        public void SaveProduct()
        {
            if (ProductID == 0)
            {
                InsertProduct();

            }
            else
            {
                UpdateProduct();
            }
        }

    }
}

