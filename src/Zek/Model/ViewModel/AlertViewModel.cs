namespace Zek.Model.ViewModel
{
    public enum AlertType
    {
        Primary = 1,
        Secondary,
        Success,
        Danger = 4,
        Warning,
        Info,
        Light,
        Dark
    }
    public class AlertViewModel
    {
        public AlertViewModel()
        {
            AlertType = AlertType.Info;
        }
        private AlertType _alertType;
        public AlertType AlertType
        {
            get { return _alertType; }
            set
            {
                if (value != _alertType)
                {
                    _alertType = value;
                    Css = $"alert-{value.ToString().ToLowerInvariant()}";
                }
            }
        }

        public string Css { get; private set; }

        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public bool Dismissible { get; set; }
    }
}
