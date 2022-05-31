#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mybnb.api.Data;
using Mybnb.api.Models;
using Mybnb.dtolibrary.DTOs.BNB;

namespace Mybnb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PossibleRentingPeriodsController : ControllerBase
    {
        private readonly MybnbapiContext _context;
        private object bNBRequest;

        public PossibleRentingPeriodsController(MybnbapiContext context)
        {
            _context = context;
        }

        // GET: api/PossibleRentingPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PossibleRentingPeriod>>> GetPossibleRentingPeriod()
        {
            return await _context.PossibleRentingPeriods.ToListAsync();
        }

        // GET: api/PossibleRentingPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PossibleRentingPeriod>> GetPossibleRentingPeriod(int id)
        {
            var possibleRentingPeriod = await _context.PossibleRentingPeriods.FindAsync(id);

            if (possibleRentingPeriod == null)
            {
                return NotFound();
            }

            return possibleRentingPeriod;
        }

        // PUT: api/PossibleRentingPeriods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPossibleRentingPeriod(int id, PossibleRentingPeriod possibleRentingPeriod)
        {
            if (id != possibleRentingPeriod.PossibleRentingPeriodID)
            {
                return BadRequest();
            }

            _context.Entry(possibleRentingPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PossibleRentingPeriodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PossibleRentingPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("bnb/{bnbID}")]
        public async Task<ActionResult<PossibleRentingPeriodResponse>> PostPossibleRentingPeriod(int bnbID, PossibleRentingPeriodRequest possibleRentingPeriodRequest)
        {
            if (bnbID == null)
            {
                return BadRequest();
            }
            User user = CheckUserExists();
            BNB bNB = _context.BNBs.Include(x => x.Owner).Include(x => x.RentingPeriods).Single(x => x.ID == bnbID);

            if (user == null)
            {
                return BadRequest();
            }

            if (bNB.Owner.UserID != user.UserID)
            {
                return Unauthorized();
            }

            PossibleRentingPeriod possibleRentingPeriod = new PossibleRentingPeriod();
            possibleRentingPeriod.StartDate = possibleRentingPeriodRequest.StartDate;
            possibleRentingPeriod.EndDate = possibleRentingPeriodRequest.EndDate;
            possibleRentingPeriod.MinimumRentingDays = possibleRentingPeriodRequest.MinimumRentingDays;
            possibleRentingPeriod.DailyPrice = possibleRentingPeriodRequest.DailyPrice;

            bNB.RentingPeriods.Add(possibleRentingPeriod);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPossibleRentingPeriod", new { id = possibleRentingPeriod.PossibleRentingPeriodID }, possibleRentingPeriod);
        }

        // DELETE: api/PossibleRentingPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePossibleRentingPeriod(int id)
        {
            var possibleRentingPeriod = await _context.PossibleRentingPeriods.FindAsync(id);
            if (possibleRentingPeriod == null)
            {
                return NotFound();
            }

            _context.PossibleRentingPeriods.Remove(possibleRentingPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PossibleRentingPeriodExists(int id)
        {
            return _context.PossibleRentingPeriods.Any(e => e.PossibleRentingPeriodID == id);
        }

        private User CheckUserExists()
        {
            string userId = User.Claims.First().Value.First().ToString();

            if (string.IsNullOrWhiteSpace(userId))
                return null;

            User user = _context.Users.Single(x => x.UserID == int.Parse(userId));

            return user;
        }
    }
}
