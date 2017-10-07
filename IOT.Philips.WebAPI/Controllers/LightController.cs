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
       
        private Dictionary<string, LightCommand> _commandQueue;

        ILocalHueClient _client;
        Light _light;
        List<string> _lightList;
        bool _isInitialized;
       
        public LightController()
        {

            InitializeHue(); 
            //initialize hue

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
                    //find by Light Id 
                    var state = db.State.Find(light.StateId);
                    //find by State Id

                    if (state != null)
                        //if state object not equal to null then change state
                    {
                        state.On = IsOn;
                        db.SaveChanges();
                        //update state in database 
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
                //get all lights details
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
                //get new Light details
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
                // find new lights
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
                //rename light by light id
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
                // delete light by id
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
