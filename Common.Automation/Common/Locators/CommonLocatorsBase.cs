using OpenQA.Selenium;

namespace Common.Automation.Common.Locators
{
    public class CommonLocatorsBase
    {
        public By AcceptCookiesButton = By.CssSelector("button#onetrust-accept-btn-handler");
        public By ByChildContainsTxt(string text) => By.XPath($".//*[contains(text(),'{text}')]");
    }
}
