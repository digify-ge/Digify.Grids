using System;

namespace Digify.DataGrid.Filtering.Number
{
    public class doubleFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            double number;
            if (double.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
