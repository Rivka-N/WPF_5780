using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class factoryBL
    {
        public static IBL getBL()
        { return MyBL.Instance; }
    }
}
