using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Web.DTO;
using CodingPlatform.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingPlatform.Web.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ChallengeController : CustomControllerBase
{
    private readonly IChallengeService _challengeService;

    public ChallengeController(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService)
        : base(httpContextAccessor)
    {
        _challengeService = challengeService;
    }

    [HttpGet("challenge/{challengeId}")]
    public async Task<IActionResult> GetChallengeById(long challengeId)
    {
        var challenge = await _challengeService.GetChallengeByIdAsync(challengeId, GetCurrentUserId());
        return Ok(new ChallengeDto()
        {
            Id = challenge.Id,
            Title = challenge.Title,
            Description = challenge.Description,
            StartDate = challenge.CreateDate,
            EndDate = challenge.EndDate,
            Tips = challenge.Tips?.Select(t => t.Description)
        });
    }

    [HttpPost("challenge")]
    public async Task<IActionResult> CreateChallenge(CreateChallengeDto param)
    {
        var challenge = await _challengeService.CreateChallenge(param.TournamentId,
        GetCurrentUserId(),
        param.Title,
        param.Description,
        param.Hours,
        param.Tips);

        return CreatedAtAction(nameof(GetChallengeById), new { challengeId = challenge.Id }, new ChallengeDto()
        {
            Id = challenge.Id,
            Title = challenge.Title,
            Description = challenge.Description,
            StartDate = challenge.CreateDate,
            EndDate = challenge.EndDate,
            Tips = challenge.Tips?.Select(t => t.Description)
        });
    }

    [HttpGet("user/challenges")]
    public async Task<IActionResult> GetChallengesByUser([FromQuery] bool onlyActive)
    {
        var userInProgressChallenges = await _challengeService.GetChallengesByUserAsync(GetCurrentUserId(), onlyActive);

        return Ok(userInProgressChallenges.Select(c => new UserChallenges
        {
            Id = c.Id,
            Title = c.Title,
            StartDate = c.CreateDate,
            EndDate = c.EndDate,
            Description = c.Description
        }));
    }

    [HttpGet("admin/challenges")]
    public async Task<IActionResult> GetChallengesByAdmin()
    {
        var challenges = await _challengeService.GetChallengesByAdminAsync(GetCurrentUserId());

        return Ok(challenges.Select(c => new UserChallenges
        {
            Id = c.Id,
            Title = c.Title,
            StartDate = c.CreateDate,
            EndDate = c.EndDate,
            Description = c.Description
        }));
    }

    [HttpPost("challenge/start/{challengeId}")]
    public async Task<IActionResult> ChallengeStart(long challengeId)
    {
        var submission = await _challengeService.StartChallengeAsync(challengeId, GetCurrentUserId());
        return CreatedAtAction(nameof(ChallengeStatus), new { challengeId = challengeId }, submission.ToDTO());
    }

    [HttpGet("challenge/status/{challengeId}")]
    public async Task<IActionResult> ChallengeStatus(long challengeId)
    {
        var submission = await _challengeService.GetSubmissionStatusAsync(challengeId, GetCurrentUserId());
        return Ok(submission.ToDTO());
    }

    [HttpPost("challenge/tip")]
    public async Task<IActionResult> ChallengeTip(long challengeId)
    {
        var tip = await _challengeService.RequestNewTipAsync(challengeId, GetCurrentUserId());
        return Ok("New tip available");
    }

    [HttpPost("challenge/end/{challengeId}")]
    public async Task<IActionResult> ChallengeEnd(long challengeId, [FromBody] string content)
    {
        var submission = await _challengeService.EndChallengeAsync(challengeId, content, GetCurrentUserId());
        return CreatedAtAction(nameof(ChallengeStatus), new { challengeId = challengeId }, submission.ToDTO());
    }
}