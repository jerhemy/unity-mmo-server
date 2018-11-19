namespace GameServer.World
{
    public enum WorldMessageType
    {
        Authenticate = 0x0001,
        OP_CharacterCreate = 0x0002,
        OP_CharacterCreateRequest = 0x0003,
        OP_CharacterDelete = 0x0004,
        OP_CharacterList = 0x0005,      
        
        OP_Login = 0x0006,
        OP_LoginAccepted = 0x0007,
        OP_LoginComplete = 0x0008,
        OP_Logout = 0x0009
    }
}