using FluentAssertions;
using TechTalk.SpecFlow;

namespace Common.Automation.Common.Steps
{
    [Binding]
    public sealed class CommonStepsBase : StepBase
    {

        [Given(@"I have opened application url")]
        public void OpenMainAppUrl()
        {
            Navigation.OpenPage(ConfigManager.MainUrl);
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
