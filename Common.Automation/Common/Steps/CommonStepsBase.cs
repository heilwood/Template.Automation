using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Common.Automation.Common.Steps
{
    [Binding]
    public sealed class CommonStepsBase : StepBase
    {
        public CommonStepsBase(IObjectContainer container) : base(container)
        {
        }

        [When(@"I Refresh the page")]
        [Given(@"I have Refreshed the page")]
        public void RefreshPage()
        {
            Navigation.Refresh();
            Window.WaitForPageToLoad();
        }

        [Then(@"Current url should contains '(.*)'")]
        public void CurrentUrlShouldContains(string text)
        {
            var url = Window.GetUrl();
            url.Should().Contain(text);
        }

       
        [When(@"I click \[Back] button in browser")]
        public void NavigateBack()
        {
            Navigation.NavigateBack();
        }
    }
}
