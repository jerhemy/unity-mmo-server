using System.Collections.Generic;
using System.Net;
using LoginServer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetcodeIO.NET;

namespace LoginServer.Controllers
{
    
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        static readonly byte[] _privateKey = new byte[]
        {
            0x60, 0x6a, 0xbe, 0x6e, 0xc9, 0x19, 0x10, 0xea,
            0x9a, 0x65, 0x62, 0xf6, 0x6f, 0x2b, 0x30, 0xe4,
            0x43, 0x71, 0xd6, 0x2c, 0xd1, 0x99, 0x27, 0x26,
            0x6b, 0x3c, 0x60, 0xf4, 0xb7, 0x15, 0xab, 0xa1,
        };
        
        private IAccountService _accountService;

        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Account userParam)
        {
            var user = _accountService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _accountService.GetAll();
            return Ok(users);
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        public byte[] worldServerToken(int worldServerId)
        {
            
            List<IPEndPoint> addressList = new List<IPEndPoint>();
            addressList.Add(new IPEndPoint(IPAddress.Loopback, 8559));

            var serverAddress = addressList.ToArray();
            
            // Get World Server Data
//            TokenFactory tokenFactory = new TokenFactory(
//                1, // must be the same protocol ID as passed to both client and server constructors
//                _privateKey // byte[32], must be the same as the private key passed to the Server constructor
//            );
//            
//            
//            return tokenFactory.GenerateConnectToken(
//                serverAddress, // IPEndPoint[] list of addresses the client can connect to. Must have at least one and no more than 32.
//                2 * 60, // in how many seconds will the token expire
//                30, // how long it takes until a connection attempt times out and the client tries the next server.
//                1UL, // ulong token sequence number used to uniquely identify a connect token.
//                1UL, // ulong ID used to uniquely identify this client
//                new byte[256] // byte[], up to 256 bytes of arbitrary user data (available to the server as RemoteClient.UserData)
//            );

            return new byte[16];

        }
        
    }
}