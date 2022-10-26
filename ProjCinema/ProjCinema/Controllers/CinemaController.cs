using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class CinemaController : Controller
    {
        CinemaDB db = new CinemaDB();
        // GET: Cinema
        public ActionResult Cinema(string location, string cinemaname, string change)
        {

            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.listlocation = db.CINEMA_LOCATION.ToList();
            if (location != "none")
            {
                ViewBag.listcinema = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromLocation N'{location}'").ToList();
                if (cinemaname != "none")
                {
                    var result = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromName N'{cinemaname}'").ToList();
                    ViewBag.cinemax = result[0];
                    ViewBag.listmovie = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema N'{cinemaname}','2021-10-25'").ToList();

                    foreach (var item in ViewBag.listmovie)
                    {
                        item.type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();
                        foreach (var item1 in item.type1)
                        {
                            item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];

                        }
                        item.type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();
                        foreach (var item1 in item.type2)
                        {
                            item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];

                        }
                        item.type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();
                        foreach (var item1 in item.type3)
                        {
                            item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];

                        }
                    }
                    ViewBag.cinemaimg = db.Database.SqlQuery<CINEMA_IMAGE>($"exec GetListImgFromCinema N'{cinemaname}'").ToList();
                }
            }
            ViewBag.checklocation = location;
            ViewBag.checkcinema = cinemaname;
            ViewBag.change = change;
            return View();
        }
    }
}