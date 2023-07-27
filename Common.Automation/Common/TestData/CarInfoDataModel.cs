using Common.Automation.Common.Helpers.DataManager;

namespace Common.Automation.Common.TestData
{
    public class CarInfoDataModel : DataModelBase<CarInfoDataModel>
    {
        protected override string JsonFilePath => @"Common\TestData\CarInfoATest.json";
        public string RegNr { get; set; }
        public string PersNr { get; set; }
        public string Description { get; set; }
        public string CarModel { get; set; }
        public string CDWEndDate { get; set; }

        public override CarInfoDataModel SelectData(string name)
        {
            var data = base.SelectData(name);
            return data;
        }
    }
}
