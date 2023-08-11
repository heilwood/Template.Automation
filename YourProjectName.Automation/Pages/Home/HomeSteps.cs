using Common.Automation.Common;
using TechTalk.SpecFlow;
using YourProjectName.Automation.Home;

namespace YourProjectName.Automation.Pages.Home
{
    [Binding]
    public sealed class HomeSteps : StepBase
    {
        private readonly HomeLocators _homeLocators;

        public HomeSteps(HomeLocators homeLocators)
        {
            _homeLocators = homeLocators;
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
