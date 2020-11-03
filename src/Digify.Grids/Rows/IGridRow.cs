namespace ITN.TS.DataGrid.Rows
{
    public interface IGridRow<out T>
    {
        string CssClasses { get; set; }
        T Model { get; }
    }
}
