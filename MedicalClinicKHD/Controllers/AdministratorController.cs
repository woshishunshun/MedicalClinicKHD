﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalClinicKHD.Models;
using Newtonsoft.Json;
using PagedList;
namespace MedicalClinicKHD.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        /// <summary>
        /// /添加登录人界面
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult LoginAdd()
        {
            return View();
        }
        /// <summary>
        /// 添加登录人
        /// </summary>
        /// <param name="staff"></param>
        public void LoginAdd(StaffLoginModels staff)
        {
            
            string list = Hctp.GetApi("post", "Administrator/LoginAdd", staff);
            if (list=="1")
            {
                Session["name"] = staff.Sl_Name;
                Response.Write("<script>alert('添加成功');</script>");
                if (staff.Sl_Type == 0)
                {
                    Response.Write("<script>location.href='/Administrator/Administratorpreants'</script>");
                }
                else if (staff.Sl_Type == 1)
                {
                    Response.Write("<script>location.href='/Administrator/DoctorAdd'</script>");
                }
                else if (staff.Sl_Type == 2)
                {
                    Response.Write("<script>location.href='/Administrator/NurseAdd'</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('添加失败');</script>");

            }
        }

        /// <summary>
        /// 管理员界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Administratorpreants()
        {
            return View();
        }
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
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
            var list2 = list1.Where(m => m.Sl_Name == staff.Sl_Name && m.Sl_Pwd == staff.Sl_Pwd).FirstOrDefault();

            if (i > 0)
            {
                Response.Write("<script>alert('登录成功');</script>");
                if (list2.Sl_Type == 0)
                {
                    Response.Write("<script>location.href='/Administrator/Administratorpreants'</script>");
                }
                else if (list2.Sl_Type==1)
                {
                    Response.Write("<script>location.href='/Administrator/DoctorShow'</script>");
                }
                else if (list2.Sl_Type == 2)
                { 
                    Response.Write("<script>location.href='/Administrator/NurseShow'</script>");
                }
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
        public ActionResult DoctorShow(string Name = "",int pageIndex=1)
        {
            string list = Hctp.GetApi("get", "Administrator/doctorsShow" + Name, null);
            var result = JsonConvert.DeserializeObject<List<Doctor>>(list);
            int pageSize = 3;
            int count = result.Count();
            IPagedList<Doctor> pageList = result.ToPagedList(pageIndex, pageSize);
            return View(pageList);
        }
        /// <summary>
        /// 医生添加绑定下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DoctorAdd()
        {
            string result = Hctp.GetApi("get", "Administrator/administrativesShow", null);
            List<AdministrativeModel> administratives = JsonConvert.DeserializeObject<List<AdministrativeModel>>(result);
            var selectitem = from a in administratives
                             select new SelectListItem
                             {
                                 Text = a.Adm_Name,
                                 Value = a.Adm_Id.ToString(),
                             };
            ViewBag.KS = selectitem;
            return View();
        }
        /// <summary>
        /// 添加界面
        /// </summary>
        /// <param name="d"></param>
        public void DoctorAdd(Doctor d)
        {
            var name = Session["name"].ToString();
            var getstaff = Hctp.GetApi("get", "Administrator/Login");
            var getstaff1 = JsonConvert.DeserializeObject<List<StaffLoginModels>>(getstaff).Where(m => m.Sl_Name == name).FirstOrDefault().Sl_Id;
            d.Sl_Id = getstaff1;
            
            string json = JsonConvert.SerializeObject(d);
            var list = Hctp.GetApi("post", "Administrator/DoctorAdd", d);
            if (list == "1")
            {
                Response.Write("<script>alert('添加成功');location.href='/Administrator/Login';</script>");
            }
            else
            {
                Response.Write("添加失败");
            }
        }
        /// <summary>
        /// 医生删除
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void DeleteDoctor(int id)
        {
            string result = Hctp.GetApi("del", "Administrator/DeleteDoctor?id=" + id, null);
            if (result != "0")
            {
                Response.Write("<script>alert('删除成功');location.href='/Administrator/DoctorShow';</script>");
            }
            else
            {
                Response.Write("删除失败");
            }
        }
        /// <summary>
        /// 医生模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Cha(string name)
        {
            string list = Hctp.GetApi("get", "Administrator/doctorsShow?Name=" + name, null);
            var result = JsonConvert.DeserializeObject<List<Doctor>>(list);
            return PartialView("_table", result);
        }
        /// <summary>
        /// 医生反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EidtorDoctor(int id)
        {
            string result = Hctp.GetApi("get", "Administrator/UpdDoctor?id=" + id, null);
            List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(result);
            var doc = doctors.Where(s => s.Doc_Id == id).FirstOrDefault();

            string table = Hctp.GetApi("get", "Administrator/administrativesShow", null);
            List<AdministrativeModel> administratives = JsonConvert.DeserializeObject<List<AdministrativeModel>>(table);
            var selectitem = from a in administratives
                             select new SelectListItem
                             {
                                 Text = a.Adm_Name,
                                 Value = a.Adm_Id.ToString(),
                             };
            ViewBag.KS = selectitem;
            return View(doc);
        }
        /// <summary>
        /// 医生修改
        /// </summary>
        /// <param name="doctor"></param>
        public void EidtorDoctor(Doctor doctor)
        {
            string result = Hctp.GetApi("put", "Administrator/EditDoctor", doctor);
            if (result != "0")
            {
                Response.Write("<script>alert('修改成功');location.href='/Administrator/DoctorShow';</script>");

            }
            else
            {
                Response.Write("修改失败");
            }
        }










        

        /// <summary>
        /// 显示医生数据
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NurseShow(string Name = "",int pageIndex=1)
        {
            string list = Hctp.GetApi("get", "Administrator/NurseShow" + Name, null);
            var result = JsonConvert.DeserializeObject<List<NurseModels>>(list);
            int pageSize = 3;
            int count = result.Count();
            IPagedList<NurseModels> pageList = result.ToPagedList(pageIndex, pageSize);
            return View(pageList);
        }
        /// <summary>
        /// 护士添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NurseAdd()
        {
            return View();
        }
        /// <summary>
        /// 添加界面
        /// </summary>
        /// <param name="d"></param>
        public void NurseAdd(NurseModels n)
        {
            var name = Session["name"].ToString();
            var getstaff = Hctp.GetApi("get", "Administrator/Login");
            var getstaff1 = JsonConvert.DeserializeObject<List<StaffLoginModels>>(getstaff).Where(m => m.Sl_Name == name).FirstOrDefault().Sl_Id;

            var list = Hctp.GetApi("post", "Administrator/NurseAdd", n);
            if (list == "1")
            {
                Response.Write("<script>alert('添加成功');location.href='/Administrator/Login';</script>");
            }
            else
            {
                Response.Write("添加失败");
            }
        }
        /// <summary>
        /// 医生删除
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void DeleteNurse(int id)
        {
            string result = Hctp.GetApi("del", "Administrator/DeleteNurse?id=" + id, null);
            if (result != "0")
            {
                Response.Write("<script>alert('删除成功');location.href='/Administrator/NurseShow';</script>");
            }
            else
            {
                Response.Write("删除失败");
            }
        }
        /// <summary>
        /// 医生模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult NurseCha(string name)
        {
            string list = Hctp.GetApi("get", "Administrator/NurseShow?Name=" + name, null);
            var result = JsonConvert.DeserializeObject<List<NurseModels>>(list);
            return PartialView("_table", result);
        }
        /// <summary>
        /// 医生反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EidtorNurse(int id)
        {
            string result = Hctp.GetApi("get", "Administrator/UpdNurse?id=" + id, null);
            List<NurseModels> doctors = JsonConvert.DeserializeObject<List<NurseModels>>(result);
            var doc = doctors.Where(s => s.N_Id == id).FirstOrDefault();
            return View(doc);
        }
        /// <summary>
        /// 医生修改
        /// </summary>
        /// <param name="doctor"></param>
        public void EidtorNurse(NurseModels n)
        {
            string result = Hctp.GetApi("put", "Administrator/EditNurse", n);
            if (result != "0")
            {
                Response.Write("<script>alert('修改成功');location.href='/Administrator/NurseShow';</script>");

            }
            else
            {
                Response.Write("修改失败");
            }
        }

    }
}