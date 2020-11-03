using System;
using Digify.DataGrid.Filtering;
using Digify.DataGrid.ModelBindings;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Digify.DataGrid.Extension
{
    public class GridModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder ModelBinder { get; private set; }
        public GridModelBinderProvider() { }
        public GridModelBinderProvider(IModelBinder modelBinder) { ModelBinder = modelBinder; }
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (IsBindable(context.Metadata.ModelType))
            {
                if (ModelBinder == null) ModelBinder = new GridModelBinder();
                return ModelBinder;
            }
            else return null;
        }
        private bool IsBindable(Type type) { return type == typeof(DataTableRequest); }
    }
}