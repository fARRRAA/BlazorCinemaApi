using Microsoft.EntityFrameworkCore;
using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CinemaDigestApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IGenreService, GenresService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMovieChatMessages,MovieChatMessagesService>();
builder.Services.AddScoped<IUserChatService, UserChatService>(); 
builder.Services.AddScoped<IUserChatMessagesService, UserChatMessagesService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod() 
               .AllowAnyHeader(); 
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            //var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                            //if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                            //{
                            //    context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();


                            //}
                            context.Token = context.Request.Cookies["wild-cookies"];

                            return Task.CompletedTask;

                        }

                    };

                });
var app = builder.Build();
app.MapHub<ChatHub>("/chatHub");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
