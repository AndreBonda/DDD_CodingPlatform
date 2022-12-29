using CodingPlatform.Domain;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;

namespace CodingPlatform.Domain.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _submissionRepo;

    public SubmissionService(ISubmissionRepository submissionRepository)
    {
        _submissionRepo = submissionRepository;
    }

    public async Task<IEnumerable<Submission>> GetSubmissionsByChallengeAsync(long challengeId, bool onlySubmitted, bool excludeEvaluated, long userId)
        => await _submissionRepo.GetSubmissionsByChallengeAsync(challengeId, onlySubmitted, excludeEvaluated, userId);

    public async Task<Submission> EvaluateSubmission(long submissionId, int score, long userId)
    {
        var submission = await _submissionRepo.GetByIdAsync(submissionId);
        submission.Evaluate(score, userId);
        await _submissionRepo.UpdateAsync(submission);
        return submission;
    }
}