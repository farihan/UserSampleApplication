using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hans.UserSample.Web.Helpers
{
    public static class Pager
    {
        public static HtmlString PageLinks(this IHtmlHelper helper, int pageSize, int totalPages, int currentPage, string currentQuery)
        {
            var s = string.Empty;
            var controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            var action = helper.ViewContext.RouteData.Values["action"].ToString();
            var controllerName = string.Format(@"{0}/{1}", controller, action);
            var maxPages = 11;

            if (totalPages <= maxPages)
            {
                return new HtmlString(BuildLinks(helper, 1, totalPages, null, currentPage, controllerName, currentQuery));
            }

            var pageAfter = totalPages - currentPage;
            var pageBefore = currentPage - 1;

            if (pageAfter <= 4)
            {
                var sb = new StringBuilder();
                var pageSubset = totalPages - maxPages - 1 > 1 ? totalPages - maxPages - 1 : 2;

                sb.Append(string.Format("<nav aria-label=\"Page navigation example\">"));
                sb.Append(string.Format("<ul class=\"pagination\">"));
                sb.Append(BuildLinks(helper, 1, 1, null, currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, pageSubset, pageSubset, "...", currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, totalPages - maxPages + 3, totalPages, null, currentPage, controllerName, currentQuery));
                sb.AppendLine(string.Format("</ul>"));
                sb.AppendLine(string.Format("</nav>"));

                return new HtmlString(sb.ToString());
            }

            if (pageBefore <= 4)
            {
                var sb = new StringBuilder();
                var pageSubset = maxPages + 2 < totalPages ? maxPages + 2 : totalPages - 1;

                sb.Append(string.Format("<nav aria-label=\"Page navigation example\">"));
                sb.Append(string.Format("<ul class=\"pagination\">"));
                sb.Append(BuildLinks(helper, 1, maxPages - 2, null, currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, pageSubset, pageSubset, "...", currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, totalPages, totalPages, null, currentPage, controllerName, currentQuery));
                sb.AppendLine(string.Format("</ul>"));
                sb.AppendLine(string.Format("</nav>"));

                return new HtmlString(sb.ToString());
            }

            if (pageAfter > 4)
            {
                var sb = new StringBuilder();

                var pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                var pageSubset2 = currentPage + 7 < totalPages ? currentPage + 7 : totalPages - 1;

                sb.Append(string.Format("<nav aria-label=\"Page navigation example\">"));
                sb.Append(string.Format("<ul class=\"pagination\">"));
                sb.Append(BuildLinks(helper, 1, 1, null, currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...", currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, currentPage - 3, currentPage + 3, null, currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, pageSubset2, pageSubset2, pageSubset2 == currentPage - 4 ? null : "...", currentPage, controllerName, currentQuery));
                sb.Append(BuildLinks(helper, totalPages, totalPages, null, currentPage, controllerName, currentQuery));
                sb.AppendLine(string.Format("</ul>"));
                sb.AppendLine(string.Format("</nav>"));

                return new HtmlString(sb.ToString());
            }

            return new HtmlString(s);
        }

        private static string BuildLinks(IHtmlHelper helper, int start, int end, string innerContent, int currentPage, string controllerName, string currentQuery)
        {
            var request = helper.ViewContext.HttpContext.Request;
            var sb = new StringBuilder();

            for (int i = start; i <= end; i++)
            {
                var qs = SetupQueryString(request, i, currentQuery);

                if (i == currentPage)
                {
                    sb.Append(string.Format("<li class=\"page-item disabled\"><a class=\"page-link\" href=\"/{0}{1}\">{2}</a></li>", controllerName, qs, i));
                }
                else
                {
                    sb.Append(string.Format("<li class=\"page-item\"><a class=\"page-link\" href=\"/{0}{1}\">{2}</a></li>", controllerName, qs, innerContent ?? i.ToString()));
                }
            }

            return sb.ToString();
        }

        private static string SetupQueryString(HttpRequest request, int pageNumber, string currentQuery)
        {
            var builder = new StringBuilder();

            builder.Append(string.Format("?page={0}", pageNumber));

            if (!string.IsNullOrEmpty(request.HttpContext.Request.Query["sort"]))
            {
                builder.Append(string.Format("&sort={0}", request.HttpContext.Request.Query["sort"].ToString().ToLower()));
            }

            if (!string.IsNullOrEmpty(request.HttpContext.Request.Query["asc"]))
            {
                builder.Append(string.Format("&asc={0}", request.HttpContext.Request.Query["asc"]));
            }

            if (!string.IsNullOrEmpty(currentQuery))
            {
                builder.Append(string.Format("&query={0}", currentQuery));
            }

            return builder.ToString();
        }
    }
}
