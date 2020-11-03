﻿using System;

namespace ITN.TS.DataGrid.Filtering.Number
{
    public class Int64Filter : NumberFilter
    {
        public override object GetNumericValue()
        {
            long number;
            if (long.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
