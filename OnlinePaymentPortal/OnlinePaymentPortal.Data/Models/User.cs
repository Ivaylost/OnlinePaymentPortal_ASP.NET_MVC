using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class User 
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public string Salt { get; set; }

        public ICollection<UsersAccounts> UsersAccounts { get; set; }

    }
}
