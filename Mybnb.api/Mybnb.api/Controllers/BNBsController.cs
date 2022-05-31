#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public BNBsController(MybnbapiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BNBs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BNBResponse>>> GetBNB()
        {
            var bnbs = await _context.BNBs.ToListAsync();

            List<BNBResponse> result = _mapper.Map<List<BNB>, List<BNBResponse>>(bnbs);

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

            BNBResponse bNBResponse = _mapper.Map<BNB, BNBResponse>(bNB);
            
            List<PossibleRentingPeriodResponse> possibleRentingPeriodResponse = _mapper.Map<List<PossibleRentingPeriod>, List<PossibleRentingPeriodResponse>>(bNB.RentingPeriods);
            bNBResponse.RentingPeriods = possibleRentingPeriodResponse;

            List<TenantPeriodResponse> tenantPeriodResponse = _mapper.Map<List<TenantPeriod>, List<TenantPeriodResponse>>(bNB.TenantPeriods);
            bNBResponse.TenantPeriods = tenantPeriodResponse;

            return bNBResponse;
        }

        // PUT: api/BNBs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBNB(int id, BNBRequest bNBRequest)
        {
            if (id != bNBRequest.ID)
            {
                return BadRequest();
            }

            BNB bnb = _mapper.Map<BNBRequest, BNB>(bNBRequest);
            
            _context.Entry(bnb).State = EntityState.Modified;

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
        public async Task<ActionResult<BNBResponse>> PostBNB(BNBRequest bNBRequest)
        {
            User user = CheckUserExists();
            if (user == null)
            {
                return Unauthorized();
            }

            BNB bNB = _mapper.Map<BNBRequest, BNB>(bNBRequest);

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
