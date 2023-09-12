using BoDi;
using Common.Automation.Common.ElementActions;

namespace Common.Automation.Common
{
    public class StepBase
    {
        readonly IObjectContainer _container;
        public StepBase(IObjectContainer container)
        {
            _container = container;
        }

        public Button Button => _container.Resolve<Button>();
        public Checkbox Checkbox => _container.Resolve<Checkbox>();
        public DatePicker DatePicker => _container.Resolve<DatePicker>();
        public Div Div => _container.Resolve<Div>();
        public Input Input => _container.Resolve<Input>();
        public Dropdown Dropdown => _container.Resolve<Dropdown>();
        public TextElement TextElement => _container.Resolve<TextElement>();
        public A A => _container.Resolve<A>();
        public Radio Radio => _container.Resolve<Radio>();
        public Window Window => _container.Resolve<Window>();
        public Navigation Navigation => _container.Resolve<Navigation>();
        public Tab Tab => _container.Resolve<Tab>();
    }
}