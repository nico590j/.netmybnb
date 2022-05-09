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
    public class TenantPeriodsController : ControllerBase
    {
        private readonly MybnbapiContext _context;

        public TenantPeriodsController(MybnbapiContext context)
        {
            _context = context;
        }

        // GET: api/TenantPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantPeriod>>> GetTenantPeriod()
        {
            return await _context.TenantPeriods.ToListAsync();
        }

        // GET: api/TenantPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TenantPeriod>> GetTenantPeriod(int id)
        {
            var tenantPeriod = await _context.TenantPeriods.FindAsync(id);

            if (tenantPeriod == null)
            {
                return NotFound();
            }

            return tenantPeriod;
        }

        // PUT: api/TenantPeriods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenantPeriod(int id, TenantPeriod tenantPeriod)
        {
            if (id != tenantPeriod.TenantPeriodID)
            {
                return BadRequest();
            }

            _context.Entry(tenantPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantPeriodExists(id))
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

        // POST: api/TenantPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TenantPeriod>> PostTenantPeriod(TenantPeriod tenantPeriod)
        {
            _context.TenantPeriods.Add(tenantPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTenantPeriod", new { id = tenantPeriod.TenantPeriodID }, tenantPeriod);
        }

        // DELETE: api/TenantPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenantPeriod(int id)
        {
            var tenantPeriod = await _context.TenantPeriods.FindAsync(id);
            if (tenantPeriod == null)
            {
                return NotFound();
            }

            _context.TenantPeriods.Remove(tenantPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TenantPeriodExists(int id)
        {
            return _context.TenantPeriods.Any(e => e.TenantPeriodID == id);
        }
    }
}
