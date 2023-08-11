using Common.Automation.Common.Locators;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Common.Automation.Common.Steps
{
    [Binding]
    public sealed class CommonStepsBase : StepBase
    {
        private readonly CommonLocatorsBase _commonLocatorsBase;
        public CommonStepsBase(CommonLocatorsBase commonLocatorsBase)
        {
            _commonLocatorsBase = commonLocatorsBase;
        }

        [Given(@"I have opened IF insurance home page")]
        public void OpenIfHomeUrl()
        {
            Navigation.OpenPageAsync(ConfigManager.MainUrl).GetAwaiter().GetResult();
            Button.Click(_commonLocatorsBase.CookiesAcceptButton);
        }


        [When(@"I Refresh the page")]
        [Given(@"I have Refreshed the page")]
        public void RefreshPage()
        {
            Navigation.Refresh();
            Window.WaitForPageToLoad();
            Window.WaitUntilAllRequestsFinished();
        }

        [Then(@"Current url should contains '(.*)'")]
        public void CurrentUrlShouldContains(string text)
        {
            var url = Href.GetCurrentUrl();
            url.Should().Contain(text);
        }

       
        [When(@"I click \[Back] button in browser")]
        public void NavigateBack()
        {
            Navigation.NavigateBack();
        }
    }
}
