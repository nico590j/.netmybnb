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
using Mybnb.dtolibrary.DTOs.User;
//Add Tenant and renting periods to this controller and delete the others
namespace Mybnb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BNBsController : ControllerBase
    {
        private readonly MybnbapiContext _context;

        public BNBsController(MybnbapiContext context)
        {
            _context = context;
        }

        // GET: api/BNBs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BNB>>> GetBNB()
        {
            return await _context.BNBs.ToListAsync();
        }

        // GET: api/BNBs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BNB>> GetBNB(int id)
        {
            var bNB = await _context.BNBs.FindAsync(id);

            if (bNB == null)
            {
                return NotFound();
            }

            return bNB;
        }

        // PUT: api/BNBs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBNB(int id, BNB bNB)
        {
            if (id != bNB.ID)
            {
                return BadRequest();
            }

            _context.Entry(bNB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BNBExists(id))
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

        // POST: api/BNBs
        [HttpPost]
        public async Task<ActionResult<BNB>> PostBNB(BNB bNB)
        {
            _context.BNBs.Add(bNB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBNB", new { id = bNB.ID }, bNB);
        }

        // DELETE: api/BNBs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBNB(int id)
        {
            var bNB = await _context.BNBs.FindAsync(id);
            if (bNB == null)
            {
                return NotFound();
            }

            _context.BNBs.Remove(bNB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("TenantPeriod")]
        public async Task<ActionResult<BNB>> BNBAddTenantPeriod(BNBUpdateRequest bNBRequest)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (bNBRequest.Owner.Id != int.Parse(userId))
            {
                return Unauthorized();
            }

            BNB bNB = _context.BNBs.Single(x => x.ID == bNBRequest.ID);
            BNBResponse bNBResponse = new BNBResponse();
            bNBResponse.ID = bNBRequest.ID;
            bNBResponse.Address = bNBRequest.Address;
            bNBResponse.ZipCode = bNBRequest.ZipCode;
            bNBResponse.Country = bNBRequest.Country;
            bNBResponse.Images = bNBRequest.Images;

            foreach (var tenantPeriodRequest in bNBRequest.TenantPeriods)
            {
                User user = _context.Users.Single(x => x.UserID == tenantPeriodRequest.Tenant.Id);
                TenantPeriod tenantPeriod = new TenantPeriod { EndDate = tenantPeriodRequest.EndDate, StartDate = tenantPeriodRequest.StartDate, Tenant = user };
                bNB.TenantPeriods.Add(tenantPeriod);
            }
            
            await _context.SaveChangesAsync();

            bNB.TenantPeriods.ForEach(x => bNBResponse.TenantPeriods.Add(new TenantPeriodResponse { EndDate = x.EndDate, StartDate = x.StartDate, TenantPeriodID = x.TenantPeriodID, Tenant = new UserResponse { Id = x.Tenant.UserID, Email = x.Tenant.Email } })); 
            return CreatedAtAction("GetTenantPeriod", new { id = bNB.ID }, bNBResponse);
        }

        [HttpPost("BNBRendingPeriod")]
        public async Task<ActionResult<BNB>> BNBAddRendingPeriod(BNBUpdateRequest bNBRequest)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (bNBRequest.Owner.Id != int.Parse(userId))
            {
                return Unauthorized();
            }

            BNB bNB = _context.BNBs.Single(x => x.ID == bNBRequest.ID);
            BNBResponse bNBResponse = new BNBResponse();
            bNBResponse.ID = bNBRequest.ID;
            bNBResponse.Address = bNBRequest.Address;
            bNBResponse.ZipCode = bNBRequest.ZipCode;
            bNBResponse.Country = bNBRequest.Country;
            bNBResponse.Images = bNBRequest.Images;

            foreach (var rentingPeriodRequest in bNBRequest.RentingPeriods)
            {
                PossibleRentingPeriod rentingPeriod = new PossibleRentingPeriod { 
                    EndDate = rentingPeriodRequest.EndDate, 
                    StartDate = rentingPeriodRequest.StartDate, 
                    DailyPrice = rentingPeriodRequest.DailyPrice, 
                    MinimumRentingDays = rentingPeriodRequest.MinimumRentingDays};

                bNB.RentingPeriods.Add(rentingPeriod);
            }

            await _context.SaveChangesAsync();

            bNB.RentingPeriods.ForEach(x => bNBResponse.RentingPeriods.Add(new PossibleRentingPeriodResponse { 
                EndDate = x.EndDate, 
                StartDate = x.StartDate, 
                PossibleRentingPeriodID = x.PossibleRentingPeriodID,
                DailyPrice = x.DailyPrice,
                MinimumRentingDays = x.MinimumRentingDays
            }));
            return CreatedAtAction("GetTenantPeriod", new { id = bNB.ID }, bNBResponse);
        }

        private bool BNBExists(int id)
        {
            return _context.BNBs.Any(e => e.ID == id);
        }
    }
}
