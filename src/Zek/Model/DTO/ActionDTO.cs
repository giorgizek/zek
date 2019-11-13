namespace Zek.Model.DTO
{
    public class ActionDTO<TId> : BaseActionDTO<TId>
    {
        public ActionDTO(TId id = default(TId), string controller = null, string action = "Delete")
        {
            Id = id;
            Controller = controller;
            Action = action;
        }
        
        /// <summary>
        /// Action name
        /// </summary>
        public string Action { get; set; }


        public string OnAjaxSuccess { get; set; }
    }

    public class ActionDTO : ActionDTO<int?>
    {
        public ActionDTO(int? id = null, string controller = null, string action = "Delete") : base(id, controller, action)
        {
        }

    }
}