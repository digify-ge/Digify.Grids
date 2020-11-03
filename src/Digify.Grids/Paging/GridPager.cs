using System;
using System.Linq;
using ITN.TS.DataGrid.Grids;
using ITN.TS.DataGrid.Processors;

namespace ITN.TS.DataGrid.Paging
{
    public class GridPager<T> : IGridPager<T>
    {
        public IGrid<T> Grid { get; set; }

        public int TotalRows { get; set; }
        public int PagesToDisplay { get; set; }

        public virtual int TotalPages => (int)Math.Ceiling(TotalRows / (double)RowsPerPage);

        public virtual int CurrentPage
        {
            get
            {
                int page;
                CurrentPageValue = int.TryParse(Grid.Query[Grid.Name + "-Page"], out page) ? page : CurrentPageValue;
                CurrentPageValue = CurrentPageValue > TotalPages ? TotalPages : CurrentPageValue;
                CurrentPageValue = CurrentPageValue <= 0 ? 1 : CurrentPageValue;

                return CurrentPageValue;
            }
            set
            {
                CurrentPageValue = value;
            }
        }
        public virtual int RowsPerPage
        {
            get
            {
                int rows;
                RowsPerPageValue = int.TryParse(Grid.Query[Grid.Name + "-Rows"], out rows) ? rows : RowsPerPageValue;
                RowsPerPageValue = RowsPerPageValue <= 0 ? 1 : RowsPerPageValue;

                return RowsPerPageValue;
            }
            set
            {
                RowsPerPageValue = value;
            }
        }
        public virtual int FirstDisplayPage
        {
            get
            {
                int middlePage = (PagesToDisplay / 2) + (PagesToDisplay % 2);
                if (CurrentPage < middlePage)
                    return 1;

                if (CurrentPage - middlePage + PagesToDisplay > TotalPages)
                    return Math.Max(TotalPages - PagesToDisplay + 1, 1);

                return CurrentPage - middlePage + 1;
            }
        }

        public string CssClasses { get; set; }
        public string PartialViewName { get; set; }
        public GridProcessorType ProcessorType { get; set; }

        private int CurrentPageValue { get; set; }
        private int RowsPerPageValue { get; set; }

        public GridPager(IGrid<T> grid)
        {
            Grid = grid;
            CurrentPage = 1;
            RowsPerPage = 20;
            PagesToDisplay = 5;
            PartialViewName = "Grids/_Pager";
            ProcessorType = GridProcessorType.Post;
        }

        public virtual IQueryable<T> Process(IQueryable<T> items)
        {
            TotalRows = items.Count();

            return items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage);
        }
    }
}
