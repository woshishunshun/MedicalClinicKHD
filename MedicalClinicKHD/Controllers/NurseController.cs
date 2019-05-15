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
        /// 病人查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientQuery()
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
        /// 护士
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
    }
}