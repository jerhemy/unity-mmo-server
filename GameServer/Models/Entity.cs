namespace GameServer.Models
{
    /// <summary>
    /// The main Math class.
    /// Contains all methods for performing basic math functions.
    /// </summary>
    public struct Entity
    {
        public int id;
        
        /* Base Character */
        public string name;

        /* Positioning */
        public string zone;
        public double x;
        public double y;
        public double z;
        public float  orientation;
    }
}