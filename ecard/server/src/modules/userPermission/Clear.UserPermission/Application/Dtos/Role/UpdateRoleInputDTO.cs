using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Role))]
    public class UpdateRoleInputDTO : RoleDTO
    {
        public string Id { get; set; }
    }
}
