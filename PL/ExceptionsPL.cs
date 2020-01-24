using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    #region number exceptions

    class LargeNumberExceptionPL : Exception//number too big
    {
        public LargeNumberExceptionPL()
        {
        }

        public LargeNumberExceptionPL(string message) : base(message)
        {
        }
    }
    class negativeNumberExceptionPL:Exception//number negative
    {

    }
    #endregion
    class invalidTypeExceptionPL :Exception//data doesn't match expected data type (string instead of number etc)
    {

    }
 

}
