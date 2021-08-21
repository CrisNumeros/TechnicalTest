using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalTest.Models
{
    public static class Utils
    {
        public static void SetAlert(dynamic viewBag, string title, AlertColors color)
        {
            viewBag.AlertMessage = title;
            viewBag.AlertClass = "alert-" + color.ToString();
        }

        public enum AlertColors
        {
            primary,
            secondary,
            success,
            danger,
            warning,
            info,
            light,
            dark
        }
    }
}