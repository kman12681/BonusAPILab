using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BonusAPILab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
                

        public ActionResult DayAndMonth()
        {
            return View();
        }

        public ActionResult NumbersAPI(int day, int month)
        {                       
            HttpWebRequest WR = WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/{day}/{month}/date?fragment=true&json=true");
            WR.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            WR.Headers.Add("X-Mashape-Key", "Y98sawFqy0mshQjloz0p5bvsLaGAp1PD8hmjsnCK6nJbIOBCBs");

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string data = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(data);
                ViewBag.Fact = JsonData["text"];              


            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }                    
          
                return View();
            

        }
    }
}