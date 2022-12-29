using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;

namespace CodingPlatform.Domain.Services;

public class ChallengeService : IChallengeService
{
    private readonly ITournamentRepository _tournamentRepo;
    private readonly IChallengeRepository _challengeRepo;
    private readonly ISubmissionRepository _submissionRepo;
    private readonly IUserRepository _userRepo;

    public ChallengeService(ITournamentRepository tournamentRepository, IChallengeRepository challengeRepository,
        ISubmissionRepository submissionRepository, IUserRepository userRepository)
    {
        _tournamentRepo = tournamentRepository;
        _challengeRepo = challengeRepository;
        _submissionRepo = submissionRepository;
        _userRepo = userRepository;
    }

    public async Task<Challenge> GetChallengeByIdAsync(long challengeId, long userId)
    {
        var challenge = await _challengeRepo.GetByIdAsync(challengeId);
        if (challenge == null) throw new NotFoundException("challenge not found");
        
        var tournament = await _tournamentRepo.GetTournamentByChallengeAsync(challengeId);
        if(tournament.Admin.Id != userId) throw new ForbiddenException("User is not allowed this challenge");
        return challenge;
    }

    public async Task<Challenge> CreateChallenge(long tournamentId, long userId, string title, string description, int hours, IEnumerable<string> tips)
    {
        var tournament = await _tournamentRepo.GetByIdAsync(tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament not found");

        var user = await _userRepo.GetByIdAsync(userId);
        var challenge = Challenge.CreateNew(title,
                description,
                hours,
                tips);
        tournament.AddChallenge(challenge, user);
        await _tournamentRepo.UpdateAsync(tournament);

        return challenge;
    }

    public async Task<IEnumerable<Challenge>> GetChallengesByUserAsync(long userId, bool onlyActive)
    {
        var subscribedTournaments = await _tournamentRepo.GetSubscribedTournamentsByUserAsync(userId);
        if (subscribedTournaments.Count() == 0) return Enumerable.Empty<Challenge>();

        var challenges = subscribedTournaments.SelectMany(t => t.Challenges);

        if (onlyActive)
            challenges = challenges.Where(c => c.IsActive());

        return challenges;
    }

    public async Task<IEnumerable<Challenge>> GetChallengesByAdminAsync(long userId)
    {
        var tournaments = await _tournamentRepo.GetTournamentsByAdmin(userId);
        if (tournaments.Count() == 0) return Enumerable.Empty<Challenge>();
        var challenges = tournaments.SelectMany(t => t.Challenges);
        return challenges;
    }

    public async Task<Submission> StartChallengeAsync(long challengeId, long userId)
    {
        var tournament = await _tournamentRepo.GetTournamentByChallengeAsync(challengeId);
        if (tournament == null) throw new NotFoundException("Challenge not found");

        var user = await _userRepo.GetByIdAsync(userId);
        var submission = tournament.StartChallenge(user);
        await _tournamentRepo.UpdateAsync(tournament);
        return submission;
    }

    public async Task<Submission> GetSubmissionStatusAsync(long challengeId, long userId)
    {
        var tournament = await _tournamentRepo.GetTournamentByChallengeAsync(challengeId);
        if (tournament == null) throw new NotFoundException("Challenge not found");

        var user = await _userRepo.GetByIdAsync(userId);
        var submission = tournament.GetSubmissionStatus(user, challengeId);
        return submission;
    }

    public async Task<Tip> RequestNewTipAsync(long challengeId, long userId)
    {
        var submission = await GetSubmissionStatusAsync(challengeId, userId);
        submission.RequestNewTip(userId);
        await _submissionRepo.UpdateAsync(submission);
        return submission.GetLastAvailableTips();
    }

    public async Task<Submission> EndChallengeAsync(long challengeId, string content, long userId)
    {
        var submission = await GetSubmissionStatusAsync(challengeId, userId);
        submission.EndSubmission(content, userId);
        await _submissionRepo.UpdateAsync(submission);
        return submission;
    }
}