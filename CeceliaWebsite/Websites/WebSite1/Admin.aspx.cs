using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
            this.lblHeader.Text = "CM DB " + ConfigurationManager.AppSettings["WebsiteFor"];
            this.lblVersion.Text = "Version: " + ConfigurationManager.AppSettings["Version"];
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

    protected void btnExtract_Click(object sender, EventArgs e) {
        bool isCF = false;
        bool isSF = false;

        string buttonPressed = ((Button)sender).Text;

        switch (buttonPressed) {
            case "G.F":
                isCF = false;
                isSF = false;
                break;
            case "C.F":
                isCF = true;
                break;
            case "C.F & S.F":
                isCF = true;
                isSF = true;
                break;
            default:
                return;
        }

        Extract ex = new Extract();
        string extract = ex.GetTextString(isCF, isSF);

        Response.Clear();

        string fileName = GetFileName(isCF, isSF);

        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);

        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8)) {
            writer.Write(extract);
        }
        Response.End();
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
    private string GetFileName(bool isCF, bool isSF) {
        string path = string.Empty;
        string fileName = "Cecelia";
        string websiteFor = ConfigurationManager.AppSettings["WebsiteFor"];
        string ext = ".txt";

        string dateTime = DateTime.Now.ToShortDateString();
        dateTime = dateTime.Replace("/", "_");
        string extractKind = string.Empty;
        if (isCF && !isSF) {
            extractKind = "CF";
        } else if (isSF && !isCF) {
            extractKind = "SF";
        } else if (isCF && isSF) {
            extractKind = "CF & SF";
        } else {
            extractKind = "GF";
        }
        path = fileName + "_" + websiteFor + "_" + extractKind + "_" + dateTime + ext;
        return path;
    }
}
