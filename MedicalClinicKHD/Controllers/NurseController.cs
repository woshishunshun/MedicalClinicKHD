using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalClinicKHD.Controllers
{
    public class NurseController : Controller
    {
        /// <summary>
        /// 病人挂号
        /// </summary>
        /// <returns></returns>
        public ActionResult Registered()
        {
            return View();
        }
        /// <summary>
        /// 护士
        /// </summary>
        /// <returns></returns>
        public ActionResult NurseLogin()
        {
            return View();
        }
    }
}