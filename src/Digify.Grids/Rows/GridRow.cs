namespace Digify.DataGrid.Rows
{
    public class GridRow<T> : IGridRow<T>
    {
        public string CssClasses { get; set; }
        public T Model { get; set; }

        public GridRow(T model)
        {
            Model = model;
        }
    }
}
