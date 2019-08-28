using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedbackend.domain.config;
using advancedbackend.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace advancedbackend
{
    public class Startup
    {
        ILogger Logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpClient();
            services.AddTransient<IHttpClientFactoryWrapper, HttpClientFactoryWrapper>();
            services.AddSingleton<IBase64, Base64>();
            services.AddSingleton<ICityMusicService, CityMusicService>();
            services.AddSingleton<ISpotifyService, SpotifyService>();
            services.AddSingleton<IWeatherService, WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
