using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class UInt64Filter : NumberFilter
    {
        public override object GetNumericValue()
        {
            ulong number;
            if (ulong.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
