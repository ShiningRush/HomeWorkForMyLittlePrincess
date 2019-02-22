using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    public class GetRoleUserOutputDTO
    {
        public string UserId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }

        public string DepartmentName { get; set; }
    }
}
