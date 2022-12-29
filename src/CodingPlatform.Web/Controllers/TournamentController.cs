using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Web.DTO;
using CodingPlatform.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingPlatform.Web.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class TournamentController : CustomControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentController(IHttpContextAccessor httpCtxAccessor, ITournamentService tournamentService)
        : base(httpCtxAccessor)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet("tournaments")]
    public async Task<IActionResult> GetTournaments([FromQuery] SearchTournamentDto param)
    {
        var tournaments = await _tournamentService.GetTournaments(param.TournamentName);
        return Ok(tournaments.Select(t => t.ToDTO()));
    }

    [HttpGet("tournament/{tournamentId}")]
    public async Task<IActionResult> GetTournamentById(long tournamentId)
    {
        var tournament = await _tournamentService.GetTournamentByIdAsync(tournamentId);
        return Ok(tournament.ToDTO());
    }

    [HttpPost("tournament")]
    public async Task<IActionResult> CreateTournament(CreateTournamentDto param)
    {
        var tournament = await _tournamentService.Create(param.TournamentName, param.MaxParticipants,
            GetCurrentUserId());

        return CreatedAtAction(nameof(GetTournaments), new { id = tournament.Id }, tournament);
    }

    [HttpPost("tournament/subscription/{tournamentId}")]
    public async Task<IActionResult> Subscription(long tournamentId)
    {
        await _tournamentService.SubscribeUserRefactor(tournamentId, GetCurrentUserId());
        return Ok("Subscription created");
    }
}