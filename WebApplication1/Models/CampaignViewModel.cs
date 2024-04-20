namespace WebApplication1.Models
{
    public class CampaignViewModel
    {
        public string CampaignId { get; set; }
        public DateTime CampaignBeginDate { get; set; }
        public DateTime CampaignEndDate { get; set; }
        public List<string> CampaignCodes { get; set; }
    }
}
