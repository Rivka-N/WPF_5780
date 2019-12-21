

using BE;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        void addGuest(GuestRequest guest);

        #region HostingUnit
        //List<HostingUnit> getAllHostingUnits();
        #endregion
    }
}