using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class byteFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            byte number;
            if (byte.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
