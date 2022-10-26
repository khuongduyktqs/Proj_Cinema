using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjCinema.Models;
namespace ProjCinema.Controllers
{
    public class TicketController : Controller
    {
        CinemaDB db = new CinemaDB();
        public static string CinemaAddress, CinemaName, MovieName, DateTime1, ShowTime, ScreenType, Tickets, Image, Room, MVT;
        public static int Money;
        public static int TKT1 = 0, TKT2 = 0, TKT3 = 0, TKT4 = 0, SV1 = 0, SV2 = 0, SV3 = 0, SV4 = 0, SV5 = 0, SV6 = 0;
        public static DISCOUNT_CODE voucher = new DISCOUNT_CODE();
        public static List<string> arr_ticket, seat_arr, all_sv;
        public static List<TICKET_TYPE> tk = new List<TICKET_TYPE>();
        public static List<SERVICE_TO_CASH> sv = new List<SERVICE_TO_CASH>();
        public ActionResult SelectTicket(string mvtid, string cinemaname, string datetime, string showtime, string room, string screentype, string filmname)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            MVT = mvtid;
            var cinemaaddress = db.Database.SqlQuery<CINEMA>($"exec GetCinemaFromName N'{cinemaname}'").ToList()[0];
            ViewBag.CinemaAddress = cinemaaddress.CinemaAddress;
            ViewBag.CinemaName = cinemaname;
            ViewBag.FilmName = filmname;
            ViewBag.showtime = showtime;
            ViewBag.datetime = datetime;
            ViewBag.screentype = screentype;
            var img = db.Database.SqlQuery<MOVIE>($"exec GetFilmFromName N'{filmname}'").ToList();
            ROOM room1 = db.ROOMs.Find(room);
            Room = room1.RoomName;
            ViewBag.room = Room;
            Image = img[0].PosterLink;
            ViewBag.Image = Image;
            CinemaAddress = cinemaaddress.CinemaAddress;
            CinemaName = cinemaname;
            MovieName = filmname;
            ShowTime = showtime;
            DateTime1 = datetime;
            ScreenType = screentype;
            ViewBag.tickettype = db.TICKET_TYPE.ToList();

