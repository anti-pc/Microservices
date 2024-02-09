using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});
builder.Services.AddScoped<ICategoryService, CategoryService>(); //added
builder.Services.AddScoped<ICourseService, CourseService>(); //added

builder.Services.AddAutoMapper(typeof(Program)); //added
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddControllers(options => options.Filters.Add(new AuthorizeFilter()));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); //added
builder.Services.AddSingleton<IDatabaseSettings>(sp => { return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; }); //added

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
