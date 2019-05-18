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
            var list2 = GetRegistrations();
            return View(list2);
        }
        public List<Registration> GetRegistrations()
        {
            //等级医生姓名获取id值
            var i = kc.Value;
            //查询所有的登陆表信息
            var getDocId = Hctp.GetApi("get", "Doctor/GetLoginTable");
            //根据姓名查询医生Id值
            var getDocId01 = JsonConvert.DeserializeObject<List<StaffLoginModels>>(getDocId).Where(m => m.Sl_Name == i).FirstOrDefault().Sl_Id;
            //获取挂号表的数据
            var list = Hctp.GetApi("get", "Doctor/GetRegistrations");
            //获取当前日期
            DateTime data = DateTime.Now;
            //查询当前医生下的说有挂号信息
            var list1 = JsonConvert.DeserializeObject<List<Registration>>(list).Where(m => m.Doc_Id == getDocId01).ToList();
            //查询当天的就诊信息
            var list2 = list1.Where(m => Convert.ToDateTime(m.Reg_Time).Day - data.Day == 0 && Convert.ToDateTime(m.Reg_Time).Month - data.Month==0).ToList().Where(m => m.Reg_Type == 0 || m.Reg_Type == 1).ToList();
            return list2;
        }
        [HttpGet]
        public ActionResult JiaoHao(int id)
        {
            //获取当前医生下有没有人正在就诊
            var list1 = GetRegistrations().Where(m => m.Reg_Type == 1).ToList();
            var list2 = list1.Count();
            if (list2 >0)
            {
                return Content("叫号失败，应为有人正在就诊");
            }
            //获取挂号表当前病人的挂号信息
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
        public Registration GetTs(int id)
        {
            var list = Hctp.GetApi("get", "Doctor/GetRegistrations");
            var list1 = JsonConvert.DeserializeObject<List<Registration>>(list).Where(m => m.Reg_Id == id).FirstOrDefault();
            return list1;
        }
        public ActionResult Doctorecord()
        {
            return View();
        }
        public ActionResult AddAndChangeZc(Doctorecord d)
        {
            var id = d.Pat_Id;
            var list = GetTs(id);
            d.Adm_Id = list.Adm_Id;
            d.Doc_Id = list.Doc_Id;
            d.Dcr_Time = DateTime.Now.ToString("yyyy-MM-dd");
            var i = Hctp.GetApi("post", "Doctor/AddGetDoctorecord",d);
            if (Convert.ToInt32(i) > 0)
            {
                var Gt = GetTs(d.Pat_Id);
                Gt.Reg_Type = 2;
                var i1 = Hctp.GetApi("post", "Doctor/UptRegistrations", Gt);
                if (Convert.ToInt32(i) > 0)
                {
                    return Content("成功");
                }
                else
                {
                    return Content("失败");
                }
            }
            else
            {
                return Content("添加就诊记录失败");
            }
        }
    }
}