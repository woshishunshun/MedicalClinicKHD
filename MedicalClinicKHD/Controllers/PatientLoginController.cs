using MedicalClinicKHD.Models;
using Newtonsoft.Json;
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
            if (Session["UserName"] == null)
            {
                Session["UserName"] = "未登陆";
            }
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
        //病人登陆
        public void LoginPatient(string title, string password)
        {
            var i = Hctp.GetApi("get", "PatientLogin/PatientLogin?PatientName=" + title + "&PatientPwd=" + password);
            if (Convert.ToInt32(i) > 0)
            {
                Session["UserName"] = title;
                var table = Hctp.GetApi("get", "PatientLogin/GetPatient");
                var json = JsonConvert.DeserializeObject<List<PatientLogin>>(table).Where(n => n.PatLog_LogName == title && n.PatLog_LogPwd == password).FirstOrDefault();
                Session["UserLogId"] = json.PatLog_Id;
                Response.Write("<script>alert('欢迎您,登陆成功');var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);parent.window.location.href = '/PatientLogin/ShowIndex';</script>");
            }
            else
            {
                Content("失败");
            }
        }
        /// <summary>
        /// 注册界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientRegisterShow()
        {
            return View();
        }
        public ActionResult Patient(int id)
        {
            //获取用户信息表数据
            var list = Hctp.GetApi("get", "Patient/GetPatients");
            var list1 = JsonConvert.DeserializeObject<List<Patient>>(list).ToList();
            //筛选
            var list2 = list1.Where(m => m.Pat_Id == id).FirstOrDefault();
            return View(list2);

        }
    }
}