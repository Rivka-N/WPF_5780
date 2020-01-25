namespace BE
{
    public static class Enums
    {
        public enum HostingUnitType
        {
            Zimmer,
            Hotel,
            Camping,
        }

        public enum DataSourseType
        {
            List,
            XML
        } 
        public enum OrderStatus
        { 
            Mailed,
            Closed,
            Started,
            Expired
        }
        public enum MealType
        {
            Full,
            Half,
            None
        }
        public enum Preference
        {
            Yes,
            No,
            Maybe
        }
        public enum Area
        {
            Center,
            Galil,
            Golan,
            South,
            Eilat
        }
        public enum FunctionSender//to help the search functions
        {
            Default,
            Owner,
            AddGuest,
            HostList,
            Host
        }
    }



}