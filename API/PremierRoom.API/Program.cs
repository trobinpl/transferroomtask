using FootballDataOrg;
using OneOf;
using PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;
using PremierRoom.API.Endpoints.Teams.GetTeamById;
using PremierRoom.Application;
using PremierRoom.Application.Features.Teams.GetAllAvailableTeams;
using PremierRoom.Application.Features.Teams.GetTeamById;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.FootballDataOrg;
using PremierRoom.Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFootballDataApi(builder.Configuration);

builder.Services.AddTransient<IFootballDataService, FootballDataOrgFootballDataService>();

builder.Services.AddTransient<IQueryHandler<GetAllAvailableTeamsQuery, IEnumerable<Team>>, GetAllAvailableTeamsQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>>, GetTeamByIdQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("teams")
    .AddGetAllAvailableTeamsEndpoint()
    .AddGetTeamByIdEndpoint();

app.Run();