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
        /// <summary>
        /// 显示医生数据
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DoctorShow(string Name="")
        {
            string list = Hctp.GetApi("get", "Administrator/doctorsShow" + Name, null);
            var result = JsonConvert.DeserializeObject<List<Doctor>>(list);
            return View(result);
        }
        [HttpGet]
        public ActionResult DoctorAdd()
        {
            string result = Hctp.GetApi("get", "Administrator/administrativesShow", null);
            List<AdministrativeModels> administratives = JsonConvert.DeserializeObject<List<AdministrativeModels>>(result);
            var selectitem = from a in administratives
                             select new SelectListItem
                             {
                                 Text = a.Adm_Name,
                                 Value=a.Adm_Id.ToString(),
                             };
            ViewBag.KS = selectitem;
            return View();
        }
        public void DoctorAdd(Doctor d)
        {
            var list = Hctp.GetApi("post", "Administrator/DoctorAdd", d);
            if (list=="1")
            {
                Response.Write("<script>alert('添加成功');location.href='/Administrator/DoctorShow';</script>");
            }
            else
            {
                Response.Write("添加失败");
            }
        }
        [HttpGet]
        public void DeleteDoctor(int id)
        {
            string result = Hctp.GetApi("del", "Administrator/DeleteDoctor?id=" + id, null);
            if (result!="0")
            {
                Response.Write("<script>alert('删除成功');location.href='/Administrator/DoctorShow';</script>");
            }
            else
            {
                Response.Write("删除失败");
            }
        }
        public ActionResult Cha(string name)
        {
            string list = Hctp.GetApi("get", "Administrator/doctorsShow?Name=" + name, null);
            var result = JsonConvert.DeserializeObject<List<Doctor>>(list);
            return PartialView("_table",result);
        }
    }
}