using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace web.core.Helper
{
    public static class ToSelectListItemsHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<Categories> categories, int selectedId)
        {
            return
                categories.OrderBy(category => category.CategoryName)
                      .Select(category =>
                          new SelectListItem
                          {
                              Selected = (category.CategoryID == selectedId),
                              Text = category.CategoryName,
                              Value = category.CategoryID.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<Suppliers> suppliers, int selectedId)
        {
            return
                suppliers.OrderBy(supplier => supplier.CompanyName)
                      .Select(category =>
                          new SelectListItem
                          {
                              Selected = (category.SupplierID == selectedId),
                              Text = category.CompanyName,
                              Value = category.SupplierID.ToString()
                          });
        }
    }
}
