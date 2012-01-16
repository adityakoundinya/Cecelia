using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cecelia;

public partial class AddUser : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e)    {
        if (Session["User"] != null) {
            if (!Page.IsPostBack) {
                ddlRole.DataSource = Enum.GetNames(typeof(Role));
                ddlRole.DataBind();
                ClearTable();
            }
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
}
