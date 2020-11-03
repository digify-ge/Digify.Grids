using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digify.DataGrid.Filtering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Digify.DataGrid.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddItnGrid(this IServiceCollection services)
        {
            return services.AddItnGrid(filters => { });
        }
        public static IServiceCollection AddItnGrid(this IServiceCollection services, Action<IGridFilters> configure)
        {
            IGridFilters filters = new GridFilters();
            configure(filters);
         
            return services.AddSingleton(filters);
        }
    }
}
