using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExchangeDataAccess.ValidationAttributes
{
    class PastDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            DateTime dt = Convert.ToDateTime(value);
            return dt <= DateTime.Now;
        }
    }
}
