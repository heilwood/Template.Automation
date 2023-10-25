using OpenQA.Selenium;

namespace MyProject.Automation.Functional.Pages.Home
{
    public class HomeLocators
    {
        public By CategoriesDropdownButton(string catName) =>
            By.XPath($"//div[contains(@class, 'cta-menu')]//button[text() = '{catName}']");

        public By CategoryDropdownValuesList(string catName) => By.XPath(
            $"//div[contains(@class, 'cta-menu')]//button[text() = '{catName}']/ancestor::div[contains(@class, 'cta-menu')]/div[contains(@class, 'cta-submenu-wrapper')]");
    }
}
