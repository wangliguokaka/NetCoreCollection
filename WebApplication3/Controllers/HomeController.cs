using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message = "";
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = GetConnection())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("select * from MessageTimer where CPDate>='"+DateTime.Now.ToString("yyyy-MM-dd")+"' order by CPDate desc ", conn))
                {
                    
                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return View(dt);
        }


        public ActionResult Exception()
        {
            string message = "";
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = GetConnection())
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter("select * from ExceptionMessageTimer where CPDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' order by CPDate desc ", conn))
                {

                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return View(dt);
        }

        private static SQLiteConnection GetConnection()
        {
            //string connectionLink = @"E:\Code\HCUpLoad\EnvironmentService\bin\Debug\enviromentmonitordatabase.sqlite";
            string connectionLink = @"E:\云端测试\Debug\enviromentmonitordatabase.sqlite";
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
            connstr.DataSource = connectionLink;
            //MessageBox.Show(connstr.DataSource);
            //connstr.Password = "admin";//设置密码，sqlite ado.net实现了数据库密码保护
            conn.ConnectionString = connstr.ToString();
            conn.Open();
            return conn;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}