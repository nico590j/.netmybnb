#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mybnb.api.Data;
using Mybnb.api.Models;

namespace Mybnb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PossibleRentingPeriodsController : ControllerBase
    {
        private readonly MybnbapiContext _context;

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
        [HttpPost]
        public async Task<ActionResult<PossibleRentingPeriod>> PostPossibleRentingPeriod(PossibleRentingPeriod possibleRentingPeriod)
        {
            _context.PossibleRentingPeriods.Add(possibleRentingPeriod);
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
    }
}
