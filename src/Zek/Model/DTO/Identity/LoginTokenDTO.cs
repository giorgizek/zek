using System;
using System.Collections.Generic;
using Zek.Utils;

namespace Zek.Model.DTO.Identity
{
    public class LoginTokenDTO : LoginTokenDTO<int> { }
    public class LoginTokenDTO<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public string Token { get; set; }
        public DateTime? Expired { get; set; }
        public DateTime? RefreshTokenTime { get; set; }
        //public KeyPair<int, int>[] Permissions { get; set; }
        public Dictionary<int, int> Permissions { get; set; }
        //public DateTime CurrentDateTime { get; set; }
    }
}