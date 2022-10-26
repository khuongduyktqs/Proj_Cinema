using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class MovieController : Controller
    {
        CinemaDB db = new CinemaDB();
        // GET: Movie
        public ActionResult Details(string id)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.detail = db.MOVIEs.Find(id);
            return View();
        }
        public ActionResult MovieCurrent()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            var current = db.Database.SqlQuery<MOVIE>("exec GetCurrentFilm").ToList();
            ViewBag.current = current;
            return View();
        }
        public ActionResult MovieFuture()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            var future = db.Database.SqlQuery<MOVIE>("exec GetFutureFilm").ToList();
            ViewBag.future = future;
            return View();
        }
    }
}