using IOT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IOT.Philips.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
       
        public const string BRIDGE_IP = "192.168.20.106";///"192.168.1.225";//"192.168.0.8";// "192.168.0.1";
        public const string APP_ID = "hb7b215e002b8b8f";// "1d0de40d14861c71a21c0c923d8a9e3";//"001788fffe7b215e";


    }
}
