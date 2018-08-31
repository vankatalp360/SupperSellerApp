namespace SuperSeller.Common.Admin.ViewModels
{
    public class AdsConciseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Condition { get; set; }
        public int Viewing { get; set; }
        public bool PromoEnable { get; set; }
    }
}