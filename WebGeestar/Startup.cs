using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileProviders;

namespace WebGeestar
{

    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //HttpContext.Features.Get().MaxRequestBodySize = 100_000_000;
            services.AddDirectoryBrowser();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //E:\Code\Blog-master\Blog-master\ASPNETCoreAPI\WebGeestar\Html

            ////////////////////////////////////////
            //var staticfile = new StaticFileOptions();
            //staticfile.FileProvider = new PhysicalFileProvider(@"E:\Code\Blog-master\Blog-master\ASPNETCoreAPI\WebGeestar\Html");//指定目录 这里指定C盘,也可以是其它目录
            //staticfile.ServeUnknownFileTypes = true;
            //staticfile.DefaultContentType = "application/x-msdownload"; //设置默认  MIME

            //  var provider = new FileExtensionContentTypeProvider();
            //provider.Mappings.Add(".log", "text/plain");//手动设置对应MIME
            //staticfile.ContentTypeProvider = provider;
            /////////////////////////////////////////////




            app.UseStaticFiles();
            //  app.UseDirectoryBrowser();



            //app.UseFileServer(new FileServerOptions()
            //{
            //    //FileProvider = new PhysicalFileProvider(@"Html"),//指定目录 这里指定C盘,也可以是其它目录

            //    EnableDirectoryBrowsing = true
            //});

            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
