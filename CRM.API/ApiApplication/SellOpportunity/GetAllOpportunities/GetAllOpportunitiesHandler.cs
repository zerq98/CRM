using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiDomain.Entity;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.GetAllOpportunities
{
    public class GetAllOpportunitiesHandler : IRequestHandler<GetAllOpportunitiesQuery, IActionResult>
    {
        private readonly IOpportunityRepository _opportunityRepostitory;
        private readonly IUserRepository _userRepository;

        public GetAllOpportunitiesHandler(IOpportunityRepository opportunityRepository, IUserRepository userRepository)
        {
            _opportunityRepostitory = opportunityRepository;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Handle(GetAllOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var traders = await _userRepository.GetCompanyTraders(request.CompanyId);
                var opportunities = new List<SellOpportunityHeader>();

                if (request.GetOrders)
                {
                    opportunities = await _opportunityRepostitory.GetAllOrdersAsync(request.CompanyId,new List<string>
                    {
                        request.Filters.LeadName,
                        request.Filters.Trader
                    },request.Filters.DateFrom,request.Filters.DateTo);
                }
                else
                {
                    opportunities = await _opportunityRepostitory.GetAllOpportunitiesAsync(request.CompanyId, new List<string>
                    {
                        request.Filters.Status,
                        request.Filters.LeadName,
                        request.Filters.Trader
                    }, request.Filters.DateFrom, request.Filters.DateTo);
                }

                var response = new SellOpportunityListDto
                {
                    SellOpportunities = new List<SellOpportunityForListDto>(),
                    TraderList = new List<string>()
                };

                foreach(var trader in traders)
                {
                    response.TraderList.Add(trader.FirstName + " " + trader.LastName);
                }

                foreach(var oppo in opportunities)
                {
                    var oppoDto = new SellOpportunityForListDto
                    {
                        Id = oppo.Id,
                        Lead = oppo.Lead.Name,
                        Positions = new List<SellOpportunityPositionForListDto>(),
                        Status = oppo.Status.Name,
                        SumGrossValue = oppo.SumGrossValue,
                        SumMarkupValue = oppo.SumMarkupValue,
                        SumNetValue = oppo.SumNetValue,
                        SumVatValue = oppo.SumVatValue,
                        Trader = oppo.Trader.FirstName + " " + oppo.Trader.LastName,
                        CreateDate = oppo.CreateDate
                    };

                    foreach(var oppoPos in oppo.Positions)
                    {
                        oppoDto.Positions.Add(new SellOpportunityPositionForListDto
                        {
                            GrossValue = oppoPos.GrossValue,
                            Id = oppoPos.Id,
                            Markup = oppoPos.Markup,
                            NetValue = oppoPos.NetValue,
                            Product = oppoPos.Product.Name,
                            Quantity = oppoPos.Quantity,
                            VatValue = oppoPos.VatValue
                        });
                    }

                    response.SellOpportunities.Add(oppoDto);
                }

                return new JsonResult(new ApiResponse<SellOpportunityListDto>
                {
                    Code = 200,
                    Data = response,
                    ErrorMessage = ""
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 500,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}
