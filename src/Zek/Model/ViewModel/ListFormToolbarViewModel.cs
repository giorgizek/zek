namespace Zek.Model.ViewModel
{
    public class ListFormToolbarViewModel
    {
        //public bool PersonalNumberTextBox { get; set; };
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Choose { get; set; }
        public bool Approve { get; set; }
        public bool Disapprove { get; set; }
        public bool Sum { get; set; }
        public bool Print { get; set; }
        public bool Export { get; set; }
        public bool Refresh { get; set; }
        public bool Filter { get; set; }

        //public bool Annulate { get { get; set; }; set { get; set; }; }
    }

    public class DefaultListFormToolbarViewModel : ListFormToolbarViewModel
    {
        public DefaultListFormToolbarViewModel()
        {
            Create = true;
            Refresh = true;
            Filter = true;
            Delete = true;
        }
    }
}
