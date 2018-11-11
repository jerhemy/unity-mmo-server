namespace GameServer
{
    namespace GameServer
    {
        public enum MessageType
        {
            Movement      = 0x01,
            Chat          = 0x02,
            Command       = 0x03,
            Attack        = 0x04,
            UseAbility    = 0x05,
            CastSpell     = 0x06,
            Craft         = 0x07,
            Teleport      = 0x08,
            Emote         = 0x09
        }
    }
}