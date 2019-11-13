namespace Zek.Model.DTO
{
    public class RowActionDTO : RowActionDTO<int?>
    {
        public RowActionDTO(
            int? id = null,
            string controller = null,
            string detailsAction = "Details",
            string editAction = "Edit",
            string approveAction = "Approve",
            string deleteAction = "Delete") : base(id, controller, detailsAction, editAction, approveAction, deleteAction)
        {
        }
    }
    public class RowActionDTO<TId> : BaseActionDTO<TId>
    {
        public RowActionDTO(
            TId id = default(TId), 
            string controller = null, 
            string detailsAction = "Details",
            string editAction = "Edit",
            string approveAction = "Approve",
            string deleteAction = "Delete"
        )
        {
            Id = id;
            Controller = controller;
            DetailsAction = detailsAction;
            EditAction = editAction;
            ApproveAction = approveAction;
            DeleteAction = deleteAction;

            ShowDetail = true;
            ShowEdit = true;
            ShowApprove = true;
            ShowDelete = true;
        }

        /// <summary>
        /// Details action name
        /// </summary>
        public string DetailsAction { get; set; }

        public bool ShowDetail
        {
            get;
            set;
        }

        /// <summary>
        /// Edit action name
        /// </summary>
        public string EditAction { get; set; }
        public bool ShowEdit { get; set; }

        /// <summary>
        /// Edit action name
        /// </summary>
        public string ApproveAction { get; set; }

        public bool ShowApprove { get; set; }

        /// <summary>
        /// Delete action name
        /// </summary>
        public string DeleteAction { get; set; }

        public bool ShowDelete { get; set; }
    }
}