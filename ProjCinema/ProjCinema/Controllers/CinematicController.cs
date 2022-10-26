using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;

namespace ProjCinema.Controllers
{
    public class CinematicController : Controller
    {
        // GET: Cinematic
        CinemaDB db = new CinemaDB();
        public ActionResult Cinematic()
        {
            return View();
        }
        public ActionResult Review(string page)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.curMovie = db.Database.SqlQuery<MOVIE>("exec CurMovie").ToList();
            var result = db.Database.SqlQuery<POST>("exec GetAllReview").ToList();

            if (page == "1")
            {
                ViewBag.listreview = result.GetRange(0, 5);
                ViewBag.page = page;
            }
            else
            {
                ViewBag.listreview = result.GetRange(5, (result.Count() - 5));
                ViewBag.page = page;
            }
            return View();
        }

        public ActionResult Blog(string page)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.curMovie = db.Database.SqlQuery<MOVIE>("exec CurMovie").ToList();
            var result = db.Database.SqlQuery<POST>("exec GetAllBlog").ToList();

            if (page == "1")
            {
                ViewBag.listblog = result.GetRange(0, 5);
                ViewBag.page = page;
            }
            else
            {
                ViewBag.listblog = result.GetRange(5, (result.Count() - 5));
                ViewBag.page = page;
            }
            return View();
        }
        public ActionResult Sale()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.curMovie = db.Database.SqlQuery<MOVIE>("exec CurMovie").ToList();
            ViewBag.listsale = db.Database.SqlQuery<POST>("exec GetAllSale").ToList();
            return View();
        }
        public ActionResult DetailPost(string id)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.curMovie = db.Database.SqlQuery<MOVIE>("exec CurMovie").ToList();
            var result = db.Database.SqlQuery<POST_CONTENT>($"exec GetPostContentFromPostID {id}").ToList();
            ViewBag.post = db.POSTs.Find(id);
            ViewBag.postcontent = result[0];
            var result2 = db.Database.SqlQuery<POST>($"exec GetCategoryFromPost {id}").ToList();
            var category = result2[0].PostCategory;
            if (category == "Blog điện ảnh")
            {
                ViewBag.category = "blog";
            }
            else if (category == "Bình luận phim")
            {
                ViewBag.category = "review";
            }
            else ViewBag.category = "sales";
            return View();
        }
    }
}