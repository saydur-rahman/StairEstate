using STAIR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STAIR.Data.Models;
using STAIR.Model.Models;

namespace WEB.Controllers
{
    public class DashBoardController : Controller
    {
        public readonly Isys_menuService sys_MenuService;


        //public DashBoardController()
        //{

        //}

        //public DashBoardController(Isys_menuService sys_MenuService)
        //{
        //    this.sys_MenuService = sys_MenuService;
        //}

        // GET: DashBoard
        public ActionResult Index()
        {
            //var menus = sys_MenuService.GetAllMenuForUser(1);

            var menus = new ApplicationEntities().sys_menu.ToList();


            ViewBag.Menus = menus;



            return View();
        }
    }
}