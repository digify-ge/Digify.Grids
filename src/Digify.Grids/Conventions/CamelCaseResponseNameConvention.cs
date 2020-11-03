namespace Digify.DataGrid
{
    public class CamelCaseResponseNameConvention
    {
        public string Draw { get { return "draw"; } }
        public string TotalRecords { get { return "recordsTotal"; } }
        public string TotalRecordsFiltered { get { return "recordsFiltered"; } }
        public string Data { get { return "data"; } }
        public string Error { get { return "error"; } }
    }
}