using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services
{
    public class CodeService : ICodeService
    {
        private readonly string allowedCharacters = "ACDEFGHKLMNPRTXYZ234579";
        private static HashSet<string> uniqueCodes = new HashSet<string>();
        public static List<CampaignViewModel> Campaigns { get; } = new List<CampaignViewModel>();
        public static HashSet<string> UsedCodes { get; } = new HashSet<string>();
        public string CheckCampaign(string campaignId, string code)
        {
            var campaign = Campaigns.Find(c => c.CampaignId == campaignId);
            if (campaign == null)
            {
                return "Geçersiz kampanya ID.";
            }
            if (campaign.CampaignBeginDate > DateTime.Now || campaign.CampaignEndDate < DateTime.Now)
            {
                return "Geçersiz tarih aralığı.";
            }

            if (!campaign.CampaignCodes.Contains(code) || !ValidateCode(code))
            {
                return "Geçersiz kampanya kodu.";
            }

            if (!UsedCodes.Add(code))
            {
                return "Bu kod zaten kullanılmış.";
            }

            return "Tebrikler kampanyaya katıldınız.";
        }
        public bool ValidateCode(string code)
        {
            if (code.Length != 8)
            {
                return false;
            }
            foreach (char c in code)
            {
                if (!allowedCharacters.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
        public List<CampaignViewModel> GenerateCampaign()
        {
            if(Campaigns.Count > 0)
            {
                return Campaigns;
            }
            for (int i = 0; i < 3; i++)
            {
                var campaignCodes = new List<string>();
                var campaign = new CampaignViewModel
                {
                    CampaignId = Guid.NewGuid().ToString(),
                    CampaignBeginDate = DateTime.Now.AddDays(-10),
                    CampaignEndDate = DateTime.Now.AddDays(10)
                };
                for (int j = 0; j < 10; j++)
                {
                    var code = GenerateCode();
                    campaignCodes.Add(code);
                }
                var isExist = Campaigns.Any(c => c.CampaignId == campaign.CampaignId);
                if (isExist)
                {
                    continue;
                }
                campaign.CampaignCodes = campaignCodes;
                Campaigns.Add(campaign);
            }
            return Campaigns;
        }
        public string GenerateCode()
        {
            byte[] randomBytes = new byte[8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            string code = "";
            bool codeIsUnique = false;
            while (!codeIsUnique)
            {
                code = "";
                for (int i = 0; i < randomBytes.Length; i++)
                {
                    int index = randomBytes[i] % allowedCharacters.Length;
                    code += allowedCharacters[index];
                }

                if (!uniqueCodes.Contains(code))
                {
                    uniqueCodes.Add(code);
                    codeIsUnique = true;
                }
            }

            return code;
        }
    }
}
