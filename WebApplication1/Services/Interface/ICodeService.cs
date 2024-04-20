using WebApplication1.Models;

namespace WebApplication1.Services.Interface
{
    public interface ICodeService
    {
        string CheckCampaign(string campaignId, string code);
        string GenerateCode();
        bool ValidateCode(string code);
        List<CampaignViewModel> GenerateCampaign();
    }
}
