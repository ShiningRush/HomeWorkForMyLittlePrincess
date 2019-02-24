using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.Entities
{
    public class Robot : Entity<int>
    {
        [MaxLength(32, ErrorMessage = "名称最大长度64")]
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary> 
        [Column("DepartmentId")]
        public Guid DepartmentId { get; set; }
        [Column("IsValid")]
        public bool IsValid { get; set; }
        [Column("Description")]
        [MaxLength(256, ErrorMessage = "名称最大长度256")]
        public string Description { get; set; }
    }
}
