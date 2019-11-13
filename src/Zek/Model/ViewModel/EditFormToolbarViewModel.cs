namespace Zek.Model.ViewModel
{
    public class DefaultEditFormToolbarViewModel : EditFormToolbarViewModel
    {
        public DefaultEditFormToolbarViewModel()
        {
            Save = true;
        }
    }

    public class EditFormToolbarViewModel
    {
        public bool Save { get; set; }
        public bool Print { get; set; }
    }
}
