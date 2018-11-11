namespace GameServer
{
    public struct Entity
    {
        public string name;

        public string lastName;
        
        // Position and Orientation
        public float x;
        public float y;
        public float z;
        
        public float rotX;
        public float rotY;
        public float rotZ;
        
        // Stats
        public int health;
        public int mana;
        public int stamina;
       
    }
}