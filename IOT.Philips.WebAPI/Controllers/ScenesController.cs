using IOT.Repository;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models;
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
    public class ScenesController : BaseController
    {
        IOTDBContext db = new IOTDBContext();
        //command keys for queue

        //ILocalHueClient _client;
        //bool _isInitialized;

        //public ScenesController()
        //{

        //    InitializeHue();
        //    //initialize hue


        //}
        //public void InitializeHue()
        //{
        //    _isInitialized = false;
        //    //initialize client with bridge IP and app GUID
        //    _client = new LocalHueClient(BRIDGE_IP);
        //    _client.Initialize(APP_ID);
        //    //only working with light #1 in this demo
        //    _isInitialized = true;

        //}
        public async Task<List<Scene>> GetAllScene()
        {
            if (_isInitialized)
            {
                var allScene = await _client.GetScenesAsync();
                //get all scene details
                return allScene.ToList();
            }
            else
            {
                return null;
            }

        }
        public async Task<Scene> GetScene(string id)
        {
            if (_isInitialized)
            {
                var scene = await _client.GetSceneAsync(id);
                //get scene by id
                return scene;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CreateScene(Scene scene)
        {
            if (_isInitialized)
            {
                var createScene = await _client.CreateSceneAsync(scene);
                //create scene 
                return createScene;
            }
            else
            {
                return null;
            }
        }

        public async Task<HueResults> UpdateScene(string id, Scene scene)
        {
            if (_isInitialized)
            {
                var updateScene = await _client.UpdateSceneAsync(id, scene);
                //update scene by id
                return updateScene;
            }
            else
            {
                return null;
            }
        }

        public async Task<HueResults> DeleteScene(string id)
        {
            if (_isInitialized)
            {
                var deleteScene = await _client.DeleteSceneAsync(id);
                //delete scene by id
                return deleteScene;
            }
            else
            {
                return null;
            }
        }
        public async Task<HueResults> ModifyScene(string id,string lightId,LightCommand command)
        {
            if (_isInitialized)
            {
                var modifyScene = await _client.ModifySceneAsync(id,lightId,command);
                //modify scene by id,lightId and command
                return modifyScene;
            }
            else
            {
                return null;
            }
        }
        public async Task<HueResults> RecallScene(string id)
        {
            if (_isInitialized)
            {
                var recallScene = await _client.RecallSceneAsync(id);
                //recall scene by id
                return recallScene;
            }
            else
            {
                return null;
            }
        }
    }
}
