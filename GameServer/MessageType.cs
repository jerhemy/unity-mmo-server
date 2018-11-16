namespace GameServer
{
    namespace GameServer
    {
        public enum MessageType
        {
            Movement      = 0x0001,
            Chat          = 0x02,
            Command       = 0x03,
            Attack        = 0x04,
            UseAbility    = 0x05,
            CastSpell     = 0x06,
            Craft         = 0x07,
            Teleport      = 0x08,
            Emote         = 0x09,
            Entity        = 0x10,
            
            
            OP_EnterWorld = 0x578f,
            
            OP_HPUpdate = 0x2828,
            OP_ManaUpdate = 0x3791,
            OP_EnduranceUpdate= 0x5f42,
            
            OP_BeginCast = 0x318f,
            OP_CastSpell = 0x1287,
            OP_ManaChange = 0x5467,
            OP_InterruptCast = 0x048c,
            
            OP_Damage = 0x6f15,
            
            OP_Save = 0x4a39,
            OP_ClickDoor = 0x3a8f,
            OP_MoveDoor = 0x08e8,
            OP_WearChange = 0x7994,
            
            OP_ZoneChange = 0x2d18,
            OP_Logout = 0x4ac6,
            
            OP_ClientReady = 0x345d,
            OP_ClientUpdate = 0x7dfc,
            OP_WhoAllRequest = 0x674b,
          
            OP_BoardBoat = 0x4211,
            OP_LeaveBoat = 0x7617
        }
    }
}