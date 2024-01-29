using api_wov.Extensions;
using api_wov.Models.Entities;
using api_wov.Models.Packets;
using api_wov.Models.QuestPackets;
using api_wov.Models.Quests;
using api_wov.Models.RegisterQuests;
using api_wov.Repository.Packets;
using api_wov.Repository.QuestPackets;
using api_wov.Repository.Quests;
using api_wov.Repository.RegisterQuests;
using api_wov.Services;
using api_wov.Services.RequestResults;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WovDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IQuestsRepository<Quest, OutputQuest, Guid>, QuestRepository>();
builder.Services.AddScoped<IPacketsRepository<Packet, OutputPacket, Guid>, PacketRepository>();
builder.Services.AddScoped<IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid>, QuestPacketRepository>();
builder.Services.AddScoped<IRegisterQuestsRepository<RegisterQuest, OutputRegisterQuest, Guid>, RegisterQuestsRepository>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                .AllowAnyHeader()
                                                .Build()));

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.WebHost.ConfigureKestrel(w =>
//{
//    w.Listen(IPAddress.Any, builder.Configuration.GetValue("ApiWov", 80));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    c.DefaultModelsExpandDepth(-1));
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
