using Common.Automation.Common.Actions;

namespace Common.Automation.Common
{
    public class StepBase
    {
        public Button Button => AutofacConfig.Resolve<Button>();
        public Checkbox Checkbox => AutofacConfig.Resolve<Checkbox>();
        public DatePicker DatePicker => AutofacConfig.Resolve<DatePicker>();
        public Div Div => AutofacConfig.Resolve<Div>();
        public Input Input => AutofacConfig.Resolve<Input>();
        public Select Select => AutofacConfig.Resolve<Select>();
        public TextElement TextElement => AutofacConfig.Resolve<TextElement>();
        public Href Href => AutofacConfig.Resolve<Href>();
        public Radio Radio => AutofacConfig.Resolve<Radio>();
        public Window Window => AutofacConfig.Resolve<Window>();
        public Navigation Navigation => AutofacConfig.Resolve<Navigation>();
        public Tab Tab => AutofacConfig.Resolve<Tab>();
    }
}