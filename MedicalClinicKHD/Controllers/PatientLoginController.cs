using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalClinicKHD.Controllers
{
    public class PatientLoginController : Controller
    {
        // GET: PatientLogin
        /// <summary>
        /// 病人登陆界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientLoginShow()
        {
            return View();
        }
    }
}