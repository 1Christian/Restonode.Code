using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Restonode.API.Interfaces;
using Restonode.API.Publishers;
using Restonode.Business.Interfaces;
using Restonode.Business.Repositories;
using Restonode.Common;
using Restonode.Common.Interfaces;
using Restonode.Common.Notifiers;
using Restonode.Messaging.Consumers;
using Restonode.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace RestonodeAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private Restonode.Common.Settings.SwaggerSettings SwaggerSettings =
            new Restonode.Common.Settings.SwaggerSettings();

        private Restonode.Common.Settings.MvcSettings MvcSettings = new Restonode.Common.Settings.MvcSettings();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<Restonode.Common.Settings.RabbitMQSettings>(Configuration.GetSection("RabbitMQ"));
            services.Configure<Restonode.Common.Settings.SwaggerSettings>(Configuration.GetSection("Swagger"));
            services.Configure<Restonode.Common.Settings.MvcSettings>(Configuration.GetSection("Mvc"));
            services.Configure<Restonode.Common.Settings.MailSettings>(Configuration.GetSection("Mail"));
            services.AddMvcCore().AddApiExplorer();
            services.AddSingleton<IPublisher, Publisher>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IMealRepository, MealRepository>();
            services.AddTransient<IScoreRepository, ScoreRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IMessage, BaseMessage>();
            services.AddTransient<IMailNotifier, MailNotifier>();
            services.AddTransient<ISmsNotifier, SmsNotifier>();
            services.AddTransient<ILocationManager, LocationManager>();
            services.AddSingleton<ConnectionFactory>();
            services.AddSingleton<Restonode.Common.Settings.MailSettings>();
            services.AddTransient(sp => new OrderMessageConsumer(sp.GetService<IModel>(), sp.GetService<IMailNotifier>()));
            services.AddTransient(sp => new NotificationMessageConsumer(sp.GetService<IModel>(), sp.GetService<ISmsNotifier>()));

            Configuration.GetSection("Swagger").Bind(SwaggerSettings);

            Configuration.GetSection("Mvc").Bind(MvcSettings);

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(SwaggerSettings.DocVersion, new Info
                {
                    Version = SwaggerSettings.DocVersion,
                    Title = SwaggerSettings.Title,
                    Description = SwaggerSettings.Description,
                    TermsOfService = SwaggerSettings.TermOfService
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(c => { c.RouteTemplate = SwaggerSettings.RouteTemplate; });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerSettings.SwaggerEndpoint, SwaggerSettings.Title);
            });

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(MvcSettings.MapRouteName, MvcSettings.MapRouteTemplate);
            });

            app.UseHttpsRedirection();
        }
    }
}
