using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class sbyteFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            sbyte number;
            if (sbyte.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
