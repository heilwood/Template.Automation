﻿using Common.Automation;
using Common.Automation.Common;
using Common.Automation.Common.Locators;
using TechTalk.SpecFlow;

namespace YourProjectName.Automation.Pages.Home
{
    [Binding]
    public sealed class HomeSteps : StepBase
    {
        private readonly HomeLocators _homeLocators;
        private readonly CommonLocatorsBase _commonLocatorsBase;

        public HomeSteps(HomeLocators homeLocators, CommonLocatorsBase commonLocatorsBase)
        {
            _homeLocators = homeLocators;
            _commonLocatorsBase = commonLocatorsBase;
        }

        [Given(@"I have opened IF insurance home page")]
        public void OpenIfHomeUrl()
        {
            Navigation.OpenPage(ConfigManager.MainUrl);
            Button.ClickAndWait(_commonLocatorsBase.AcceptCookiesButton);
        }

        [Given(@"I have selected '(.*)' from category '(.*)'")]
        public void SelectFromCategory(string value, string categoryName)
        {
            var dropdownButton = _homeLocators.CategoriesDropdownButton(categoryName);
            var dropdownList = _homeLocators.CategoryDropdownValuesList(categoryName);
            Dropdown.SelectByText(dropdownButton, dropdownList, value);
        }
    }
}
