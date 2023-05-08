namespace tuto
{
    public class Startup

    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            app.UseMvc();
            app.Run(context => {
                return context.Response.WriteAsync("Hello World!");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        /*public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext...
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSession();
        }*/
    }
}

