﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalClinicKHD.Models
{
    public class StaffLoginModels
    {
        public int Sl_Id { get; set; } //主键  登录表Id
        public string Sl_Name { get; set; } // 登陆名称
        public string Sl_Pwd { get; set; } // 登陆密码
        public int Sl_Type { get; set; } // 员工类型

    }
}