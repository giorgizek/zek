using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Zek.Extensions;
using Zek.Extensions.Security.Claims;

namespace Zek.Web
{
    public class BaseController : Controller
    {
        private int? _userId;
        protected virtual int UserId
        {
            get
            {
                if (_userId == null)
                    _userId = User.GetUserId().ToInt32();
                return _userId.Value;
            }
            set => _userId = value;
        }

        private string[] _roles;
        public string[] Roles
        {
            get => _roles ?? (_roles = User.GetRoles().ToArray());
            set => _roles = value;
        }
    }
}
