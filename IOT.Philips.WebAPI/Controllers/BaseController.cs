using IOT.Repository;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
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

        public const string BRIDGE_IP = "192.168.10.137";///"192.168.1.225";//"192.168.0.8";// "192.168.0.1";
            //Ip of philips hue device
        public const string APP_ID = "Rlm7rHV0vJ1JxAxHZFLvGO7SJIgKJhslh8NPoF7O";// "1d0de40d14861c71a21c0c923d8a9e3";//"001788fffe7b215e";
        public ILocalHueClient _client;                                                            // app id of philips bridge 
        public bool _isInitialized;
        public BaseController()
        {
            InitializeHue();
        }
        public void InitializeHue()
        {
            _isInitialized = false;
            //initialize client with bridge IP and app GUID
            _client = new LocalHueClient(BRIDGE_IP);
            _client.Initialize(APP_ID);

            // var test = _client.RegisterAsync("WebAPP","WebDevice");



            _isInitialized = true;

        }
    }
}
