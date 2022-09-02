using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Bu kodlar yerine Autofac kulland�k.

//builder.Services.AddSingleton<ICarService, CarManager>();
//builder.Services.AddSingleton<ICarDal, EfCarDal>();

//builder.Services.AddSingleton<IRentalService, RentalManager>();
//builder.Services.AddSingleton<IRentalDal, EfRentalDal>();

//builder.Services.AddSingleton<IColorDal, EfColorDal>();
//builder.Services.AddSingleton<IColorService, ColorManager>();

//builder.Services.AddSingleton<IBrandDal, EfBrandDal>();
//builder.Services.AddSingleton<IBrandService, BrandManager>();

//builder.Services.AddSingleton<ICustomerDal, EfCustomerDal>();
//builder.Services.AddSingleton<ICustomerService, CustomerManager>();

//builder.Services.AddSingleton<IUserDal, EfUserDal>();
//builder.Services.AddSingleton<IUserService, UserManager>();

//Autofac'i aya�a kald�rd�k.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
