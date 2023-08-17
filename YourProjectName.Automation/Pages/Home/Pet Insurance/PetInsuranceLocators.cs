using OpenQA.Selenium;

namespace YourProjectName.Automation.Pages.Home.Pet_Insurance
{
    public class PetInsuranceLocators
    {

        //Always prefer to select elements By.Id or By.CssSelector
        public By SelectPetRadio(string petName) => By.XPath($"//div[@role='radiogroup']//span[text()='{petName}']");
        public By DateOfBirthDatePickerIcon = By.CssSelector("[name='dateOfBirth'] + div  svg");
        public By CalculatePriceButton = By.XPath("//button[span[text()='Aprēķināt cenu']]");
        public By BreedInput = By.CssSelector("[name='breed']");
        public By PersNrInput = By.CssSelector("[name='clientCode']");
        public By EmailInput = By.CssSelector("[name='email']");
        public By PhoneNumberInput = By.CssSelector("[name='phoneNumber']");

        public By DatePickerDay(string day) =>
            By.XPath(
                $"//*[contains(@class, 'MuiPickersBasePicker-container')]//button[@tabindex='0']//p[text()='{day}']");

        public By PersonalCodeErrorTxt =
            By.XPath(
                "//input[@name='clientCode']/ancestor::*[contains(@class, 'MuiFormControl-fullWidth')]/following-sibling::label");

        public By PhoneCodeErrorTxt = By.CssSelector("[class*='phone-input'] + label");
    }
}
