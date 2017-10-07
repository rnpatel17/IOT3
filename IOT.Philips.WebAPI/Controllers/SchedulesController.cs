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
    public class SchedulesController : BaseController
    {
        IOTDBContext db = new IOTDBContext();
        //command keys for queue
       
        ILocalHueClient _client;
        bool _isInitialized;

        public SchedulesController()
        {

            InitializeHue();
            //initialize hue


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
        public async Task<List<Schedule>> GetAllSchedules()
        {
            if (_isInitialized)
            {
                var allSchedule = await _client.GetSchedulesAsync();
                //get all schedules details
                return allSchedule.ToList();
            }
            else
            {
                return null;
            }

        }
        public async Task<Schedule> GetSchedule(string id)
        {
            if (_isInitialized)
            {
                var schedule = await _client.GetScheduleAsync(id);
                //get schedule by id
                return schedule;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CreateSchedule(Schedule schedule)
        {
            if (_isInitialized)
            {
                var createSchedule = await _client.CreateScheduleAsync(schedule);
                //create schedule 
                return createSchedule;
            }
            else
            {
                return null;
            }
        }

        public async Task<HueResults> UpdateSchedule(string id,Schedule schedule)
        {
            if (_isInitialized)
            {
                var updateSchedule = await _client.UpdateScheduleAsync(id,schedule);
                //update schedule by id
                return updateSchedule;
            }
            else
            {
                return null;
            }
        }

        public async Task<HueResults> DeleteSchedule(string id)
        {
            if (_isInitialized)
            {
                var deleteSchedule = await _client.DeleteScheduleAsync(id);
                //delete schedule by id
                return deleteSchedule;
            }
            else
            {
                return null;
            }
        }
    }
}
