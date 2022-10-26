using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class HomepageController : Controller
    {
        CinemaDB db = new CinemaDB();
        // GET: HomePage
        public ActionResult HomePage()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
                var movie = db.MOVIEs.ToList();
                ViewBag.movie = movie;
                var blp = db.Database.SqlQuery<POST>("exec GetReview").ToList();
                var blog = db.Database.SqlQuery<POST>("exec GetBlog").ToList();
                var sale = db.Database.SqlQuery<POST>("exec GetSaleNew").ToList();
                ViewBag.blp = blp;
                ViewBag.blog = blog;
                ViewBag.sale = sale;
                ViewBag.slide = db.Database.SqlQuery<AD>("exec GetListSlide").ToList();
                ViewBag.listlocation = db.CINEMA_LOCATION.ToList();

                ViewBag.checklocation = "none";
                ViewBag.checkcinema = "none";
                return View();
            }
            else
            {
                var movie = db.MOVIEs.ToList();
                ViewBag.movie = movie;
                var blp = db.Database.SqlQuery<POST>("exec GetReview").ToList();
                var blog = db.Database.SqlQuery<POST>("exec GetBlog").ToList();
                var sale = db.Database.SqlQuery<POST>("exec GetSaleNew").ToList();
                ViewBag.blp = blp;
                ViewBag.blog = blog;
                ViewBag.sale = sale;
                ViewBag.listlocation = db.CINEMA_LOCATION.ToList();
                ViewBag.slide = db.Database.SqlQuery<AD>("exec GetListSlide").ToList();
                ViewBag.checklocation = "none";
                ViewBag.checkcinema = "none";
                return View();
            }

        }


    }
}