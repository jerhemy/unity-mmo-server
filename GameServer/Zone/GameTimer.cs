using System;

namespace GameServer.Zone
{
    public class GameTimer
    {
        private uint time_start;
        private uint time_end;

        private uint last_time_end;
        
        public GameTimer()
        {
        
        }

        public void Start(uint endTime)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            time_start = (uint)t.TotalSeconds;
            last_time_end = endTime;
            time_end = time_start + endTime;
        }

        public bool Check()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var currentTime = (uint)t.TotalSeconds;
            if (time_end < currentTime)
                return true;

            return false;
        }

        public void Reset()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            time_start = (uint)t.TotalSeconds;
            time_end = time_start + last_time_end;            
        }
        
    }
}