namespace Zek.Office
{
    public class ItemBody
    {
        /// <summary>
        /// Gets or sets content.
        /// The content of the item.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets contentType.
        /// The type of the content. Possible values are text and html.
        /// </summary>
        public BodyType? ContentType { get; set; }
    }
}