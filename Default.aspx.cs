using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Admin_Products_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(kmb.GetConnection());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProducts();
        }
    }

    void GetProducts()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT ProductID, Name, Category,
            Code, Description, Image, Price,
            IsFeatured, DateAdded, DateModified, Status
            FROM Products p INNER JOIN Categories c ON c.CatID = p.CatID WHERE p.CatID IN (1,2,3,4,5)";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Products");
        lvProducts.DataSource = ds;
        lvProducts.DataBind();
        con.Close();
        
    }

    void GetProducts(string keyword)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT ProductID, Name, Category,
            Code, Description, Image, Price,
            IsFeatured, DateAdded, DateModified, Status
            FROM Products p INNER JOIN Categories c ON c.CatID = p.CatID WHERE 
p.ProductID LIKE '%" + keyword + "%' OR p.Name LIKE '%" + keyword + "%' OR c.Category LIKE '%" + keyword + "%' OR p.Code LIKE '%" + keyword + "%' OR p.Description LIKE '%" + keyword + "%' OR p.Image LIKE '%" + keyword + "%' OR p.Price LIKE '%" + keyword + "%' OR p.IsFeatured LIKE '%" + keyword + "%'";
                     

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Products");
        lvProducts.DataSource = ds;
        lvProducts.DataBind();
        con.Close();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtKeyword.Text == "")
        {
            GetProducts();
        }
        else
        {
            GetProducts(txtKeyword.Text);
        }
    }

    protected void lvProducts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpProducts.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

        if (txtKeyword.Text == "")
        {
            GetProducts();
        }
        else
        {
            GetProducts(txtKeyword.Text);
        }
    }
}