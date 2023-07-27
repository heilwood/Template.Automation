using Common.Automation.Common.Actions;

namespace Common.Automation.Common
{
    public class StepBase
    {
        public Button Button => DriverHolder.CreateObject<Button>();
        public Checkbox Checkbox => DriverHolder.CreateObject<Checkbox>();
        public DatePicker DatePicker => DriverHolder.CreateObject<DatePicker>();
        public Div Div => DriverHolder.CreateObject<Div>();
        public Input Input => DriverHolder.CreateObject<Input>();
        public Select Select => DriverHolder.CreateObject<Select>();
        public TextElement TextElement => DriverHolder.CreateObject<TextElement>();
        public Href Href => DriverHolder.CreateObject<Href>();
        public Radio Radio => DriverHolder.CreateObject<Radio>();
        public Window Window => DriverHolder.CreateObject<Window>();
        public Navigation Navigation => DriverHolder.CreateObject<Navigation>();
        public Tab Tab => DriverHolder.CreateObject<Tab>();
    }
}
