using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class factoryDAL
    {
        public static IDAL getDAL()
        { 
            return DALList.Instance;
                //  return DALXML.Instance;
           
        }
    }
}
