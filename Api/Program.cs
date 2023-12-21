using Application.Services.Employees;
using Domain.Interfaces;
using Domain.SharedKernel;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AttachmentsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();

builder.Services.AddScoped<EmployeeService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("all",
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();


app.UseCors("all");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\m.moien.ext\\Desktop\\attachments-api\\Application\\Files\\")),
    RequestPath = "/pdfs"
});

app.MapControllers();

app.Run();
