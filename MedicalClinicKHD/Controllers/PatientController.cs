using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalClinicKHD.Controllers
{
    public class PatientController : Controller
    {
        //病人完善个人信息界面
        // GET: Patient
        public ActionResult PatientInfo()
        {
            if (Session["UserLogId"] == null)
            {
                Session["UserLogId"] = 0;
            }
            return View();
        }
    }
}