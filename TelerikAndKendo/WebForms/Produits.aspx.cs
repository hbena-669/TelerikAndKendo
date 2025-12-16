using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TelerikAndKendo.WebForms
{
    public partial class Produits : System.Web.UI.Page
    {
        private static List<Product> _products;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_products == null)
                {
                    _products = new List<Product>
                {
                    new Product { Id = 1, Name = "Produit A", Price = 10 },
                    new Product { Id = 2, Name = "Produit B", Price = 20 }
                };
                }
            }
        }

        protected void RadGridProducts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGridProducts.DataSource = _products;
        }

        protected void RadGridProducts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            var values = new Hashtable();
            e.Item.OwnerTableView.ExtractValuesFromItem(values, editedItem);

            var product = new Product
            {
                Id = _products.Max(p => p.Id) + 1,
                Name = values["Name"]?.ToString(),
                Price = Convert.ToDecimal(values["Price"])
            };

            _products.Add(product);
            
        }

        protected void RadGridProducts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = (GridEditableItem)e.Item;
            int id = (int)editableItem.GetDataKeyValue("Id");

            var product = _products.First(p => p.Id == id);
            product.Name = (editableItem["Name"].Controls[0] as TextBox)?.Text;
            product.Price = Convert.ToDecimal((editableItem["Price"].Controls[0] as TextBox)?.Text);
        }

        protected void RadGridProducts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            int id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
            var product = _products.First(p => p.Id == id);
            _products.Remove(product);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}