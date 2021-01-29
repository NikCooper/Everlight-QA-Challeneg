namespace Everlight.Core
{
    public class ComputerData
    {
        public ComputerData()
        {
            ComputerNameValue = DataLoad.GetData("ComputerName");
            IntroducedDate = DataLoad.GetData("IntroducedDate");
            DiscontinuedDate = DataLoad.GetData("DiscontinuedDate");
            CompanyValue = DataLoad.GetData("Company");
            SearchBar = DataLoad.GetData("Search");
            SearchValue = DataLoad.GetData("SearchValue");
        }

        //Store Data
        public string ComputerNameValue { get; set; }
        public string IntroducedDate { get; set; }
        public string DiscontinuedDate { get; set; }
        public string CompanyValue { get; set; }
        public string SearchBar { get; set; }
        public string SearchValue { get; set; }
    }
}