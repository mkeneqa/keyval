using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace KeyValWpf
{
   static class Common
    {
        //http://stackoverflow.com/questions/5468342/how-to-modify-my-app-exe-config-keys-at-runtime
        //http://msdn.microsoft.com/en-us/library/aa983326(v=vs.90).aspx
        //string firsttime = ConfigurationSettings.AppSettings
        //public static bool IsFirstTimeRun = Con .AppSettings["IsRunningForFirstTime"];

       //string exeFolder = System.IO.Path.GetDirectoryName();

       public static string dmn = AppDomain.CurrentDomain.BaseDirectory;
        public static string DatabaseDirectory = "~\\Resources\\KeyVal.s3db";
        public static string DBFileLocation = AppDomain.CurrentDomain.BaseDirectory + @"Resources\KeyVal.s3db";
        public static string DatabaseConnString = "Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Resources\KeyVal.s3db;Version=3;";
    }
}
