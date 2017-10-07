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
            //initialize hue
        }
        public async Task<List<Group>> GetAllGroup()
        {
            if (_isInitialized)
            {
                var allGroups = await _client.GetGroupsAsync();
                //get all group details
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
                //get group by Id
                int grpId = Convert.ToInt32(id);
                var group = db.Group.Find(grpId);
                //find group object by id from database
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
                //update group by id and lights
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
                //create group with number of light
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
                // send send command for group with body
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
                //delete group by id
                return deleteGroups;
            }
            else
            {
                return null;
            }

        }

    }
}
