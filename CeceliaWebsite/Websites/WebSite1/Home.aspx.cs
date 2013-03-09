using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cecelia;
using System.Data;
using System.Linq;


public partial class _Default : System.Web.UI.Page {

    #region Global Variables

    List<Product> _Products = new List<Product>();
    Users LoginUser;

    #endregion

    #region HtmlEvents

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            Session.Remove("Edit");
            Session.Remove("Products");
            Session.Remove("searchType");
            Session.Remove("Expression");
            Session.Remove("IsAscending");
            Session.Remove("IsSorted");
        }
        if (Session["User"] != null) {
            LoginUser = (Users)Session["User"];
            lblWelcome.Text = "Welcome " + LoginUser.UserName;
            if (LoginUser.Role == Role.Admin) {
                ibAdmin.Visible = true;
            }
            if (Session["Edit"] == null) {
                Session.Add("Edit", false);
            }

            if (Session["Products"] != null) {
                _Products = (List<Product>)Session["Products"];

            }
            if (Session["Expression"] == null) {
                Session.Add("Expression", "Id");
            }
            if (Session["IsAscending"] == null) {
                Session.Add("IsAscending", true);
            }
            if (Session["IsSorted"] == null) {
                Session.Add("IsSorted", false);
            }
            if (!(bool)(Session["Edit"])) {
                this.PopulateProducts(this._Products);
            }
            lblError.Visible = false;
            dlgSortError.Visible = false;
        } else {
            Response.Redirect("Login.aspx");
        }

    }
    protected void btnQuickAddProduct_Click(object sender, EventArgs e) {
        if (txtCompanyName.Text != string.Empty && txtCategory.Text != string.Empty && txtFlavor.Text != string.Empty) {
            lblError.Visible = true;
            Product p = new Product();

            p.Category = txtCategory.Text.Trim();
            p.CompanyName = txtCompanyName.Text.Trim();
            p.Flavor = txtFlavor.Text.Trim();
            if (chkCF.Checked) {
                p.CF = true;
            } else {
                p.CF = false;
            }
            if (chkSF.Checked) {
                p.SF = true;
            } else {
                p.SF = false;
            }
            if (chkCT.Checked) {
                p.CRT = true;
            } else {
                p.CRT = false;
            }
            if (chkFC.Checked) {
                p.FAC = true;
            } else {
                p.FAC = false;
            }
            p.LastUpdated = DateTime.Now;
            p.Type1 = txtType1.Text;
            p.Type2 = txtType2.Text;
            p.User = ((Users)Session["User"]).UserName;

            ProductWorker w = new ProductWorker();

            bool result = w.AddProduct(p);
            if (result) {
                MessageBox("Product Added!");
            } else {
                MessageBox("Product Add Error!");
            }

            Session.Remove("Products");

            ClearQuickAddProduct();

        } else {
            MessageBox("Category, Company or Flavor cannot be empty");
            //lblError.Text = "Category, Company or Flavor cannot be empty";
            //lblError.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        if (((txtCompanySearch.Text != string.Empty || txtCategorySearch.Text != string.Empty) && (rdbCategory.Checked || rdbCompany.Checked)) ||
            (txtCompanySearch.Text != string.Empty && txtCategorySearch.Text != string.Empty && (rdbBoth.Checked || rdbAny.Checked))) {
            lblsearchResults.Visible = true;
            lblSearchBy.Visible = true;
            string categorysearch = txtCategorySearch.Text;
            string companysearch = txtCompanySearch.Text;
            lblError.Visible = false;
            bool isCategory = false;
            bool isSingleFilter = false;
            if (categorysearch != string.Empty && companysearch == string.Empty) {
                isCategory = true;
            }
            if (!rdbAny.Checked && !rdbBoth.Checked) {
                isSingleFilter = true;
                if (isCategory) {
                    lblSearchBy.Text = "Search by Category: " + categorysearch;
                } else {
                    lblSearchBy.Text = "Search by Company: " + companysearch;
                }
            } else {
                isSingleFilter = false;
                if (rdbAny.Checked) {
                    lblSearchBy.Text = "Search by Company: " + companysearch + " or Search by Category: " + categorysearch;
                } else {
                    lblSearchBy.Text = "Search by Company: " + companysearch + " and Search by Category: " + categorysearch;
                }
            }
            ProductWorker w = new ProductWorker();
            if (isSingleFilter && isCategory) {
                _Products = w.SearchProduct(categorysearch, isCategory);
            } else if (isSingleFilter && !isCategory) {
                _Products = w.SearchProduct(companysearch, isCategory);
            } else if (!isSingleFilter) {
                _Products = w.SearchProduct(categorysearch, companysearch, rdbBoth.Checked);
            }
            Session.Add("Products", _Products);
            lblsearchResults.Text = "Search Results: " + this._Products.Count.ToString() + " products found";
            gridView.PageIndex = 0;
            PopulateProducts(this._Products);
            ClearSearchBoxes();
            Session.Remove("Expression");
            Session.Remove("IsAscending");
        } else {
            if (txtCompanySearch.Text == string.Empty && txtCategorySearch.Text == string.Empty) {
                MessageBox("Enter a Search String");
            } else if ((txtCompanySearch.Text != string.Empty && txtCategorySearch.Text != string.Empty && (!rdbBoth.Checked || !rdbAny.Checked))) {
                MessageBox("Check either Any or Both radio button");
            } else if (txtCategorySearch.Text != string.Empty && txtCompanySearch.Text == string.Empty && !rdbCategory.Checked) {
                MessageBox("Check Category radio button");
            } else if (txtCategorySearch.Text == string.Empty && txtCompanySearch.Text != string.Empty && !rdbCompany.Checked) {
                MessageBox("Check Company radio button");
            }
        }


    }
    protected void ibLogout_Click(object sender, EventArgs e) {
        Session.Remove("User");
        Session.Remove("Products");
        Session.Remove("Edit");
        Response.Redirect("Login.aspx");
    }
    protected void ibAdmin_Click(object sender, EventArgs e) {
        Response.Redirect("Admin.aspx");
    }
    #endregion

    #region GridView Events

    protected void gridView_RowEditing(object sender, GridViewEditEventArgs e) {
        GridView gv = (GridView)sender;
        gv.EditIndex = e.NewEditIndex;
        Session.Add("Edit", true);
        BindData((List<Product>)Session["Products"]);
    }
    protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
        Session.Add("Edit", false);
        gridView.EditIndex = -1;
        BindData((List<Product>)Session["Products"]);
    }
    protected void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e) {
        List<Product> products = (List<Product>)Session["Products"];
        GridViewRow row = gridView.Rows[e.RowIndex];
        string cell2 = ((TextBox)(row.Cells[3].Controls[0])).Text;
        string cell3 = ((TextBox)(row.Cells[4].Controls[0])).Text;
        string cell4 = ((TextBox)(row.Cells[5].Controls[0])).Text;
        string cell5 = ((TextBox)(row.Cells[6].Controls[0])).Text;
        string cell6 = ((TextBox)(row.Cells[7].Controls[0])).Text;
        Product p = products.Find(o => o.Id == long.Parse(((TextBox)(row.Cells[2].Controls[0])).Text.ToString()));
        if (p != null) {
            if (Session["Edit"] != null) {
                //if (Session["searchType"] != null) {
                //    if (bool.Parse(((string[])Session["searchType"])[0])) {
                //        p.CompanyName = cell2;
                //    } else {
                //        p.Category = cell3;
                //    }
                //} else {
                p.CompanyName = cell2;
                p.Category = cell3;
                //}
                p.Type1 = cell4;
                p.Type2 = cell5;
                p.Flavor = cell6;
                p.CF = ((CheckBox)row.Cells[8].Controls[0]).Checked;
                p.SF = ((CheckBox)row.Cells[9].Controls[0]).Checked;
                p.CRT = ((CheckBox)row.Cells[10].Controls[0]).Checked;
                p.FAC = ((CheckBox)row.Cells[11].Controls[0]).Checked;
                p.LastUpdated = DateTime.Now;
                p.User = ((Users)Session["User"]).UserName;

                CeceliaDataProvider dp = new CeceliaDataProvider();
                dp.UpdateProduct(p);
                products.Remove(products.Find(o => o.Id == p.Id));
                products.Add(p);
            }
            Session.Add("Products", products);
        }
        Session.Add("Edit", false);
        gridView.EditIndex = -1;
        BindData((List<Product>)Session["Products"]);
    }
    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType != DataControlRowType.Pager) {

            //if (Session["searchType"] != null) {
            //    string[] searchType = (string[])Session["searchType"];
            //    bool isCategory = bool.Parse(searchType[0]);
            //    if (searchType[1].ToLower() != "all") {
            //        if (isCategory) {
            //            e.Row.Cells[3].Visible = false;
            //        } else {
            //            e.Row.Cells[2].Visible = false;
            //        }
            //    }
            //}
            string time = e.Row.Cells[12].Text;
            if (e.Row.RowType != DataControlRowType.Header) {
                e.Row.Cells[2].Enabled = false;
                e.Row.Cells[12].Enabled = false;
                e.Row.Cells[13].Enabled = false;
                if (!e.Row.RowState.ToString().Contains("Edit")) {
                    if (!time.ToLower().Contains("lastupdated")) {
                        if (time != DateTime.MinValue.ToString()) {
                            e.Row.BackColor = System.Drawing.Color.Black;
                            e.Row.ForeColor = System.Drawing.Color.White;
                        }
                    }
                } else {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
        }

    }
    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        gridView.PageIndex = e.NewPageIndex;
        BindData((List<Product>)Session["Products"]);
    }
    protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
        GridViewRow row = gridView.Rows[e.RowIndex];
        long ProductId = long.Parse(row.Cells[2].Text.ToString());
        ProductWorker pw = new ProductWorker();
        pw.DeleteProduct(ProductId);
        List<Product> products = (List<Product>)Session["Products"];
        products.Remove(products.Find(o => o.Id == long.Parse(row.Cells[2].Text.ToString())));
        Session.Add("Products", products);
        PopulateProducts(products);

    }
    protected void gridView_ColumnSorting(object sender, GridViewSortEventArgs e) {
        if (e.SortExpression == "Id" || e.SortExpression == "Category" || e.SortExpression == "CompanyName" || e.SortExpression == "Flavor" ||
            e.SortExpression == "Type1" || e.SortExpression == "Type2" || e.SortExpression == "LastUpdated" || e.SortExpression == "User") {
            Session["Expression"] = e.SortExpression;
            if (Session["Expression"].ToString() != null && Session["Expression"].ToString() == e.SortExpression) {
                Session["IsAscending"] = !bool.Parse(Session["IsAscending"].ToString());
            }
            Session.Add("IsSorted", false);
        } else {
            MessageBox("Table cannot be sorted on " + e.SortExpression + " column");
            e.Cancel = true;
        }

    }
    protected void gridView_ColumnSorted(object sender, EventArgs e) {
        BindData((List<Product>)Session["Products"]);
    }
    #endregion

    #region Private Methods

    private void BindData(List<Product> products) {
        string expression = Session["Expression"].ToString();
        bool isAscending = bool.Parse(Session["IsAscending"].ToString());
        gridView.DataSource = SortProducts(expression, isAscending, products);
        gridView.DataBind();
    }
    private void PopulateProducts(List<Product> products) {
        gridView.AutoGenerateEditButton = true;
        gridView.AutoGenerateColumns = true;
        gridView.AutoGenerateDeleteButton = false;
        gridView.HeaderStyle.BackColor = System.Drawing.Color.Gray;
        gridView.ShowHeader = true;
        gridView.RowEditing += new GridViewEditEventHandler(gridView_RowEditing);
        gridView.RowUpdating += new GridViewUpdateEventHandler(gridView_RowUpdating);
        BindData(products);
    }
    private void MessageBox(string sMessage) {
        LiteralControl c = new LiteralControl("<center><br />" + sMessage + "<br /><br /><input type='button' value='OK' onclick='dlgSortError.Close()' /></center>");
        dlgSortError.Controls.Add(c);
        dlgSortError.Title = "Information Message";
        dlgSortError.Visible = true;
        dlgSortError.VisibleOnLoad = true;
        //form1.Controls.Add(dlgSortError);
    }
    private void ClearQuickAddProduct() {
        txtCompanyName.Text = string.Empty;
        txtCategory.Text = string.Empty;
        txtFlavor.Text = string.Empty;
        txtType1.Text = string.Empty;
        txtType2.Text = string.Empty;
        chkCF.Checked = false;
        chkCT.Checked = false;
        chkFC.Checked = false;
        chkSF.Checked = false;
    }
    private void ClearSearchBoxes() {
        rdbAny.Checked = false;
        rdbBoth.Checked = false;
        rdbCompany.Checked = false;
        rdbCategory.Checked = false;
        txtCategorySearch.Text = string.Empty;
        txtCompanySearch.Text = string.Empty;
        txtCategorySearch.Enabled = true;
        txtCompanySearch.Enabled = true;
        txtCategorySearch.BackColor = System.Drawing.Color.White;
        txtCompanySearch.BackColor = System.Drawing.Color.White;
    }
    private List<Product> SortProducts(string Expression, bool isAscending, List<Product> products) {
        if (bool.Parse(Session["IsSorted"].ToString())) return products;
        switch (Expression) {
            case "CompanyName":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.CompanyName.Trim().CompareTo(p2.CompanyName.Trim()); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.CompanyName.Trim().CompareTo(p1.CompanyName.Trim()); });
                break;
            case "Flavor":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.Flavor.Trim().CompareTo(p2.Flavor.Trim()); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.Flavor.Trim().CompareTo(p1.Flavor.Trim()); });
                break;
            case "Category":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.Category.Trim().CompareTo(p2.Category.Trim()); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.Category.Trim().CompareTo(p1.Category.Trim()); });
                break;
            case "Type1":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.Type1.Trim().CompareTo(p2.Type1.Trim()); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.Type1.Trim().CompareTo(p1.Type1.Trim()); });
                break;
            case "Type2":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.Type2.Trim().CompareTo(p2.Type2.Trim()); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.Type2.Trim().CompareTo(p1.Type2.Trim()); });
                break;
            case "Id":
                if (isAscending)
                    products.Sort(delegate(Product p1, Product p2) { return p1.Id.CompareTo(p2.Id); });
                else
                    products.Sort(delegate(Product p1, Product p2) { return p2.Id.CompareTo(p1.Id); });
                break;
            case "LastUpdated":
                if (isAscending) {
                    products.Sort(delegate(Product p1, Product p2) { return p1.LastUpdated.CompareTo(p2.LastUpdated); });
                } else {
                    products.Sort(delegate(Product p1, Product p2) { return p2.LastUpdated.CompareTo(p1.LastUpdated); });
                }
                break;
            case "User":
                if (isAscending) {
                    products.Sort(delegate(Product p1, Product p2) { return p1.User.CompareTo(p2.User); });
                } else {
                    products.Sort(delegate(Product p1, Product p2) { return p2.User.CompareTo(p1.User); });
                }
                break;
            default:
                products.Sort(delegate(Product p1, Product p2) { return p1.Id.CompareTo(p2.Id); });
                break;
        }
        Session.Add("IsSorted", true);
        return products;
    }
    #endregion

}
