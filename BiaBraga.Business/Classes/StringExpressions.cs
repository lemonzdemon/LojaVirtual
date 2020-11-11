using System;
using System.Collections.Generic;
using System.Text;

namespace BiaBraga.Repository.Classes
{
    public static class StringExpressions
    {
        public static string ClearCNPJ(this string value)
        {
            return value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}
