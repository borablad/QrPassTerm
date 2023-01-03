﻿using QrPassTerm.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrPassTerm.Helpers
{
    public partial class Constans:BaseViewModel
    {
       /* public static string HostUrl = "192.168.31.62";
        public static string Scheme = "http";
        public static string Port = "8000";
  */
        public static string RestUrl = $"{Sheme}://{HostUrl}:{Port}/{{0}}";

        public static string GetQr = "Visit/GainQr";
        public static string Login = "Auth/Token";
        public static string Visits = "Visits";
        public static string Register = "User/Register";
    }
}
