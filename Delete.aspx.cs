﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

public partial class Admin_Products_Delete : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(kmb.GetConnection());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int productID = 0;
            bool validProduct = int.TryParse(Request.QueryString["ID"].ToString(), 
                out productID);

            if (validProduct)
            {
                if (!IsPostBack)
                {
                    DeleteRecord(productID);
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
                
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
            
        }
    

    void DeleteRecord(int ID)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"DELETE FROM Products WHERE ProductID=@ProductID";
        cmd.Parameters.AddWithValue("ProductID", ID);
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("Default.aspx");
    }
}