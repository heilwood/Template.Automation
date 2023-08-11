using OpenQA.Selenium;

namespace YourProjectName.Automation.Pages.Home.Pet_Insurance
{
    public class PetInsuranceLocators
    {
        public By SelectPetRadio(string petName) => By.XPath($"//div[@role='radiogroup']//span[text()='{petName}']");
        public By BreedInput = By.CssSelector("[name='breed']");
        public By DateOfBirthDatePickerIcon = By.CssSelector("[name='dateOfBirth'] + div  svg");
        public By CalculatePriceButton = By.XPath("//button[span[text()='Aprēķināt cenu']]");

        public By DatePickerDay(string day) =>
            By.XPath(
                $"//*[contains(@class, 'MuiPickersBasePicker-container')]//button[@tabindex='0']//p[text()='{day}']");

        public By PersonalCodeErrorTxt =
            By.XPath(
                "//input[@name='clientCode']/ancestor::*[contains(@class, 'MuiFormControl-fullWidth')]/following-sibling::label");
    }
}
