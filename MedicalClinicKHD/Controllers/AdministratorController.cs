using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalClinicKHD.Models;
using Newtonsoft.Json;

namespace MedicalClinicKHD.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="staff"></param>
        public void Login(StaffLoginModels staff)
        {
            var list = Hctp.GetApi("get", "Administrator/Login", null);
            var list1 = JsonConvert.DeserializeObject<List<StaffLoginModels>>(list);
            var i = list1.Where(m => m.Sl_Name == staff.Sl_Name && m.Sl_Pwd == staff.Sl_Pwd).Count();
            if (i > 0)
            {
                Response.Write("登录成功");
            }
            else
            {
                Response.Write("登录失败");
            }

        }
    }
}