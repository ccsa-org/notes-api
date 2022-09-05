using CCSANoteApp.Auth;
using CCSANoteApp.DB;
using CCSANoteApp.DB.Configurations;
using CCSANoteApp.DB.Repositories;
using CCSANoteApp.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(DBConfiguration)).Get<DBConfiguration>());

builder.Services.AddSingleton <IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<INoteService,NoteService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<NoteRepository>();   
builder.Services.AddScoped<TokenRepository>();
builder.Services.AddSingleton<SessionFactory>();
CCSANoteApp.Auth.Configuration.SetupAuthentication(builder.Services, builder.Configuration);

builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
