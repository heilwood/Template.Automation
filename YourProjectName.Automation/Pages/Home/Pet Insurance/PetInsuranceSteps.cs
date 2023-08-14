using Common.Automation.Common;
using TechTalk.SpecFlow;

namespace YourProjectName.Automation.Pages.Home.Pet_Insurance
{
    [Binding]
    public sealed class PetInsuranceSteps : StepBase
    {
        private readonly PetInsuranceLocators _petInsuranceLocators;
        public PetInsuranceSteps(PetInsuranceLocators petInsuranceLocators)
        {
            _petInsuranceLocators = petInsuranceLocators;
        }

        [Given(@"I have selected pet '(.*)'")]
        public void SelectPet(string petName)
        {
            Radio.Click(_petInsuranceLocators.SelectPetRadio(petName));
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
        public void RequiredErrorForPersonCode(string errorTxt)
        {
            TextElement.TextShouldEqual(_petInsuranceLocators.PersonalCodeErrorTxt, errorTxt);
        }

    }
}
