using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITN.TS.DataGrid.Grids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITN.TS.DataGrid.Extension
{
    public static  class HtmlExtension
    {
        public static HtmlGrid<T> Grid<T>(this IHtmlHelper html, IEnumerable<T> source, string partialName = "Grids/_NewGrid") where T : class
        {
            return new HtmlGrid<T>(html, new Grid<T>(source), partialName);
        }
    }
}
