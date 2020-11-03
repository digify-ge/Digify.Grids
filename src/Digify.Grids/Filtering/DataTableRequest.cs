using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digify.DataGrid.Filtering
{
    public class DataTableRequest
    {
        public IEnumerable<Column> Columns { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public SearchQuery SearchQuery { get; set; }
        public int Start { get; set; }
        public IDictionary<string, object> AditionalParameters { get; set; }
    }
}
