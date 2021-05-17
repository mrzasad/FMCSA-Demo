using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.CQRS.VehicleInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Authorize(Policy = "CustomAuthorization")]
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<JsonResult> LoadDT(DTParameterModel param,GetVehicleInfoListQueryFilters filters)
        {
            var data = await Mediator.Send(new GetVehicleInfoListQuery(param.Start, param.Length, param.Search.Value,filters));

            return Json(new
            {
                draw = param.Draw,
                recordsFiltered = data.VehicleInfo.Count,
                recordsTotal = data.Total,
                data = data.VehicleInfo
            });
        }
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}