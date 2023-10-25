using Common.Automation.Common.Helpers.DataManager;

namespace MyProject.Automation.Functional.Pages.Home.Pet_Insurance.TestData
{
    public class PetsDataModel : DataModelBase<PetsDataModel>
    {
        //Set in properties of file PetsDataATest.json Copy if never or Copy always option
        protected override string JsonFilePath => @"Pages\Home\Pet Insurance\TestData\PetsDataATest.json";
        public string Breed { get; set; }
        public string DateOfBirth { get; set; }
        public string PersNr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override PetsDataModel SelectData(string name)
        {
            var data = base.SelectData(name);
            return data;
        }
    }
}
