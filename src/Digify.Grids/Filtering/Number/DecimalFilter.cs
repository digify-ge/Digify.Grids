using System;

namespace Digify.DataGrid.Filtering.Number
{
    public class DecimalFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            Decimal number;
            if (Decimal.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