            return PartialView();
        }
        public ActionResult SelectSeat(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.room = Room;
            ViewBag.datetime = DateTime1;
            ViewBag.TicketCount = data.Count;
            arr_ticket = data;
            foreach (var item in data)
            {
                if (item == "TKT1")
                {
                    TKT1++;
                    Money += 80000;
                }
                if (item == "TKT2")
                {
                    TKT2++;
                    Money += 60000;
                }
                if (item == "TKT3")
                {
                    TKT3++;
                    Money += 40000;
                }
                if (item == "TKT4")
                {
                    TKT4++;
                    Money += 200000;
                }
            }
            ViewBag.ListSeatBooked = db.Database.SqlQuery<string>($"exec GetListSeatBooked N'{MovieName}', '{Room}', '{ShowTime}', N'2021-10-25'").ToList();
            return PartialView();
        }
        public ActionResult SelectService(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.datetime = DateTime1;
            ViewBag.room = Room;
            ViewBag.service = db.SERVICEs.ToList();
            ViewBag.count = 0;
            seat_arr = data;

            return PartialView();
        }
        public ActionResult Payment(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.datetime = DateTime1;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.room = Room;
            all_sv = data;
            if (TKT1 != 0)
            {
                TICKET_TYPE result = db.TICKET_TYPE.Find("TKT1");
                result.TicketNum = TKT1;
                tk.Add(result);
            }
            if (TKT2 != 0)
            {
                TICKET_TYPE result = db.TICKET_TYPE.Find("TKT2");
                result.TicketNum = TKT2;
                tk.Add(result);

            }
            if (TKT3 != 0)
            {
                TICKET_TYPE result = db.TICKET_TYPE.Find("TKT3");
                result.TicketNum = TKT3;
                tk.Add(result);
            }
            if (TKT4 != 0)
            {
                TICKET_TYPE result = db.TICKET_TYPE.Find("TKT4");
                result.TicketNum = TKT4;
                tk.Add(result);
            }
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item == "SV1")
                    {
                        SV1++;
                        Money += 25000;
                    }
                    if (item == "SV2")
                    {
                        SV2++;
                        Money += 45000;
                    }
                    if (item == "SV3")
                    {
                        SV3++;
                        Money += 60000;
                    }
                    if (item == "SV4")
                    {
                        SV4++;
                        Money += 80000;
                    }
                    if (item == "SV5")
                    {
                        SV5++;
                        Money += 20000;
                    }
                    if (item == "SV6")
                    {
                        SV6++;
                        Money += 55000;
                    }
                }
            }
            if (SV1 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV1");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV1;
                sv.Add(svtc);
            }
            if (SV2 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV2");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV2;
                sv.Add(svtc);
            }
            if (SV3 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV3");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV3;
                sv.Add(svtc);
            }
            if (SV4 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV4");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV4;
                sv.Add(svtc);
            }
            if (SV5 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV5");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV5;
                sv.Add(svtc);
            }
            if (SV6 != 0)
            {
                SERVICE result = db.SERVICEs.Find("SV6");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV6;
                sv.Add(svtc);
            }
            ViewBag.arr_ticket = tk;
            ViewBag.service = sv;
            if (voucher.CodeID == null)
            {
                ViewBag.Money = Money / 1000;
            }
            else
            {
                ViewBag.voucher = voucher;
                ViewBag.Money = Money * (100 - voucher.DiscountNumber) / 100000;
            }
            ViewBag.seat_arr = seat_arr;
            return PartialView();
        }
        public JsonResult CheckVoucher(string vc)
        {
            string data = "";
            var result = db.DISCOUNT_CODE.Find(vc);
            if (result != null)
            {
                if (result.State == 0)
                {
                    voucher = result;
                    data = "ok";
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Refresh()
        {

            Money = 0;
            TKT1 = 0; TKT2 = 0; TKT3 = 0; TKT4 = 0; SV1 = 0; SV2 = 0; SV3 = 0; SV4 = 0; SV5 = 0; SV6 = 0;
            DISCOUNT_CODE voucher = new DISCOUNT_CODE();
            arr_ticket = null; seat_arr = null;
            tk = null;
            sv = null;
            return Json("Refresh", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Order()
        {
            //insert into ticket, seat, service to cash, bill
            string userid = (string)Session["UserID"];
            if (userid != "")
            {
                string TicketSession = "TKS" + DateTime1 + System.DateTime.Now.TimeOfDay.Minutes.ToString();
                string ServiceSession = "SVS" + DateTime1 + System.DateTime.Now.TimeOfDay.Minutes.ToString();
                foreach (var item in seat_arr)
                {

                    TICKET tk = new TICKET();
                    SEAT s = new SEAT();



                    //seat
                    string SeatID = db.Database.SqlQuery<String>("exec GetMaxSeatID").ToList()[0];
                    s.SeatID = "SEAT";
                    for (int i = 1; i <= (SeatID.Length - (Int32.Parse(SeatID.Substring(4, SeatID.Length - 4))).ToString().Length) - 4; i++)
                    {
                        s.SeatID += "0";
                    }
                    s.SeatID = s.SeatID + (Int32.Parse(SeatID.Substring(4, SeatID.Length - 4)) + 1).ToString();
                    s.SeatName = item;
                    s.MovieTimeID = db.MOVIE_TIME.Find(MVT).MovieTimeID;
                    db.SEATs.Add(s);
                    db.SaveChanges();
                    //ticket
                    string Ticket = db.Database.SqlQuery<String>("exec GetMaxTicketID").ToList()[0];
                    tk.TicketID = "TK";
                    for (int i = 1; i <= (Ticket.Length - (Int32.Parse(Ticket.Substring(2, Ticket.Length - 2))).ToString().Length) - 2; i++)
                    {
                        tk.TicketID += "0";
                    }
                    tk.TicketID = tk.TicketID + (Int32.Parse(Ticket.Substring(2, Ticket.Length - 2)) + 1).ToString();
                    tk.SeatID = s.SeatID;
                    tk.TicketSession = TicketSession;
                    tk.TicketType = arr_ticket[item.IndexOf(item)];
                    db.TICKETs.Add(tk);


                    db.SaveChanges();

                }
                foreach (var item in all_sv)
                {

                    SERVICE_TO_CASH svc = new SERVICE_TO_CASH();
                    string SVC = db.Database.SqlQuery<String>("exec GetMaxSVCID").ToList()[0];
                    svc.ServiceToCashID = "SVTC";
                    for (int i = 1; i <= (SVC.Length - (Int32.Parse(SVC.Substring(4, SVC.Length - 4))).ToString().Length) - 4; i++)
                    {
                        svc.ServiceToCashID += "0";
                    }
                    svc.ServiceToCashID = svc.ServiceToCashID + (Int32.Parse(SVC.Substring(4, SVC.Length - 4)) + 1).ToString();
                    svc.ServiceName = db.SERVICEs.Find(item).ServiceName;
                    svc.ServiceSession = ServiceSession;
                    svc.ServiceID = item;
                    svc.ServicePrice = db.SERVICEs.Find(item).ServicePrice;
                    db.SERVICE_TO_CASH.Add(svc);


                    db.SaveChanges();
                }
                BILL b = new BILL();
                string BillID = db.Database.SqlQuery<String>("exec GetMaxBillID").ToList()[0];
                b.BillID = "B";
                for (int i = 1; i <= (BillID.Length - (Int32.Parse(BillID.Substring(1, BillID.Length - 1))).ToString().Length) - 1; i++)
                {
                    b.BillID += "0";
                }
                b.BillID = b.BillID + (Int32.Parse(BillID.Substring(1, BillID.Length - 1)) + 1).ToString();
                b.UserID = userid;
                b.ServiceSession = ServiceSession;
                b.TicketSession = TicketSession;
                b.PayDay = DateTime.Parse(DateTime1);
                b.UserID = userid;
                b.CodeID = voucher.CodeID;
                db.BILLs.Add(b);
                db.SaveChanges();
                return Redirect("/HomePage");
            }
            else
            {
                return Redirect("/HomePage");
            }
        }


    }
}