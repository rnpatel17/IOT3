using IOT.Philips.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //LightController light = new LightController();
            //var test=light.OnOff(false,"1");
            // light number 
            //light.GetAllLights("test");
           // light.LightOnOff("username", 1,true);
           // light.DeleteLights("test", 7);
            return View();
        }
        [HttpPost]
        public ActionResult LightOnOff(string id,bool isOn)
        {
            LightController light = new LightController();

            var test =id!=null && id!=string.Empty?light.OnOff(isOn, id): light.OnOff(isOn, null);
            return Json(test.Result,JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}