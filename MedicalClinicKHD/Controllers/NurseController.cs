using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalClinicKHD.Controllers
{
    public class NurseController : Controller
    {
        // GET: Nurse
        /// <summary>
        /// 护士登录
        /// </summary>
        /// <returns></returns>
        public ActionResult NurseLogin()
        {
            return View();
        }
        /// <summary>
        /// 病人挂号
        /// </summary>
        /// <returns></returns>
        public ActionResult Registered()
        {
            return View();
        }
        /// <summary>
        /// 病人注册
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientAdd()
        {
            return View();
        }
    }
}