using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Newtonsoft.Json;
using TelerikAndKendo.Models;

namespace TelerikAndKendo.Controllers
{
    public class ProductsController : Controller
    {
        // Simule une base de données (à remplacer par Entity Framework)
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ordinateur Portable", Price = 999.99m, Quantity = 10, Category = "Électronique", InStock = true, CreatedDate = DateTime.Now.AddDays(-30) },
            new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Quantity = 25, Category = "Électronique", InStock = true, CreatedDate = DateTime.Now.AddDays(-15) },
            new Product { Id = 3, Name = "Chaise de Bureau", Price = 249.99m, Quantity = 5, Category = "Mobilier", InStock = false, CreatedDate = DateTime.Now.AddDays(-60) }
        };

        public ActionResult Index()
        {
            return View();
        }

        // API: GET tous les produits (avec pagination, tri, filtrage)
        [HttpGet]
        public JsonResult GetProducts(int? take, int? skip, List<GridSort> sort, GridFilter filter)
        {
            try
            {
                var query = _products.AsQueryable();

                // Filtrage
                if (filter != null && filter.Filters != null && filter.Filters.Any())
                {
                    query = ApplyFilters(query, filter);
                }

                // Tri
                if (sort != null && sort.Any())
                {
                    query = ApplySorting(query, sort);
                }

                // Comptage total (avant pagination)
                var total = query.Count();

                // Pagination
                if (skip.HasValue) query = query.Skip(skip.Value);
                if (take.HasValue) query = query.Take(take.Value);

                return Json(new
                {
                    data = query.ToList(),
                    total = total
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // API: CREATE
        [HttpPost]
        public JsonResult CreateProduct(Product product)
        {
            try
            {
                product.Id = _products.Max(p => p.Id) + 1;
                product.CreatedDate = DateTime.Now;
                _products.Add(product);

                return Json(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // API: UPDATE
        [HttpPost]
        public JsonResult UpdateProduct(int id, Product product)
        {
            try
            {
                var existing = _products.FirstOrDefault(p => p.Id == id);
                if (existing == null)
                    return Json(new { success = false, error = "Produit non trouvé" });

                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Quantity = product.Quantity;
                existing.Category = product.Category;
                existing.InStock = product.InStock;

                return Json(new { success = true, data = existing });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // API: DELETE
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                    _products.Remove(product);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // Classes pour le filtrage et tri (format Kendo UI)
        public class GridSort
        {
            public string field { get; set; }
            public string dir { get; set; }
        }

        public class GridFilter
        {
            public List<Filter> Filters { get; set; }
            public string Logic { get; set; }
        }

        public class Filter
        {
            public string Field { get; set; }
            public string Operator { get; set; }
            public object Value { get; set; }
        }

        // Méthodes helpers pour le filtrage/tri
        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, GridFilter filter)
        {
            // Implémentation simple du filtrage
            foreach (var f in filter.Filters)
            {
                switch (f.Field.ToLower())
                {
                    case "name":
                        query = query.Where(p => p.Name.Contains(f.Value.ToString()));
                        break;
                    case "category":
                        query = query.Where(p => p.Category == f.Value.ToString());
                        break;
                    case "instock":
                        query = query.Where(p => p.InStock == Convert.ToBoolean(f.Value));
                        break;
                }
            }
            return query;
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> query, List<GridSort> sort)
        {
            var firstSort = sort.First();
            if (firstSort.dir == "asc")
                query = query.OrderBy(p => p.Name);
            else
                query = query.OrderByDescending(p => p.Name);

            return query;
        }
    }
}