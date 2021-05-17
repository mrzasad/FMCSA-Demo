using System;
using System.Collections.Generic;
using System.Text;


using Application.POCOs;
using MediatR;
using Core.Entities;
using Core.Context;
using System.Threading.Tasks;
using System.Threading;
using Core.EnumsAndConsts;
using System.Linq;
using AutoMapper;
using Application.Infrastructure.AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.VehicleInfo
{

    public class GetVehicleInfoListQuery : IRequest<GetVehicleInfoListQueryDTO>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
        public GetVehicleInfoListQueryFilters Filters { get; set; }
        public GetVehicleInfoListQuery(int _start, int _Length)
        {
            Start = _start;
            Length = _Length;
        }
        public GetVehicleInfoListQuery(int _start, int _Length,string _search)
        {
            Start = _start;
            Length = _Length;
            Search = _search;
        }
        public GetVehicleInfoListQuery(int _start, int _Length, string _search, GetVehicleInfoListQueryFilters _filters)
        {
            Start = _start;
            Length = _Length;
            Search = _search;
            Filters = _filters;
        }
    }
    public class GetVehicleInfoListQueryFilters
    {
        public string ZipCode { get; set; }
        public string VehicleType { get; set; }
        public string States { get; set; }
    }
    public class GetVehicleInfoListQueryDTO
    {
        public RequestResponse RequestResponse = new RequestResponse();

        public List<GetVehicleInfoListLookup> VehicleInfo = new List<GetVehicleInfoListLookup>();

        public int Total { get; set; }

    }
    public class GetVehicleInfoListLookup : IHaveCustomMapping
    {
        public long VehicleInfoId { get; set; }
        public int Type { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string DOTNumber { get; set; }
        public string SafetyLink { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Core.Entities.VehicleInfo, GetVehicleInfoListLookup>();
        }
    }
    public class GetVehicleInfoListQueryHandler : IRequestHandler<GetVehicleInfoListQuery, GetVehicleInfoListQueryDTO>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetVehicleInfoListQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetVehicleInfoListQueryDTO> Handle(GetVehicleInfoListQuery request, CancellationToken cancellationToken)
        {
            GetVehicleInfoListQueryDTO model = new GetVehicleInfoListQueryDTO();
            try
            {

                var data = _context.VehicleInfos.AsQueryable();
                if (!string.IsNullOrEmpty(request.Search))
                {
                    data = data.Where(x => x.CompanyName.ToLower().Contains(request.Search.ToLower())|| x.DOTNumber.ToLower().Contains(request.Search.ToLower()));
                }
                if (request.Filters != null)
                {
                    if (!string.IsNullOrEmpty(request.Filters.ZipCode))
                    {
                        data = data.Where(x => x.ZipCode.ToLower().Contains(request.Filters.ZipCode.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(request.Filters.States) && request.Filters.States != "0")
                    {
                        string[] states = request.Filters.States.Split(',');
                        data = data.Where(x => states.Contains(x.State));
                    }
                    if (!string.IsNullOrEmpty(request.Filters.VehicleType) && request.Filters.VehicleType != "0")
                    {
                        string[] type = request.Filters.VehicleType.Split(',');
                        List<string> types = new List<string>();
                        foreach (var item in type)
                        {
                            types.Add(VehicleType.GetId(item).ToString());
                        }
                        data = data.Where(x => types.Contains(x.Type.ToString()));
                    }
                }
                
                model.Total = data.Count();

                if (request.Start == 0 && request.Length == 0)
                {
                    model.VehicleInfo = await data.ProjectTo<GetVehicleInfoListLookup>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                }
                else
                {
                    model.VehicleInfo = await data.Skip(request.Start).Take(request.Length).ProjectTo<GetVehicleInfoListLookup>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                }

                model.RequestResponse.Result = true;
                model.RequestResponse.Status = RequestResponseStatus.Success;
                model.RequestResponse.Msg = "";

            }
            catch (Exception ex)
            {
                model.RequestResponse.Result = false;
                model.RequestResponse.Status = RequestResponseStatus.Exception;
                model.RequestResponse.Msg = ex.Message;
            }
            return model;
        }
    }


}
