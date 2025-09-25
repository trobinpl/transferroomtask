using FootballDataOrg;
using PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.FootballDataOrg;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFootballDataApi(builder.Configuration);

builder.Services.AddTransient<IFootballDataService, FootballDataOrgDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("teams")
    .AddGetAllAvailableTeamsEndpoint();

app.Run();