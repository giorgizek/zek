namespace Zek.Model.DTO
{
    /// <summary>
    /// Edit model base class
    /// </summary>
    public class EditBaseDTO : EditBaseDTO<int?>
    {
        
    }

    /// <summary>
    /// Edit model base class
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class EditBaseDTO<TId>
    {
        /// <summary>
        /// ID of model
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Set true if entity is read only
        /// </summary>
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Set is deleted if entity is deleted
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
