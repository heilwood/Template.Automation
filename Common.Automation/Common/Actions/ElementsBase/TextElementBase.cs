using System.Text.RegularExpressions;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using FluentAssertions;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions.ElementsBase
{
    public class TextElementBase : ElementBase
    {
        public TextElementBase(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public string GetText(IWebElement elem)
        {
            return elem.Text;
        }

        public string GetText(By by)
        {
            var element = GetElement(by);
            WaitUntilVisible(element);
            return GetText(element);
        }

        public string GetAttributeText(By by, string attribute)
        {
            return GetElement(by).GetAttribute(attribute);
        }

        public string GetTextByAttribute(By by)
        {
            return GetElement(by).GetAttribute("innerHTML");
        }

        public int TextToInt(string text)
        {
            return int.Parse(Regex.Match(text, @"^\d+").Value);
        }

        public int GetTextAsInteger(By by)
        {
            var text = GetTextWithNoSpaces(by);
            return TextToInt(text);
        }

        public string GetTextWithNoSpaces(By by)
        {
            return RemovTextSpaces(GetTextByAttribute(by));
        }

        public string RemovTextSpaces(string text)
        {
            return text.Replace(" ", "").Replace(" ", "");
        }

        public void TextShouldEqual(By by, string text)
        {
            var actual = GetText(by);
            actual.Should().Be(text);
        }

        public void TextShouldContain(By by, string text)
        {
            var actual = GetText(by);
            actual.Should().Contain(text);
        }

        public void TextShouldContainIgnoreCase(By by, string text)
        {
            var actual = GetText(by);
            actual.Should().ContainEquivalentOf(text);
        }

        public void TextShouldNotBeEmpty(By by)
        {
            var actual = GetText(by);
            actual.Should().NotBeEmpty();
        }
    }
}
