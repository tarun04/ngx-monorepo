using System;

namespace Ngx.Monorepo.Framework.Identity.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string GivenName => $"{FirstName} {Lastname}";
        public string Email { get; set; }
    }
}
