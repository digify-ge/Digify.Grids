using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class intFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            int number;
            if (int.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
