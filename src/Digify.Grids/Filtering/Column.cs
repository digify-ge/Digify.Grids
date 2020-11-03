namespace ITN.TS.DataGrid.Filtering
{
    public class Column
    {
        public Sort Sort { get; private set; }
        public string ColumnField { get; set; }
        public string Name { get; set; }
        public bool IsSearchable { get; set; }
        public bool IsSortable { get; set; }
        public SearchQuery SearchQuery { get; set; }
        public bool SetSort(int order, string direction)
        {
            if (!IsSortable) return false;

            Sort = new Sort(order, direction);
            return true;
        }
    }
}