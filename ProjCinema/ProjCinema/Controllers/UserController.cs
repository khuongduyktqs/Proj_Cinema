using ProjCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjCinema.Controllers
{
    public class UserController : Controller
    {
        CinemaDB _db = new CinemaDB();
        public ActionResult Index()
        {
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
        }
        // GET: User
        [HttpGet]
        public ActionResult Register(string index)
        {
            ViewBag.index = index;
            return PartialView();

        }
        [HttpPost]
        public ActionResult RegisterResult(USER_ACCOUNT _user)
        {
            string name = Request.Form["user"];
            string pass = Request.Form["pass"];
            string repass = Request.Form["repass"];
            string email = Request.Form["email"];
            string a = _db.Database.SqlQuery<String>("exec check_ID").ToList()[0];
            string b = a.Substring(4, 4);
            int _id = Int32.Parse(b);

            if (_id < 10)
            {
                _id += 1;
                _user.UserID = string.Concat("USER000", _id.ToString());
            }
            else if (_id < 100 && _id >= 10)
            {
                _id += 1;
                _user.UserID = string.Concat("USER00", _id.ToString());
            }
            else if (_id < 1000 && _id >= 100)
            {
                _id += 1;
                _user.UserID = string.Concat("USER0", _id.ToString());
            }
            else if (_id < 10000 && _id >= 1000)
            {
                _id += 1;
                _user.UserID = string.Concat("USER", _id.ToString());
            }
            var check = _db.USER_ACCOUNT.FirstOrDefault(s => s.email == email);
            var check1 = _db.USER_ACCOUNT.FirstOrDefault(s => s.UserID == _user.UserID);
            var check2 = _db.USER_ACCOUNT.FirstOrDefault(s => s.Username == name);
            if (check == null && check1 == null && check2 == null)
            {
                if (pass == repass)
                {
                    _user.UserPassword = GetMD5(pass);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _user.Username = name;
                    _user.email = email;

                    _db.USER_ACCOUNT.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.error = "Wrong pass";
                return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
            }
            else
            {
                ViewBag.error = "UserName or Email already exists";
                return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
            }
        }

        public ActionResult Login()
        {
            string name = Request.Form["user"];
            string pass = Request.Form["password"];

            return RedirectToAction("Index");
        }
        [HttpPost]

        public ActionResult Login(string email, string pass)
        {
            string name = Request.Form["user"];
            string password = Request.Form["pass"];
            var f_password = GetMD5(password);
            var data = _db.USER_ACCOUNT.Where(s => s.Username.Equals(name) && s.UserPassword.Equals(f_password)).ToList();
            if (data.Count() > 0)
            {
                //add session
                Session["email"] = data.FirstOrDefault().email;
                Session["UserID"] = data.FirstOrDefault().UserID;

                ViewBag.error = "Login oke";
                ViewBag.index = 1;
                Session["name_user"] = data.FirstOrDefault().Username;
                return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
            }
            else
            {
                return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
            }


        }
        public ActionResult TestLogin(string tk, string mk)
        {
            string name = tk;
            string password = mk;
            var f_password = GetMD5(password);
            var data = _db.USER_ACCOUNT.Where(s => s.Username.Equals(name) && s.UserPassword.Equals(f_password)).ToList();

            //add session
            Session["email"] = data.FirstOrDefault().email;
            Session["UserID"] = data.FirstOrDefault().UserID;

            ViewBag.error = "Login oke";
            ViewBag.index = 1;
            Session["name_user"] = data.FirstOrDefault().Username;
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });



        }
        public JsonResult CheckLogin(string name, string password)
        {
            var f_password = GetMD5(password);
            var data = _db.USER_ACCOUNT.Where(s => s.Username.Equals(name) && s.UserPassword.Equals(f_password)).ToList();
            if (data.Count() > 0)
            {
                //add session


                return Json(new
                {
                    redirectUrl = Url.Action("TestLogin", "User", new { tk = name, mk = password }),
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {

                    isRedirect = "Đăng nhập thất bại, kiểm tra lại thông tin !!!"
                }, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult CheckSignup(string name, string pass, string repass, string email)
        {
            USER_ACCOUNT _user = new USER_ACCOUNT();

            string a = _db.Database.SqlQuery<String>("exec check_ID").ToList()[0];
            string b = a.Substring(4, 4);
            int _id = Int32.Parse(b);
            _user.email = email;
            if (_id < 10)
            {
                _id += 1;
                _user.UserID = string.Concat("USER000", _id.ToString());
            }
            else if (_id < 100 && _id >= 10)
            {
                _id += 1;
                _user.UserID = string.Concat("USER00", _id.ToString());
            }
            else if (_id < 1000 && _id >= 100)
            {
                _id += 1;
                _user.UserID = string.Concat("USER0", _id.ToString());
            }
            else if (_id < 10000 && _id >= 1000)
            {
                _id += 1;
                _user.UserID = string.Concat("USER", _id.ToString());
            }
            var check = _db.USER_ACCOUNT.FirstOrDefault(s => s.email == email);
            var check1 = _db.USER_ACCOUNT.FirstOrDefault(s => s.UserID == _user.UserID);
            var check2 = _db.USER_ACCOUNT.FirstOrDefault(s => s.Username == name);
            if (check == null && check1 == null && check2 == null)
            {
                if (pass == repass)
                {
                    _user.UserPassword = GetMD5(pass);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _user.Username = name;
                    _user.email = email;

                    _db.USER_ACCOUNT.Add(_user);
                    _db.SaveChanges();
                    return
                    Json(new
                    {
                        data = 1,
                        msg = "Đăng ký thành công !!"
                    }, JsonRequestBehavior.AllowGet);
                }
                return
                     Json(new
                     {
                         data = 2,
                         msg = "Đăng ký thất bại !! Kiểm tra lại thông tin"
                     }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return
                    Json(new
                    {
                        data = 2,
                        msg = "Đăng ký thất bại !! Kiểm tra lại thông tin"
                    }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["UserID"] = null;
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult UserInfo(string id)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            USER_ACCOUNT user = _db.USER_ACCOUNT.Find(id);
            ViewBag.user = user;
            List<Bill_Info> result = _db.Database.SqlQuery<Bill_Info>($"exec GetListBillFromUser N'{user.UserID}'").ToList();
            foreach (var item in result)
            {
                item.TicketType = _db.Database.SqlQuery<TICKET_2>($"exec GetTicketsFromTicketSession N'{item.TicketSession}'").ToList();
                item.Service = _db.Database.SqlQuery<SERVICE_TO_CASH>($"exec GetServicesFromServiceSession N'{item.ServiceSession}'").ToList();
                item.data = _db.Database.SqlQuery<Bill_data>($"exec GetInfoBillFromTicketSession N'{item.TicketSession}'").ToList()[0];
                item.PayDay = item.PayDay.Value.Date;
            }

            ViewBag.bill = result;

            return View();
        }
    }
}