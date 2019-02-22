using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Organize))]
    public class UpdateOrganizeInputDTO : OrganizeDTO
    {
        public string Id { get; set; }
    }
}
