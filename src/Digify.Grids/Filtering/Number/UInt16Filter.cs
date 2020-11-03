using System;

namespace Digify.DataGrid.Filtering.Number
{
    public class ushortFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            ushort number;
            if (ushort.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
