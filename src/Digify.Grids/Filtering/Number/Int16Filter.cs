﻿using System;

namespace Digify.DataGrid.Filtering.Number
{
    public class shortFilter : NumberFilter
    {
        public override object GetNumericValue()
        {
            short number;
            if (short.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
