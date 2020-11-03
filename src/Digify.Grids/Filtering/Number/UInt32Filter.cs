using System;

namespace Digify.DataGrid.Filtering.Number
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
