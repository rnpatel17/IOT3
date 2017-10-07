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
    public class GroupsController : BaseController
    {
        IOTDBContext db = new IOTDBContext();
        ILocalHueClient _client;
        bool _isInitialized;
        public void InitializeHue()
        {
            _isInitialized = false;
            //initialize client with bridge IP and app GUID for Philips
            _client = new LocalHueClient(BRIDGE_IP);
            _client.Initialize(APP_ID);
            //only working with light #1 in this demo



            _isInitialized = true;

        }
        

        public GroupsController()
        {
            InitializeHue();
        }
        public async Task<List<Group>> GetAllGroup()
        {
            if (_isInitialized)
            {
                var allGroups = await _client.GetGroupsAsync();
                return allGroups.ToList();
            }
            else
            {
                return null;
            }

        }
        public async Task<Group> GetGroup(string id)
        {
            if (_isInitialized)
            {
                var groups = await _client.GetGroupAsync(id);
                int grpId = Convert.ToInt32(id);
                var group = db.Group.Find(grpId);
                if (group!=null)
                {

                return groups;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public async Task<HueResults> UpdateGroup(string id,IEnumerable<string> lights)
        {
            if (_isInitialized)
            {
                var updateGroups = await _client.UpdateGroupAsync(id,lights);
                return updateGroups;
            }
            else
            {
                return null;
            }

        }
        public async Task<string> CreateGroup(IEnumerable<string> light)
        {
            if (_isInitialized)
            {
                var createGroups = await _client.CreateGroupAsync(light);
                return createGroups;
            }
            else
            {
                return null;
            }

        }
        public async Task<HueResults> SendGroupCommand(ICommandBody commandBody)
        {
            if (_isInitialized)
            {
                var sendGroupsCommand = await _client.SendGroupCommandAsync(commandBody);
                return sendGroupsCommand;
            }
            else
            {
                return null;
            }

        }
        public async Task<HueResults> DeleteGroup(string id)
        {
            if (_isInitialized)
            {
                var deleteGroups = await _client.DeleteGroupAsync(id);
                return deleteGroups;
            }
            else
            {
                return null;
            }

        }

    }
}
