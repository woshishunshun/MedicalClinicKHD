using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalClinicKHD.Models;
using Newtonsoft.Json;

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
            if (Session["UserName"] == null)
            {
                Session["UserName"] = "未登陆";
            }
            if (Session["UserLogId"]==null)
            {
                Session["UserLogId"] = 0;
            }
            if (Session["ReNum"]==null)
            {
                Session["ReNum"] = 10;
            }
            return View();
        }
        /// <summary>
        /// 获取科室 绑定下拉
        /// </summary>
        /// <returns></returns>
        public string GetAdministrative()
        {
            var list = Hctp.GetApi("get", "Administrative/ShowAdministrative");
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<AdministrativeModel>>(list));
        }
        /// <summary>
        /// 获取医师 绑定下拉
        /// </summary>
        /// <returns></returns>
        public string GetDoctor(int id)
        {
            var list = Hctp.GetApi("get", "Administrative/ShowDoctor?id="+id);
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<Doctor>>(list));
        }
        /// <summary>
        /// 查询病人个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPatientInfo(int id)
        {
            var info = Hctp.GetApi("get", "Patient/GetPatients");
            var user = JsonConvert.DeserializeObject<List<Patient>>(info).Where(n => n.PatLog_Id == id).FirstOrDefault();
            var json = JsonConvert.SerializeObject(user);
            return json;
        }
    }
}