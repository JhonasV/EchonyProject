using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Service;

namespace EchonyCore
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


            services.AddMvc()
            .AddSessionStateTempDataProvider()
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
;
             });

            services.AddSession();

            var connection = Configuration.GetConnectionString("EchonyDB");
            services.AddDbContext<EchonyDbContext>(options => options.UseSqlServer(connection));
            //My dependecies
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPublicacionesService, PublicacionesService>();
            services.AddTransient<IComentariosService, ComentariosService>();
            services.AddTransient<ICommentReplyService, CommentReplyService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<ISolicitudAmistadService, SolicitudAmistadService>();
            services.AddTransient<INotificacionService, NotificacionService>();
            services.AddTransient<ILikesService, LikesService>();

            services.AddSingleton<ChatMessage>();

            services.AddCors(option => option.AddPolicy("CorsPolicy", p => 
            p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
           // services.AddSignalR();
            /*services.AddDbContext<EchonyEntityContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("EchonyDB")));*/

        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
            }
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
           // app.UseSignalR(option => option.MapHub<ChatHub>("chatHub"));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
