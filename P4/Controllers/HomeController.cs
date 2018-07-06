using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P4.Entities;
using P4.Models;
namespace P4.Controllers
{
    
    
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if((Session["login"]!=null))
            {

            } else
            {
                Session["login"] = false;
            }
            
            return View();
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
        public JsonResult Master()
        {

            try
            {
                channelEntities p1 = new channelEntities();
                classPro1 modelP1 = new classPro1();
                modelP1.rooms = (
                        from full in p1.rooms.AsEnumerable()
                        select new
                        {

                            cName1 = full.cName,

                        }).ToList();
                return Json(new { status = "200", message = "success", data = modelP1.rooms });
            }
            catch (Exception ex)
            { return Json(new { status = "500", message = ex.Message ,data =""}); }

        }
        public ActionResult Login(string ffName)
        {


            try
            {
                dynamic item = JsonConvert.DeserializeObject(ffName);
                string fName = item.fName;
                string lName = item.lName;
                channelEntities p1 = new channelEntities();
                classPro1 modelP1 = new classPro1();
                modelP1.users = (
                        from full in p1.users.AsEnumerable()
                        where full.userName == fName && full.userPass == lName
                        select new
                        {
                           
                            userName1 = full.userName,
                            userPass1 = full.userPass

                        }).First();
               

               
                if (modelP1.users != null)
                {

                    Session["ID"] = fName;
                    Session["PASS"] = lName;
                    user model = p1.users.FirstOrDefault(x => x.userName == fName && x.userPass == lName);
                    model.status = true;
                    Session["login"] = true;
                    p1.SaveChanges();

                    return Json(new { status = "200", message = "success"});
                }
                else
                {
                    Session["login"] = false;
                    return Json(new { status = "500", message = "cannot login" });
                }

                


            }
            catch (Exception ex)
            {
                Session["login"] = false;
                return Json(new { status = "500", message = ex.Message}); }
        }

                public JsonResult Logout()
        {
            try
            {
                string test = Session["ID"].ToString();
                channelEntities p1 = new channelEntities();
                user model = p1.users.FirstOrDefault(x => x.userName == test);
                model.status = false;
                Session["login"] = false;
                p1.SaveChanges();
                return Json(new { status = "200", message = "success" });
            } catch (Exception ex)
            { return Json(new { status = "500", message = ex.Message }); }

        }


        public JsonResult ActionCreate(string ffName)
        {
            try
            {

                dynamic item = JsonConvert.DeserializeObject(ffName);

                channelEntities tfn = new channelEntities();

                user p1 = new user();
                p1.userName = item.fName;
                p1.userPass = item.lName;
                tfn.users.Add(p1);
                tfn.SaveChanges();

                return Json(new { status = "200", message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "500", message = ex.Message });
            }
        }
    }
}