using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.Services.Interface;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : Controller
    {
        public ICodeService _codeService;
        public CampaignController(ICodeService codeService)
        {
            _codeService = codeService;
        }
        [HttpGet]
        [Route("GetCampaigns")]
        public IActionResult GetCampaigns()
        {
            var campaigns = _codeService.GenerateCampaign();
            if (campaigns.Count == 0)
            {
                return NotFound("Kampanya bulunamadı");
            }
            return Ok(campaigns);
        }

        [HttpGet]
        [Route("ApplyCampaign")]
        public IActionResult ApplyCampaign(string campaignId, string code)
        {
            var result = _codeService.CheckCampaign(campaignId, code);
            return Ok(result);
        }       
    }
}