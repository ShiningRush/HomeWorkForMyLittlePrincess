using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Domain
{
    [Table("sys_app")]
    public class AuthorizationInfo : Entity
    {
        /// <summary>
        /// 应用ID
        /// AppID 授权应用唯一标识，由服务器运营人员授权后发布
        /// </summary>
        [MaxLength(50, ErrorMessage = "终端应用ID最大长度50")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "终端应用ID不能为空")]
        public string AppID { get; set; }

        /// <summary>
        /// 应用名
        /// </summary>
        [MaxLength(50, ErrorMessage = "终端应用名最大长度50")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "终端应用名不能为空")]
        public string AppName { get; set; }

        /// <summary>
        /// 应用授权密钥hash值
        /// 应用授权密钥的Hash值，加密信息关联机器硬件码信息
        /// </summary>
        [MaxLength(200, ErrorMessage = "应用授权密钥最大长度200")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "应用授权密钥不能为空")]
        public string AppSecretHash { get; set; }

        /// <summary>
        /// 授权密钥加密随机值
        /// </summary>
        [MaxLength(50, ErrorMessage = "授权密钥加密随机值最大长度50")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "授权密钥加密随机值不能为空")]
        public string AppSecretSalt { get; set; }

        /// <summary>
        /// 授权验证数据
        /// 包含线上系统分配的医院信息（K3系统的医院ID）、院内主备服务器的硬件信息和接口访问模块的授权信息，防止数据导出到其它医院可以使用
        /// </summary>
        [MaxLength(4096, ErrorMessage = "授权验证数据最大长度50")]
        public byte[] ValidateData { get; set; }

    }
}
