using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomerCrudOperations.Models
{
    public class CustomerDbContext
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["CustomerConn"].ToString();
            con = new SqlConnection(constring);
        }


        //------------------- Add Customer -------------------
        public string AddCustomer(Customer custObj)
        {
            connection();
            string result = "";

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateDelete_Customer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", custObj.CustomerId);
                cmd.Parameters.AddWithValue("@Name", custObj.Name);
                cmd.Parameters.AddWithValue("@MobileNo", custObj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", custObj.Address);
                cmd.Parameters.AddWithValue("@Birthdate", custObj.Birthdate);
                cmd.Parameters.AddWithValue("@EmailId", custObj.EmailID);
                cmd.Parameters.AddWithValue("@Query", 1);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        //----------------------------------------------------


        //----------------- Update Customer ------------------

        public string UpdateCustomer(Customer custObj)
        {
            connection();
            string result = "";

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateDelete_Customer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", custObj.CustomerId);
                cmd.Parameters.AddWithValue("@Name", custObj.Name);
                cmd.Parameters.AddWithValue("@MobileNo", custObj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", custObj.Address);
                cmd.Parameters.AddWithValue("@Birthdate", custObj.Birthdate);
                cmd.Parameters.AddWithValue("@EmailId", custObj.EmailID);
                cmd.Parameters.AddWithValue("@Query", 2);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customers: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        //----------------------------------------------------


        //----------------- Delete Customer ------------------

        public int DeleteData(String Id)
        {
            connection();
            int result;

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateDelete_Customer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", Id);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 3);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }

        //----------------------------------------------------


        //----------------- GetAll Customer ------------------

        public List<Customer> SelectAllData()
        {
            connection();
            DataSet ds = null;
            List<Customer> custlist = new List<Customer>();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateDelete_Customer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 4);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Customer cobj = new Customer();
                        cobj.CustomerId = Convert.ToInt32(row["CustomerId"]);
                        cobj.Name = row["Name"].ToString();
                        cobj.Address = row["Address"].ToString();
                        cobj.MobileNo = row["MobileNo"].ToString();
                        cobj.EmailID = row["EmailId"].ToString();
                        cobj.Birthdate = Convert.ToDateTime(row["Birthdate"]);

                        custlist.Add(cobj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customers: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return custlist;
        }

        //----------------------------------------------------


        //--------------- Get Customer By Id -----------------

        public Customer SelectDatabyID(string CustomerID)
        {
            connection();
            DataSet ds = null;
            Customer cobj = null;
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertUpdateDelete_Customer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@MobileNo", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailId", null);
                cmd.Parameters.AddWithValue("@Query", 5);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new Customer();
                    cobj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.MobileNo = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailId"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                }
                return cobj;
            }
            catch
            {
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }

        //----------------------------------------------------
    }
}