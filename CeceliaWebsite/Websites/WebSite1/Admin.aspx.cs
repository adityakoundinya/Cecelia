using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cecelia;

public partial class Admin : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e)    {
        if (Session["User"] != null) {
            Users u =(Users) Session["User"];
            if (u.Role != Role.Admin) {
                MessageBox("You do not have the permission to view this page. Please relogin.");
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack) {
                ddlRole.DataSource = Enum.GetNames(typeof(Role));
                ddlRole.DataBind();
                ClearTable();
            }

            lblWelcome.Text = "Welcome " + u.UserName;
            lblError.Visible = false;
        } else {
            Response.Redirect("Login.aspx");
        } 
    }
    protected void btnAddUser_Click(object sender, EventArgs e) {
        if (txtPassword.Text != string.Empty && txtUserId.Text != string.Empty && txtUserName.Text != string.Empty && txtVerifyPassword.Text != string.Empty) {
            if (txtPassword.Text == txtVerifyPassword.Text) {
                Users u = new Users();
                u.UserName = txtUserName.Text;
                u.UserId = txtUserId.Text;
                u.Password = txtPassword.Text;
                u.Role = (Role)Enum.Parse(typeof(Role), ddlRole.SelectedItem.Text);
                UserWorker uw = new UserWorker();
                bool result = uw.AddUser(u);
                if (result) {
                    Response.Redirect("Home.aspx");
                } else {
                    lblError.Text = "User already exists. Choose a different UserId";
                    lblError.Visible = true;
                }
            } else {
                lblError.Text = "The passwords do not match. Please check and try again";
                lblError.Visible = true;
                txtPassword.Text = string.Empty;
                txtVerifyPassword.Text = string.Empty;
            }
        } else {
            lblError.Text = "Enter all the information to add the new user";
            lblError.Visible = true;
        }
    }
    protected void btnCancelUser_Click(object sender, EventArgs e) {
        ClearTable();
        lblError.Visible = false;
        Response.Redirect("Home.aspx");
    }

    private void ClearTable() {
        txtPassword.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtVerifyPassword.Text = string.Empty;
        ddlRole.SelectedIndex = -1;
    }

    private void MessageBox(string sMessage) {
        LiteralControl c = new LiteralControl("<center><br />" + sMessage + "<br /><br /><input type='button' value='OK' onclick='dlgPageLoadError.Close()' /></center>");
        dlgPageLoadError.Controls.Add(c);
        dlgPageLoadError.Title = "Information Message";
        dlgPageLoadError.Visible = true;
        dlgPageLoadError.VisibleOnLoad = true;
        //form1.Controls.Add(dlgSortError);
    }
    protected void btnResetDatabase_Click(object sender, EventArgs e) {
        ProductWorker pw = new ProductWorker();
        int result = pw.ResetDatabase();
        if (result > -1) {
            MessageBox(result.ToString() + " Products were reset");
        } else {
            MessageBox("There was an error resetting the database.");
        }
    }
    protected void ibHome_Click(object sender, EventArgs e) {
        Response.Redirect("Home.aspx");
    }
    protected void ibLogout_Click(object sender, EventArgs e) {
        Session.Remove("User");
        Session.Remove("Products");
        Session.Remove("Edit");
        Response.Redirect("Login.aspx");
    }
}
