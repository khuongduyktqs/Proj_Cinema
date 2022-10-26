using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        CinemaDB db = new CinemaDB();
        public ActionResult Booking(string name, string location)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            SqlParameter moviename = new SqlParameter();
            moviename.Value = name;
            ViewBag.movie = db.Database.SqlQuery<MOVIE>("exec GetCurrentFilm").ToList();
            ViewBag.LC = db.CINEMA_LOCATION.ToList();
            if (location == "all")
            {
                ViewBag.cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilm N'{name}', N'2021-10-25'").ToList();
            }
            else
            {
                ViewBag.cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilmAndLocation N'{name}', N'2021-10-25',N'{location}'").ToList();
            }
            foreach (var item in ViewBag.cinema)
            {
                item.type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{item.CinemaName}',N'{name}', N'2021-10-25'").ToList();
                foreach (var item1 in item.type1)
                {
                    item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{name}', '{item1.roomid}','{item1.showtime}'").ToList()[0];

                }
                item.type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{item.CinemaName}',N'{name}', N'2021-10-25'").ToList();
                foreach (var item1 in item.type2)
                {
                    item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{name}', '{item1.roomid}','{item1.showtime}'").ToList()[0];

                }
                item.type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{item.CinemaName}',N'{name}', N'2021-10-25'").ToList();
                foreach (var item1 in item.type3)
                {
                    item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{name}', '{item1.roomid}','{item1.showtime}'").ToList()[0];

                }
            }
            ViewBag.check = name;
            ViewBag.location = location;

            return View();
        }
        public JsonResult Location(string location)
        {
            var listcinema = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromLocation N'{location}'").ToList();
            ViewBag.checklocation = location;
            ViewBag.listcinema = listcinema;
            ViewBag.checkcinema = "none";
            return Json(listcinema, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Cinema(string cinema)
        {
            var listmovie = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema N'{cinema}','2021-10-25'").ToList();
            ViewBag.listmovie = listmovie;

            ViewBag.checkcinema = cinema;
            return Json(listmovie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowTime(string movie, string cinemaname)
        {
            var type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            var type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            var type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            foreach (var item in type1)
            {
                item.screentype = "2D";
            }
            foreach (var item in type2)
            {
                item.screentype = "3D";
            }
            foreach (var item in type3)
            {
                item.screentype = "4Dx";
            }
            type1.AddRange(type2);
            type1.AddRange(type3);
            return Json(type1, JsonRequestBehavior.AllowGet);
        }
    }
}