using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TelerikAndKendo.WebForms
{
    public partial class TelerikSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadGrid1.DataSource = new[]
                {
                    new { Id = 1, Name = "Produit A" },
                    new { Id = 2, Name = "Produit B" }
                };
                RadGrid1.DataBind();
            }
        }

    }
}