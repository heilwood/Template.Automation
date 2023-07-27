using Common.Automation.Common;
using TechTalk.SpecFlow;

namespace YourProjectName.Automation.Home
{
    [Binding]
    public sealed class HomeSteps : StepBase
    {
        private readonly HomeLocators _homeLocators;

        public HomeSteps()
        {
            _homeLocators = new HomeLocators();
        }
    }
}
