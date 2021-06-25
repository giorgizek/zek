namespace Zek.Model.DTO
{
    /// <summary>
    /// Edit base (Id field is nullable int)
    /// </summary>
    public class EditBaseDTO : EditBaseDTO<int?>
    {
        
    }

    /// <summary>
    /// Edit base (Id field is generic)
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
