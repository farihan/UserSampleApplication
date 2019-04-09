using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hans.UserSample.Web.Helpers
{
    public static class Sorter
    {
        public static HtmlString SortLinks(this IHtmlHelper helper, string headerName, string sortBy, string currentSort, int currentPage, bool currentAsc)
        {
            var controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            var sort = sortBy.ToLower();
            var isAsc = sort == currentSort ? (!currentAsc).ToString().ToLower() : "true";
            var sb = new StringBuilder();
            sb.Append(string.Format("<a href=\"{0}?page={1}&sort={2}&asc={3}\">{4}</a>", controller, currentPage, sort, isAsc, headerName));

            if (sort == currentSort)
            {
                if (currentAsc)
                {
                    sb.Append(string.Format("&nbsp;<span class=\"glyphicon glyphicon-circle-arrow-up\"></span>"));
                }
                else
                {
                    sb.Append(string.Format("&nbsp;<span class=\"glyphicon glyphicon-circle-arrow-down\"></span>"));
                }
            }

            return new HtmlString(sb.ToString());
        }

        public static HtmlString SortLinks(this IHtmlHelper helper, string headerName, string sortBy, string currentSort, int currentPage, bool currentAsc, string currentQuery)
        {
            var controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            var sort = sortBy.ToLower();
            var isAsc = sort == currentSort ? (!currentAsc).ToString().ToLower() : "true";
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(currentQuery))
            {
                sb.Append(string.Format("<a href=\"{0}?page={1}&sort={2}&asc={3}&query={4}\">{5}</a>", controller, currentPage, sort, isAsc, currentQuery, headerName));
            }
            else
            {
                sb.Append(string.Format("<a href=\"{0}?page={1}&sort={2}&asc={3}\">{4}</a>", controller, currentPage, sort, isAsc, headerName));
            }

            if (sort == currentSort)
            {
                if (currentAsc)
                {
                    sb.Append(string.Format("&nbsp;<span class=\"glyphicon glyphicon-circle-arrow-up\"></span>"));
                }
                else
                {
                    sb.Append(string.Format("&nbsp;<span class=\"glyphicon glyphicon-circle-arrow-down\"></span>"));
                }
            }

            return new HtmlString(sb.ToString());
        }
    }
}
