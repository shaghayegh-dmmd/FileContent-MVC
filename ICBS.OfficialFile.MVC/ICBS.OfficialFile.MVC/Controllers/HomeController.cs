using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICBS.OfficialFile.BLL.OfficialFileServiceReference;
using ICBS.OfficialFile.MVC.Models;
using ICBS.OfficialFile.MVC.Enum;
using ICBS.OfficialFile.MVC;
using static ICBS.OfficialFile.MVC.Enum.EnumService;

namespace ICBS.OfficialFile.MVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadMenu()
        { 


            MenuModel menuModel = CreateActionMenu();


            return PartialView("_PartialMenu", menuModel);
        }

        private MenuModel CreateActionMenu()
        {


            MenuModel menuModel = new MenuModel()
            {
                Id = 1,
                ChildList = new List<MenuModel>()
            };



            if (AccessValidation.HasUserAccess(User.Identity.Name, AccessLevel.Manager.ToString()))
            {


                menuModel.ChildList.Add(new MenuModel()
                {

                    Id = 2,
                    ParentId = 1,
                    DisplayName = "پنل مدیریت",
                    ControlName = "OfficialFile",
                    ActionName = "FileContentIndex",

                });
            }


            if (AccessValidation.HasUserAccess(User.Identity.Name, AccessLevel.Manager.ToString()) || AccessValidation.HasUserAccess(User.Identity.Name, AccessLevel.SimpleUser.ToString()))
            {
                menuModel.ChildList.Add(new MenuModel()
                {
                    Id = 3,
                    ParentId = 1,
                    DisplayName = "آموزش",
                    ControlName = "OfficialFile",
                    ActionName = "GetAllFileContentLearnIndex",

                });

            }
            if (AccessValidation.HasUserAccess(User.Identity.Name, AccessLevel.Manager.ToString()) || AccessValidation.HasUserAccess(User.Identity.Name, AccessLevel.SimpleUser.ToString()))
            {
                menuModel.ChildList.Add(new MenuModel()
                {
                    Id = 4,
                    ParentId = 1,
                    DisplayName = "فنی",
                    ControlName = "OfficialFile",
                    ActionName = "GetAllFileContentTechIndex",

                });
           }
            return menuModel;
        }

    }
}