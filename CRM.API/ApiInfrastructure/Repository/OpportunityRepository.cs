using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class OpportunityRepository: BaseRepository,IOpportunityRepository
    {
        public OpportunityRepository(AppDbContext context): base(context) { }

        public async Task<SellOpportunityHeader> AddOpportunityAsync(SellOpportunityHeader opportunity)
        {
            using (var transact = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SellOpportunityPositions.AddRangeAsync(opportunity.Positions);
                    await _context.SellOpportunityHeaders.AddAsync(opportunity);
                    await _context.SaveChangesAsync();
                    transact.Commit();
                }
                catch (Exception ex)
                {
                    await _context.Logs.AddAsync(new Log
                    {
                        LogMessage = ex.Message,
                        ModuleName = "OpportunityRepository/AddOpportunityAsync"
                    });
                    transact.Rollback();

                    throw;
                }
            }
            return opportunity;
        }

        public async Task<List<SellOpportunityHeader>> GetAllOpportunitiesAsync(int companyId, List<string> filters, DateTime DateFrom, DateTime DateTo)
        {
            var list = _context
                .SellOpportunityHeaders
                .Include(x => x.Status)
                .Include(x => x.Lead)
                .Include(x => x.Positions)
                .ThenInclude(x => x.Product)
                .Include(x => x.Trader)
                .Where(x => x.StatusId != 5 && x.CompanyId == companyId && x.CreateDate.Date >= DateFrom.Date && x.CreateDate.Date <= DateTo.Date);

            if (filters[0] != "" && filters[0] != null)
            {
                list = list.Where(x => x.Status.Name == filters[0]);
            }
            if (filters[1] != "" && filters[1]!=null)
            {
                list = list.Where(x => x.Lead.Name.Contains(filters[1]));
            }
            if (filters[2] != "" && filters[2] != null)
            {
                list = list.Where(x => (x.Trader.FirstName + " " + x.Trader.LastName).Contains(filters[2]));
            }

            return await list
                .OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<List<SellOpportunityHeader>> GetAllOrdersAsync(int companyId, List<string> filters, DateTime DateFrom, DateTime DateTo)
        {
            var list = _context
                .SellOpportunityHeaders
                .Include(x => x.Status)
                .Include(x => x.Lead)
                .Include(x => x.Positions)
                .ThenInclude(x => x.Product)
                .Include(x => x.Trader)
                .Where(x => x.StatusId == 5 && x.CompanyId == companyId && x.CreateDate.Date >= DateFrom.Date && x.CreateDate.Date <= DateTo.Date);

            if (filters[0] != "" && filters[0] != null)
            {
                list = list.Where(x => x.Lead.Name.Contains(filters[1]));
            }
            if (filters[1] != "" && filters[1] != null)
            {
                list = list.Where(x => (x.Trader.FirstName + " " + x.Trader.LastName).Contains(filters[2]));
            }

            return await list
                .OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<SellOpportunityHeader> GetOpportunityAsync(int headerId, int companyId)
        {
            return await _context
                .SellOpportunityHeaders
                .Include(x => x.Status)
                .Include(x => x.Lead)
                .Include(x => x.Positions)
                .ThenInclude(x => x.Product)
                .Include(x => x.Trader)
                .FirstOrDefaultAsync(x => x.Id==headerId && x.CompanyId==companyId);
        }

        public async Task<List<int>> GetUserOpportunitiesCountAsync(string userId)
        {
            var list = new List<int>();

            list.Add((await _context.SellOpportunityHeaders.Where(x => x.StatusId == 1 && x.TraderId == userId).ToListAsync()).Count);
            list.Add((await _context.SellOpportunityHeaders.Where(x => x.StatusId == 2 && x.TraderId == userId).ToListAsync()).Count);
            list.Add((await _context.SellOpportunityHeaders.Where(x => x.StatusId == 3 && x.TraderId == userId).ToListAsync()).Count);
            list.Add((await _context.SellOpportunityHeaders.Where(x => x.StatusId == 4 && x.TraderId == userId).ToListAsync()).Count);
            list.Add((await _context.SellOpportunityHeaders.Where(x => x.StatusId == 5 && x.TraderId == userId).ToListAsync()).Count);

            return list;
        }

        public async Task RemoveOpportunityPositionAsync(int positionId)
        {
            try
            {
                var position = await _context.SellOpportunityPositions.FirstOrDefaultAsync(x => x.Id == positionId);

                if (position != null)
                {
                    _context.SellOpportunityPositions.Remove(position);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "OpportunityRepository/RemoveOpportunityPositionAsync"
                });

                throw;
            }
        }

        public async Task<SellOpportunityHeader> UpdateAsync(SellOpportunityHeader opportunityHeader)
        {
            using (var transact = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var position in opportunityHeader.Positions)
                    {
                        if (position.Id == 0)
                        {
                            await _context.SellOpportunityPositions.AddAsync(position);
                        }
                        else
                        {
                            _context.SellOpportunityPositions.Update(position);
                        }
                    }
                    _context.SellOpportunityHeaders.Update(opportunityHeader);
                    await _context.SaveChangesAsync();
                    transact.Commit();
                }
                catch (Exception ex)
                {
                    await _context.Logs.AddAsync(new Log
                    {
                        LogMessage = ex.Message,
                        ModuleName = "OpportunityRepository/UpdateAsync"
                    });
                    transact.Rollback();

                    throw;
                }
            }
            return opportunityHeader;
        }
    }
}
