using IOT.Philips.WebAPI.Models;
using IOT.Repository;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
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

        //private Dictionary<string, LightCommand> _commandQueue;


        Light _light;
        List<string> _lightList;

        //IHueClient hueClient;
        //public LightController()
        //{

        //    InitializeHue();
        //    //initialize hue

        //    //initialize command queue
        //    //  _commandQueue = new Dictionary<string, LightCommand>();

        //}

        public async Task<bool> OnOff(bool IsOn, string id)
        {
            try
            {
                if (_isInitialized)
                {
                    //_light = await _client.GetLightAsync(id);
                    if (id!=null)
                    {
                        _lightList = new List<string>();
                        _lightList.Add(id);
                    }
                    else
                    {
                        _lightList = null;
                    }
                    var command = new LightCommand();
                    command.On = IsOn;

                    command.TransitionTime = new TimeSpan(0, 0, 0, 0, 1);
                    if (IsOn)
                    {

                   command.TurnOn();
                    }
                    else
                    {
                        command.TurnOff();
                    }
                    command.Alert = Alert.Once;

                    //Or start a colorloop
                    command.Effect = Effect.ColorLoop;
                    //_light.State.On = IsOn;
                    //queue power command

                    var test= await _client.SendCommandAsync(command, _lightList);
                  //var ts= await _client.SendCommandAsync(cmd, _lightList);
                   

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

        //public async Task<List<Light>> GetNewLights()
        //{
        //    if (_isInitialized)
        //    {
        //        var newLight = await _client.GetNewLightsAsync();
        //        //get new Light details
        //        return newLight.ToList();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        public void sendCommand(LightCommand command,string id)
        {
            string data =string.Format("on: {0}",command.On);
       
            using (var client = new System.Net.WebClient())
            {
                client.UploadData(BRIDGE_IP+"/lights/"+id+"state", "PUT", null);
            }
            // var test = _client.SendCommandAsync(command, lightList);
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
        public async Task<HueResults> SetLightName(string id, string name)
        {
            if (_isInitialized)
            {
                var rename = await _client.SetLightNameAsync(id, name);
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
            //if (_commandQueue.ContainsKey(commandType))
            //{
            //    //replace with most recent
            //    _commandQueue[commandType] = cmd;
            //}
            //else
            //{
            //    _commandQueue.Add(commandType, cmd);
            //}

        }
    }
}
