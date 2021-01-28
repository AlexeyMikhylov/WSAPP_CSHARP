using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace app_XX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static string MainConnection = @"Data Source=LAPTOP-R79E5F1S\SQLEXPRESS;Initial Catalog=WSDEMOEX;Integrated Security=True";
        public static string MainConnection = "Data Source=vc-stud-mssql1;Initial Catalog=user53_db;User ID=user53_db;Password=user53";
    }
}
