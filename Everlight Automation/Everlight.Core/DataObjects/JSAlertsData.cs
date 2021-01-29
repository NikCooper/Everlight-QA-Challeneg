namespace Everlight.Core
{
    public class JSAlertsData
    {
        public JSAlertsData()
        {
            ResultOne = DataLoad.GetData("Result 1");
            ResultTwo = DataLoad.GetData("Result 2");
            ResultThree = DataLoad.GetData("Result 3");
            ResultFour = DataLoad.GetData("Result 4");
            ResultFive = DataLoad.GetData("Result 5");
        }

        //Store Data
        public string ResultOne { get; set; }
        public string ResultTwo { get; set; }
        public string ResultThree { get; set; }
        public string ResultFour { get; set; }
        public string ResultFive { get; set; }
    }
}