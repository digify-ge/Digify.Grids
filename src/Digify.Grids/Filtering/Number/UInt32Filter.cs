using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class UintFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            uint number;
            if (uint.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
