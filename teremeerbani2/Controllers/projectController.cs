using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.NameObjectCollectionBase;

namespace teremeerbani2.Controllers
{
    public class projectController : Controller
    {
        // GET: project
        int totalItems = 0;
       


        int pageSize = 9;

        public projectController()
        {
            
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult showdata(string name)
        {

            ViewBag.name = name;
           

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Images/SURGICAL_INSTRUMENTS/"+name+"/"));
            IList<string> abc = new List<string>();
            foreach (string filePath in filePaths)
            {
                abc.Add(Path.GetFileName(filePath));
            }
            
          

            if (abc != null)
            {
                //var pagedData = content.ToPagedList(pageSize, page);

                return View(abc);
            }
            else
            {
                return View("UPDATE");
            }
        }


        public ActionResult showdata2(string name)
        {
            ViewBag.name = name;


            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Images/BEAUTY_INSTRUMENT/" + name + "/"));
            IList<string> abc = new List<string>();
            foreach (string filePath in filePaths)
            {
                abc.Add(Path.GetFileName(filePath));
            }



            if (abc != null)
            {
                //var pagedData = content.ToPagedList(pageSize, page);

                return View(abc);
            }
            else
            {
                return View("UPDATE");
            }

        }


        public ActionResult showdata3(string name)
        {
            ViewBag.name = name;

            
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Images/DENTAL_INSTRUMENTS/" + name + "/"));
            IList<string> abc = new List<string>();
            foreach (string filePath in filePaths)
            {
                abc.Add(Path.GetFileName(filePath));
            }



            if (abc != null)
            {
                //var pagedData = content.ToPagedList(pageSize, page);

                return View(abc);
            }
            else
            {
                return View("UPDATE");
            }
        }

        public int addToCart(string count, string url)
        {

            cart c1 = new cart();
            c1.count = count;
            c1.URL = url;
            

            

            CookieOptions cb = new CookieOptions();
            cb.Expires = DateTime.Now.AddDays(1);

            //totalItems = Request.Cookies["totalItems"];

            totalItems = Request.Cookies.Count;

            if (totalItems != 0)
            {
                //Request.Cookies.Add(cb);
                //string list = Request.Cookies.Last().Key;
                //int key = Convert.ToInt16(list);
                //key = key + 1;
                //string name = key.ToString();
                //Response.Cookies.Append(name, item1, cb);
                ////Response.Cookies.Append("totalItmes", name, cb);

                string key = DateTime.Now.ToString();

                string nameCookie = (key).ToString();

                c1.key = nameCookie;
                string item1 = JsonConvert.SerializeObject(c1);

                HttpCookie cookie = new HttpCookie(nameCookie);
                cookie.Value = item1;


                cookie.Expires = System.DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

            }

            else
            {
                //Response.Cookies.Append("1", item1, cb);
                HttpCookie cookie = new HttpCookie("1");
                c1.key = "1";
                string item1 = JsonConvert.SerializeObject(c1);
                cookie.Value = item1;
                cookie.Expires = System.DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

            }

            return 1;
        }

        public ActionResult cartitem()
        {
            KeysCollection list = Request.Cookies.Keys;

            IList<cart> allObj = new List<cart>();


            foreach (string a in list)
            {
                string obj = Request.Cookies[a].Value;
                cart obj2 = JsonConvert.DeserializeObject<cart>(obj);

                allObj.Add(obj2);
                

            }
            ViewBag.item_Keys = list;

            return View("cartitem",allObj);
        }

        public int checkout(string username, string phone, string email, string address)
        {

            KeysCollection list = Request.Cookies.Keys;

            IList<cart> allObj = new List<cart>();


            foreach (string a in list)
            {
                string obj = Request.Cookies[a].Value;
                cart obj2 = JsonConvert.DeserializeObject<cart>(obj);
                allObj.Add(obj2);

            }


            KeysCollection items_keys = Request.Cookies.Keys;
            MailMessage EmailObject = new MailMessage();
            EmailObject.From = new MailAddress("fahadsherwani01@gmail.com", "Order");
            EmailObject.To.Add(new MailAddress("ashfaqproducts09@gmail.com", "Pakistan@123.."));

            EmailObject.Subject = username;
            //EmailObject.Body = "<h1>this is body of the email</h1>";
            EmailObject.IsBodyHtml = true;


            string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            body += "</HEAD><BODY><div>Name  :'" + username + "'</div><div>Address  :'" + address + "'</div><div>Email  :'" + email + "'</div><div>Phone  :'" + phone + "'</div><table class='table table-responsive'>";



            int i = 0;

            List<LinkedResource> resources = new List<LinkedResource>();

            foreach (var item in allObj)
            {
                string imageTag = string.Format("<tr><td><img src=cid:chart'" + i + "' /></td><td><h5>'" + item.count + "'</h5></td></tr>");
                body += imageTag;
                
                LinkedResource img = new LinkedResource(Server.MapPath(item.URL), System.Net.Mime.MediaTypeNames.Image.Jpeg);
                img.ContentId = "chart" + i;

                resources.Add(img);
                i++;
            }

            body += "</table></BODY></HTML>";





            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            //LinkedResource logo = new LinkedResource("wwwroot/BEAUTY_INSTRUMENT/CUTICLEE SCISSORS/cuticle-nippers-1419075075-RB-7701.jpg");
            //logo.ContentId = "companylogo";

            //htmlView.LinkedResources.Add(logo);

            resources.ForEach(x => htmlView.LinkedResources.Add(x));

            EmailObject.AlternateViews.Add(htmlView);
            EmailObject.Body = body;
            SmtpClient SC = new SmtpClient("smtp.gmail.com", 587);
            SC.Credentials = new System.Net.NetworkCredential("ashfaqproducts09@gmail.com", "Pakistan@123..");
            SC.EnableSsl = true;
            SC.Send(EmailObject);

            return 1;
        }



        public ActionResult MyCart(string key)
        {

            HttpCookie cb = new HttpCookie(key);
            cb.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cb);
            return RedirectToAction("cartitem");
        }

        public ActionResult userForm()
        {
            return View();
        }

        public ActionResult ZoomCheck(string url, string name, string item)
        {
            ViewBag.name = name;
            ViewBag.item = item;
            ViewBag.url = url;
            return View();
        }

    }
}