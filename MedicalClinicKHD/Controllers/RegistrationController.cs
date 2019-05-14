using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalClinicKHD.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        /// <summary>
        /// 预约挂号界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            return View();
        }
    }
}