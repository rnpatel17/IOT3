using IOT.Philips.WebAPI.Models;
using IOT.Repository;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IOT.Philips.WebAPI.Controllers
{
    public class LightController : BaseController
    {
        IOTDBContext db = new IOTDBContext();
        //command keys for queue
        private const string ON_OFF = "ON_OFF";
        //private const string BRIGHTNESS = "BRIGHTNESS";
        //private const string COLOR = "COLOR";

        private Dictionary<string, LightCommand> _commandQueue;
        //DispatcherTimer _timer; //timer for queue

        ILocalHueClient _client;
        Light _light;
        List<string> _lightList;
        bool _isInitialized;
        //protected override async void OnNavigatedTo()
        //{
        //    base.OnNavigatedTo();

        //    string appId = "";
        //    string clientId = "";
        //    string clientSecret = "";

        //    IRemoteAuthenticationClient authClient = new RemoteAuthenticationClient(clientId, clientSecret, appId);



        //    var authorizeUri = authClient.BuildAuthorizeUri("sample", "consoleapp");
        //    var callbackUri = new Uri("https://localhost/q42hueapi");

        //    var webAuthenticationResult = await Windows.Security.Authentication.Web WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authorizeUri, callbackUri);

        //    if (webAuthenticationResult != null)
        //    {
        //        var result = authClient.ProcessAuthorizeResponse(webAuthenticationResult.ResponseData);

        //        if (!string.IsNullOrEmpty(result.Code))
        //        {
        //            //You can store the accessToken for later use
        //            var accessToken = await authClient.GetToken(result.Code);

        //            IRemoteHueClient client = new RemoteHueClient(authClient.GetValidToken);
        //            var bridges = await client.GetBridgesAsync();

        //            if (bridges != null)
        //            {
        //                //Register app
        //                //var key = await client.RegisterAsync(bridges.First().Id, "Sample App");

        //                //Or initialize with saved key:
        //                client.Initialize(bridges.First().Id, "C95sK6Cchq2LfbkbVkfpRKSBlns2CylN-VxxDD8F");

        //                //Turn all lights on
        //                var lightResult = await client.SendCommandAsync(new LightCommand().TurnOn());

        //            }
        //        }
        //    }
        //}

        public LightController()
        {

            InitializeHue(); //fire and forget, don't wait on this call// uncomment local

            //initialize command queue
            _commandQueue = new Dictionary<string, LightCommand>();

        }
        public void InitializeHue()
        {
            _isInitialized = false;
            //initialize client with bridge IP and app GUID
            _client = new LocalHueClient(BRIDGE_IP);
            _client.Initialize(APP_ID);
            //only working with light #1 in this demo



            _isInitialized = true;

        }
        public async Task<bool> OnOff(bool IsOn, string id)
        {
            try
            {
                if (_isInitialized)
                {
                    _light = await _client.GetLightAsync(id);
                    _lightList = new List<string>() { id };
                    _light.State.On = IsOn;
                    //queue power command
                    LightCommand cmd = new LightCommand();
                    cmd.On = IsOn;
                    QueueCommand(ON_OFF, cmd);

                    var light = db.Light.Find(id);
                    var state = db.State.Find(light.StateId);

                    if (state != null)
                    {
                        state.On = IsOn;
                        db.SaveChanges();
                        // model.TurnOnAction();
                        return state.On;
                    }
                    else
                    {
                        return IsOn;
                    }

                }
                else
                {
                    return IsOn;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public async Task<List<Light>> GetAllLights()
        {
            if (_isInitialized)
            {
                var allLight = await _client.GetLightsAsync();
                return allLight.ToList();
            }
            else
            {
                return null;
            }

        }

        public async Task<List<Light>> GetNewLights()
        {
            if (_isInitialized)
            {
                var newLight = await _client.GetNewLightsAsync();
                return newLight.ToList();
            }
            else
            {
                 return null;
            }
        }

        public async Task<HueResults> SearchForNewLights()
        {
            if (_isInitialized)
            {
                var searchNewLight = await _client.SearchNewLightsAsync();
                return searchNewLight;
            }
            else
            {
                return null;
            }
        }
        public async Task<HueResults> SetLightName(string id,string name)
        {
            if (_isInitialized)
            {
                var rename = await _client.SetLightNameAsync(id,name);
                return rename;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<DeleteDefaultHueResult>> DeleteLight(string id)
        {
            if (_isInitialized)
            {
                var deleteDefaultHueResult = await _client.DeleteLightAsync(id);
                return deleteDefaultHueResult.ToList();
            }
            else
            {
                return null;
            }
        }


        private void QueueCommand(string commandType, LightCommand cmd)
        {
            if (_commandQueue.ContainsKey(commandType))
            {
                //replace with most recent
                _commandQueue[commandType] = cmd;
            }
            else
            {
                _commandQueue.Add(commandType, cmd);
            }

        }
    }
}
