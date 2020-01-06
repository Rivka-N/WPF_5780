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
            Closed,
            Mailed,
            Started          
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
    }



}