using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserAppService : IOpenWebApi
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">用户信息</param>
        Guid CreateUser(InputCreateUser input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        void DeleteUser(Guid userId);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        void UpdateUser(InputUpdateUser input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userId"></param>
        void RestUserPassword(Guid userId);


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<UserDto> GetUsers(InputGetUsers input);

        /// <summary>
        /// 获取简单用户信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns>接口最多返回50个用户信息</returns>
        [DontNeedAuth]
        List<SimpleUserDto> GetSimpleUsers(InputGetSimpleUsers input);


        /// <summary>
        /// 获取用户信息（需要用户已经登陆）
        /// </summary>
        /// <returns></returns>
        [DontNeedAuth]
        UserAuthorizationDto GetUserInfo();

        /// <summary>
        /// 修改用户密码（需要用户已经登陆）
        /// </summary>
        /// <param name="input"></param>
        [DontNeedAuth]
        void ChangePassword(ChangePasswordInput input);

        /// <summary>
        /// 用户登出
        /// </summary>
        [DontNeedAuth]
        void Logout();
    }
}
