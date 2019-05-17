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
            return View();
        }
        /// <summary>
        /// 获取科室 绑定下拉
        /// </summary>
        /// <returns></returns>
        public string GetAdministrative()
        {
            var list = Hctp.GetApi("get", "Administrative/ShowAdministrative");
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<AdministrativeModels>>(list));
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
    }
}