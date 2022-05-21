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
        public async Task<ActionResult<IEnumerable<BNBResponse>>> GetBNB()
        {
            List<BNBResponse> result = new List<BNBResponse>();
            var bnbs = await _context.BNBs.ToListAsync();

            bnbs.ForEach(bnb => result.Add(new BNBResponse
            {
                ID = bnb.ID,
                Description = bnb.Description,
                Title = bnb.Title,
                Address = bnb.Address,
                Country = bnb.Country,
                ZipCode = bnb.ZipCode,
                TypeOfEstablishment = bnb.TypeOfEstablishment
            }));

            return result;
        }

        // GET: api/BNBs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BNBResponse>> GetBNB(int id)
        {
            var bNB = await _context.BNBs.Include(x => x.RentingPeriods).Include(x => x.TenantPeriods).SingleAsync(x => x.ID == id);

            if (bNB == null)
            {
                return NotFound();
            }

            BNBResponse bNBResponse = new BNBResponse();
            bNBResponse.ID = bNB.ID;
            bNBResponse.Address = bNB.Address;
            bNBResponse.ZipCode = bNB.ZipCode;
            bNBResponse.Country = bNB.Country;
            bNBResponse.Title = bNB.Title;
            bNBResponse.Description = bNB.Description;
            bNBResponse.TypeOfEstablishment = bNB.TypeOfEstablishment;

            List<PossibleRentingPeriodResponse> possibleRentingPeriodResponse = new List<PossibleRentingPeriodResponse>();

            bNB.RentingPeriods.ForEach(x => possibleRentingPeriodResponse.Add(
                new PossibleRentingPeriodResponse{ 
                    PossibleRentingPeriodID = x.PossibleRentingPeriodID,
                    DailyPrice = x.DailyPrice, 
                    EndDate = x.EndDate, 
                    MinimumRentingDays = x.MinimumRentingDays,
                    StartDate = x.StartDate
                }
            ));
            bNBResponse.RentingPeriods = possibleRentingPeriodResponse;

            bNB.TenantPeriods.ForEach(x => bNBResponse.TenantPeriods.Add(
                new TenantPeriodResponse
                {
                    TenantPeriodID = x.TenantPeriodID,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate
                }
            ));

            return bNBResponse;
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
        public async Task<ActionResult<BNBResponse>> PostBNB(CreateBNB createBNB)
        {
            User user = CheckUserExists();
            if (user == null)
            {
                return Unauthorized();
            }

            BNB bNB = new BNB();
            bNB.Title = createBNB.Title;
            bNB.Description = createBNB.Description;
            bNB.Address = createBNB.Address;
            bNB.ZipCode = createBNB.ZipCode;
            bNB.Country = createBNB.Country;
            bNB.TypeOfEstablishment = createBNB.TypeOfEstablishment;
            bNB.Owner = user;
            bNB.Images = new List<BnbImage>();
            bNB.RentingPeriods = new List<PossibleRentingPeriod>();
            bNB.TenantPeriods = new List<TenantPeriod>();

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

        private bool BNBExists(int id)
        {
            return _context.BNBs.Any(e => e.ID == id);
        }

        private User CheckUserExists()
        {
            string userId = User.Claims.First().Value.First().ToString();

            if(string.IsNullOrWhiteSpace(userId))
                return null;
            
            User user = _context.Users.Single(x => x.UserID == int.Parse(userId));

            return user;
        }
    }
}
