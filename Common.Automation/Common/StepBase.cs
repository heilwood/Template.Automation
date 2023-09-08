using Common.Automation.Common.ElementActions;

namespace Common.Automation.Common
{
    //TODO: Is not good idea to use Service Locator pattern for real life apps, in our case it fits ok.
    public class StepBase
    {
        public Button Button { get; } = AutofacConfig.Resolve<Button>();
        public Checkbox Checkbox { get; } = AutofacConfig.Resolve<Checkbox>();
        public DatePicker DatePicker { get; } = AutofacConfig.Resolve<DatePicker>();
        public Div Div { get; } = AutofacConfig.Resolve<Div>();
        public Input Input { get; } = AutofacConfig.Resolve<Input>();
        public Dropdown Dropdown { get; } = AutofacConfig.Resolve<Dropdown>();
        public TextElement TextElement { get; } = AutofacConfig.Resolve<TextElement>();
        public A A { get; } = AutofacConfig.Resolve<A>();
        public Radio Radio { get; } = AutofacConfig.Resolve<Radio>();
        public Window Window { get; } = AutofacConfig.Resolve<Window>();
        public Navigation Navigation { get; } = AutofacConfig.Resolve<Navigation>();
        public Tab Tab { get; } = AutofacConfig.Resolve<Tab>();

    }
}