using Common.Automation.Common.Actions;

namespace Common.Automation.Common
{
    public class StepBase
    {
        private DriverHolder _driverHolder => AutofacConfig.Resolve<DriverHolder>();

        public Button Button => _driverHolder.CreateObject<Button>();
        public Checkbox Checkbox => _driverHolder.CreateObject<Checkbox>();
        public DatePicker DatePicker => _driverHolder.CreateObject<DatePicker>();
        public Div Div => _driverHolder.CreateObject<Div>();
        public Input Input => _driverHolder.CreateObject<Input>();
        public Select Select => _driverHolder.CreateObject<Select>();
        public TextElement TextElement => _driverHolder.CreateObject<TextElement>();
        public Href Href => _driverHolder.CreateObject<Href>();
        public Radio Radio => _driverHolder.CreateObject<Radio>();
        public Window Window => _driverHolder.CreateObject<Window>();
        public Navigation Navigation => _driverHolder.CreateObject<Navigation>();
        public Tab Tab => _driverHolder.CreateObject<Tab>();
    }
}
