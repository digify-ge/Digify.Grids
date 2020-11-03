using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class SingleFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            float number;
            if (float.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
