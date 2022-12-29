using CodingPlatform.Domain.Factories;
using CodingPlatform.Domain.Interfaces;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Interfaces.Utility;
using CodingPlatform.Domain.Services;
using CodingPlatform.Infrastructure.Repositories;
using CodingPlatform.Infrastructure.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace CodingPlatform.Infrastructure.StartUp;

public static class DependencyInjection
{
    public static IServiceCollection SetupServicesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddScoped<IChallengeRepository, ChallengeRepository>();
        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
        services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
        services.AddScoped<ISubmissionRepository, SubmissionRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<IChallengeService, ChallengeService>();
        services.AddScoped<ILeaderboardService, LeaderboardService>();
        services.AddScoped<ISubmissionService, SubmissionService>();

        services.AddScoped<ILeaderboardFactory, LeaderboardFactory>();
        services.AddSingleton<IAuthenticationProvider, SHA512AuthenticationProvider>();

        return services;
    }
}