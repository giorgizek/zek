using System.ComponentModel.DataAnnotations;

namespace Zek.Model.WS
{
    public class Request<T> : Request
    {
        public Request(string requestId = null) : base(requestId)
        {
            
        }

        [Required]
        public T Value { get; set; }
    }

    public class Request
    {
        public Request(string requestId = null)
        {
            RequestId = requestId;
        }

        /// <summary>
        /// ყველა რექვესთს უნდა ჰქონდეს უნიკალური იდენთიფიკატორი (მაგ: Guid.NewGuid() ან int 1,2,3...). გამოიყენება დადარებისთვის მერჩანტმა რა გამოგვიგზავნა და რა დაუბრუნეთ.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RequestId { get; set; }
    }
}
