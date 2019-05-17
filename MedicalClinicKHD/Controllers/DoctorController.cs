using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MedicalClinicKHD.Models;

namespace MedicalClinicKHD.Controllers
{
    public class DoctorController : Controller
    {
        static HttpCookie kc = null;
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginDocter()
        {
            return View();
        }
        public ActionResult LoginDoctor(string title, string password)
        {
            var i = Hctp.GetApi("get", "Doctor/LoginDoctor?name=" + title + "&pwd=" + password);
            if (Convert.ToInt32(i) > 0)
            {
                kc = new HttpCookie("name",title);
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
        }
        public ActionResult Registration()
        {
            var i = kc.Value;
            var getDocId = Hctp.GetApi("get", "Doctor/GetLoginTable");
            var getDocId01 = JsonConvert.DeserializeObject<List<StaffLoginModels>>(getDocId).Where(m => m.Sl_Name == i).FirstOrDefault().Sl_Id;
            var list = Hctp.GetApi("get", "Doctor/GetRegistrations");
            DateTime data = DateTime.Now;
            var list1 = JsonConvert.DeserializeObject<List<Registration>>(list).Where( m => m.Doc_Id == getDocId01).ToList();
            var list2 = list1.Where(m => Convert.ToDateTime(m.Reg_Time).Day-data.Day==0).ToList();
            return View(list2);
        }
        [HttpGet]
        public ActionResult Type_change1(int id)
        {
            var list = GetTs(id);
            list.Reg_Type = 1;
            var i = Hctp.GetApi("post", "Doctor/UptRegistrations", list);
            if (Convert.ToInt32(i) > 0)
            {
                return Content("叫号成功");
            }
            else
            {
                return Content("叫号失败");
            }
        }
        public ActionResult Type_change2(int id)
        {
            var list = GetTs(id);
            list.Reg_Type = 2;
            var i = Hctp.GetApi("post", "Doctor/UptRegistrations", list);
            if (Convert.ToInt32(i) > 0)
            {
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
        }
        public Registration GetTs(int id)
        {
            var list = Hctp.GetApi("get", "Doctor/GetRegistrations");
            var list1 = JsonConvert.DeserializeObject<List<Registration>>(list).Where(m => m.Reg_Id == id).FirstOrDefault();
            return list1;
        }
    }
}