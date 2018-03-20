using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebGeestar.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Data.Sqlite;

namespace WebGeestar.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;
        public HomeController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        private static SqliteConnection GetConnection()
        {
            //string connectionLink = @"E:\Code\HCUpLoad\EnvironmentService\bin\Debug\enviromentmonitordatabase.sqlite";
            string connectionLink = @"E:\云端测试\Debug\enviromentmonitordatabase.sqlite";
            SqliteConnection conn = new SqliteConnection();
            SqliteConnectionStringBuilder connstr = new SqliteConnectionStringBuilder();
            connstr.DataSource = connectionLink;
            //MessageBox.Show(connstr.DataSource);
            //connstr.Password = "admin";//设置密码，sqlite ado.net实现了数据库密码保护
            conn.ConnectionString = connstr.ToString();
            conn.Open();
            return conn;
        }

        public void Exception()
        {
            string message = "";
            using (SqliteConnection conn = GetConnection())
            {
                using (SqliteCommand cmd = new SqliteCommand("select * from ExceptionMessageTimer ", conn))
                {
                    try
                    {
                        using (SqliteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                message = message + dr["ResponseInfo"] + "," + Environment.NewLine;
                            }
                        }



                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            ViewData["ExceptionMessage"] = message;

        }

        public IActionResult Index()
        {
            string message = "";
            using (SqliteConnection conn = GetConnection())
            {
                using (SqliteCommand cmd = new SqliteCommand("select * from MessageTimer ", conn))
                {
                    try
                    {
                        using (SqliteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                        {
                                message = message + dr["ResponseInfo"] + ","+Environment.NewLine;
                            }
                        }
                                                  

                        
                    }
                    catch (Exception ex)
                    {
                       
                    }
                }
            }

            ViewData["IndexMessage"] = message;
            List<string> listfile = new List<string>();
            DirectoryInfo d = new DirectoryInfo(hostingEnv.WebRootPath);
            //FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
            //foreach (FileSystemInfo fsinfo in fsinfos)
            //{
            foreach (var file in d.EnumerateFiles())
            {
                listfile.Add(file.Name);
            }
           
           // listfile.Add("A2");
            return View(listfile);
        }

        public IActionResult HtmlIndex()
        {
          
            return Redirect("1.html");
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult File(string value)
        {
            long size = 0;
            var filename = "";
            string message = "";
            try
            {
                var files = Request.Form.Files;

                foreach (var file in files)
                {
                    filename = ContentDispositionHeaderValue
                           .Parse(file.ContentDisposition)
                           .FileName
                           .Trim('"');

                    filename = hostingEnv.WebRootPath + $@"/{filename}";
                    //filename = @"C:\Users\lg.HL\Desktop" + $@"\{filename}";
                    size += file.Length;
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }


                message = $"{files.Count} file(s) / { size}bytes have been uploaded successfully!";
            }
            catch(Exception ex)
            {

            }
           
            return Json(message + filename);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
