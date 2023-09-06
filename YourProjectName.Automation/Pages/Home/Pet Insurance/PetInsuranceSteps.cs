using Common.Automation.Common;
using Common.Automation.Common.Helpers.DataManager;
using TechTalk.SpecFlow;
using YourProjectName.Automation.Pages.Home.Pet_Insurance.TestData;

namespace YourProjectName.Automation.Pages.Home.Pet_Insurance
{
    [Binding]
    public sealed class PetInsuranceSteps : StepBase
    {
        private readonly PetInsuranceLocators _petInsuranceLocators;
        private readonly TestDataManager<PetsDataModel> _petsDataManager;
        public PetInsuranceSteps(PetInsuranceLocators petInsuranceLocators, TestDataManager<PetsDataModel> petsDataManager)
        {
            _petInsuranceLocators = petInsuranceLocators;
            _petsDataManager = petsDataManager;
        }

        [Given(@"I have selected pet '(.*)'")]
        public void SelectPet(string petName)
        {
            Radio.ClickAndWait(_petInsuranceLocators.SelectPetRadio(petName));
        }

        [Given(@"I have selected breed '(.*)'")]
        public void SelectBreed(string breed)
        {
            var value = Button.ByChildContainsTxt(breed);
            Dropdown.Click(_petInsuranceLocators.BreedInput);
            Input.ManualType(_petInsuranceLocators.BreedInput, breed);
            Button.Click(value);
        }

        [Given(@"I have selected date of birth day '(.*)' of current month")]
        public void SelectDateOfBirth(string day)
        {
            var dayToSelect = _petInsuranceLocators.DatePickerDay(day);
            DatePicker.SelectCurrentMonthDay(_petInsuranceLocators.DateOfBirthDatePickerIcon, dayToSelect);
        }

        [When(@"I press \[Calculate Price] button")]
        public void PressCalculatePriceButton()
        {
            Button.Click(_petInsuranceLocators.CalculatePriceButton);
        }

        [Then(@"Error '(.*)' for personal code field should be displayed")]
        public void ErrorForPersonCode(string errorTxt)
        {
            TextElement.TextShouldEqual(_petInsuranceLocators.PersonalCodeErrorTxt, errorTxt);
        }

        [Then(@"Error '(.*)' for phone number field should be displayed")]
        public void ErrorPhoneNumber(string errorTxt)
        {
            TextElement.TextShouldEqual(_petInsuranceLocators.PhoneCodeErrorTxt, errorTxt);
        }

        [Given(@"I have filled information about pet using data with name '(.*)'")]
        public void EnterPetInfo(string dataName)
        {
            var data = _petsDataManager.GetData(dataName);

            SelectBreed(data.Breed);
            SelectDateOfBirth(data.DateOfBirth);
            Input.Type(_petInsuranceLocators.PersNrInput, data.PersNr);
            Input.Type(_petInsuranceLocators.EmailInput, data.Email);
            Input.Type(_petInsuranceLocators.PhoneNumberInput, data.Phone);
        }


    }
}
