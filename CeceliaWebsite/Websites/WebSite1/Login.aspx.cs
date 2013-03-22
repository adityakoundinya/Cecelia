using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Cecelia;
using System.Configuration;


public partial class Login : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        this.lblHeader.Text = "CM DB " + ConfigurationManager.AppSettings["WebsiteFor"];
        this.lblVersion.Text = "Version: " + ConfigurationManager.AppSettings["Version"];
        lblLoginError.Visible = false;
        txtUserName.Focus();
        //txtUserName.Text = "aditya.koundinya";
        //txtPassWord.Text = "123";
    }

    protected void btnLogin_Click(object sender, EventArgs e) {
        Users u = new Users();
        UserWorker uw = new UserWorker();
        Users user = null;
        try {
            user = uw.Login(txtUserName.Text, txtPassWord.Text);

            if (user != null) {
                Session.Add("User", user);
                Response.Redirect("Home.aspx");
            } else {
                txtUserName.Text = string.Empty;
                txtPassWord.Text = string.Empty;
                lblLoginError.Text = "Incorrect Credentials. Please check your username and/or password";
                lblLoginError.Visible = true;
            }
        } catch (SqlException ex) {
            lblLoginError.Text = ex.Message;
            lblLoginError.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e) {

        txtUserName.Text = string.Empty;
        txtPassWord.Text = string.Empty;
        lblLoginError.Text = "Cancelled!";
        lblLoginError.Visible = true;
    }
}
