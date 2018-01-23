using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExchangeDataAccess.ValidationAttributes
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = Convert.ToDateTime(value);
            return dt >= DateTime.Now;
        }


    }
}
