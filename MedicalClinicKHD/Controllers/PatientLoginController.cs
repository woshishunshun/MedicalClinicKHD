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
                Response.Write("<script>alert('登陆成功');var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);location.href='/PatientLogin/ShowIndex'</script>");
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
    }
}