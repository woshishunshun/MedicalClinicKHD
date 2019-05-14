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
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowIndex()
        {
            return View();
        }
        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientLoginShow()
        {
            return View();
        }
        /// <summary>
        /// 注册界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientRegisterShow()
        {
            return View();
        }
    }
}