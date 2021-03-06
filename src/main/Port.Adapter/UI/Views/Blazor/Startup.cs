using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using neurUL.Common.Http;
using ei8.Cortex.Diary.Application.Dependency;
using ei8.Cortex.Diary.Application.Identity;
using ei8.Cortex.Diary.Application.Neurons;
using ei8.Cortex.Diary.Application.Notifications;
using ei8.Cortex.Diary.Application.Settings;
using ei8.Cortex.Diary.Nucleus.Client.In;
using ei8.Cortex.Diary.Nucleus.Client.Out;
using ei8.Cortex.Diary.Port.Adapter.IO.Process.Services;
using ei8.Cortex.Diary.Port.Adapter.IO.Process.Services.Identity;
using ei8.Cortex.Diary.Port.Adapter.IO.Process.Services.Settings;
using ei8.Cortex.Diary.Port.Adapter.UI.Views.Blazor.Data;
using ei8.EventSourcing.Client;

namespace ei8.Cortex.Diary.Port.Adapter.UI.Views.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            var ssi = new SettingsServiceImplementation();
            var dp = new DependencyService(ssi);
            var ss = new SettingsService(dp);
            var ts = new TokenService(ss);
            var rp = new RequestProvider();
            var nec = new HttpNeuronClient(rp, ts);
            var esf = new IO.Process.Services.Events.EventSourceFactory();
            var nas = new NotificationApplicationService(esf);
            var neas = new NeuronApplicationService(nec);
            var nqc = new HttpNeuronQueryClient(rp, ts);
            var nqs = new NeuronQueryService(nqc);

            services.AddSingleton<IDependencyService>(dp);            
            services.AddSingleton<ISettingsService>(ss);            
            services.AddSingleton<IRequestProvider>(rp);
            services.AddSingleton<IIdentityService>(new IdentityService(ss, rp));
            services.AddSingleton<IEventSourceFactory>(esf);
            services.AddSingleton<INotificationApplicationService>(nas);
            services.AddSingleton<INeuronClient>(nec);
            services.AddSingleton<INeuronApplicationService>(neas);
            services.AddSingleton<ITokenService>(ts);
            services.AddSingleton<INeuronQueryClient>(nqc);
            services.AddSingleton<INeuronQueryService>(nqs);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/d23");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });            
        }
    }
}
