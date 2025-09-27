using FootballDataOrg;
using OneOf;
using PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;
using PremierRoom.API.Endpoints.Teams.GetTeamById;
using PremierRoom.Application;
using PremierRoom.Application.Features.Teams.GetAllAvailableTeams;
using PremierRoom.Application.Features.Teams.GetTeamById;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.Enhancers;
using PremierRoom.Application.FootballDataService.Enhancers.ProfilePicture;
using PremierRoom.Application.FootballDataService.FootballDataOrg;
using PremierRoom.Application.FootballDataService.FootballDataOrg.Cache;
using PremierRoom.Application.Models;
using TheSportsDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("https://localhost:9092")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddFootballDataOrgApi(builder.Configuration);
builder.Services.AddTheSportsDbApi(builder.Configuration);

builder.Services.AddTransient<IFootballDataService, FootballDataOrgFootballDataService>();
builder.Services.Decorate<IFootballDataService, CacheableFootballDataOrgFootballDataService>();

builder.Services.AddTransient<IQueryHandler<GetAllAvailableTeamsQuery, IEnumerable<Team>>, GetAllAvailableTeamsQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>>, GetTeamByIdQueryHandler>();
builder.Services.AddTransient<IPlayerEnhancer, TheSportsDbProfilePictureEnhancer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGroup("teams")
    .AddGetAllAvailableTeamsEndpoint()
    .AddGetTeamByIdEndpoint();

app.Run();