using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digify.DataGrid.Grids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digify.DataGrid.Extension
{
    public static  class HtmlExtension
    {
        public static HtmlGrid<T> Grid<T>(this IHtmlHelper html, IEnumerable<T> source, string partialName = "Grids/_NewGrid") where T : class
        {
            return new HtmlGrid<T>(html, new Grid<T>(source), partialName);
        }
    }
}
