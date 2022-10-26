using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        CinemaDB db = new CinemaDB();
        static string static_keyword = "";
        public ActionResult Search(string category, string nation, string page)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            int p = Int32.Parse(page);
            string keyword = Request.Form["keyword"];
            if (static_keyword != keyword && keyword != null)
            {
                static_keyword = keyword;
            }
            SqlParameter x = new SqlParameter();
            x.Value = static_keyword;
            if (category == "all" && nation == "all")
            {
                var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{x.Value}'").ToList();
                if ((5 * (p) - result.Count()) < 5 && (5 * (p) - result.Count()) > 0)
                {
                    ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                }
                else
                {
                    if (result.Count() - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), 5);
                    }
                }
                if (result.Count() % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString()) + 1;
                }
            }
            if (category == "all" && nation != "all")
            {
                var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Nation @name=N'{x.Value}', @nation=N'{nation}'").ToList();
                if ((5 * (p) - result.Count()) < 5 && (5 * (p) - result.Count()) > 0)
                {
                    ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                }
                else
                {
                    if (result.Count() - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), 5);
                    }
                }
                if (result.Count() % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString()) + 1;
                }
            }
            if (nation == "all" && category != "all")
            {
                var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Category @name=N'{x.Value}', @category=N'{category}'").ToList();
                if ((5 * (p) - result.Count()) < 5 && (5 * (p) - result.Count()) > 0)
                {
                    ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                }
                else
                {
                    if (result.Count() - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), 5);
                    }
                }
                if (result.Count() % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString()) + 1;
                }
            }
            if (nation != "all" && category != "all")
            {
                var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm_Nation_Category @name=N'{x.Value}', @nation=N'{nation}', @category=N'{category}'").ToList();
                if ((5 * (p) - result.Count()) < 5 && (5 * (p) - result.Count()) > 0)
                {
                    ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                }
                else
                {
                    if (result.Count() - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), result.Count() - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = result.GetRange(5 * (p - 1), 5);
                    }
                }
                if (result.Count() % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((result.Count() / 5).ToString()) + 1;
                }
            }
            ViewBag.keyword = x.Value;
            ViewBag.category = db.Database.SqlQuery<string>($"exec FilmCategory ").ToList();
            ViewBag.nation = db.Database.SqlQuery<string>($"exec FilmNation ").ToList();
            ViewBag.checkcategory = category;
            ViewBag.checknation = nation;
            return View();
        }
    }
}