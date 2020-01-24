using System;

namespace Zek.Model.DTO.Identity
{
    public class LoginTokenDTO : LoginTokenDTO<int> { }
    public class LoginTokenDTO<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        //public DateTime CurrentDateTime { get; set; }
    }
}
