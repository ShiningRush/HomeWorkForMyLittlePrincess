using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Clear.UserPermission.Domain.Entities;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Extensions;
using YiBan.Common.Pages;

namespace Clear.UserPermission.Application
{
    public class GetRobotsInput : PagerParameter
    {
        /// <summary>
        /// 机器人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid? DepartmentId { get; set; }
    }

    [AutoMapFrom(typeof(Robot))]
    public class GetRobotsOutput
    {
        /// <summary>
        /// 机器人Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 机器人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 机器人描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }
    }

    [AutoMapTo(typeof(Robot))]
    public class InsertOrUpdateRobotInput
    {
        /// <summary>
        /// 机器人Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 机器人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 机器人描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }
    }

    /// <summary>
    /// 机器人服务
    /// </summary>
    public interface IRobotAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取机器人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagerResult<GetRobotsOutput> GetRobots(GetRobotsInput input);
        /// <summary>
        /// 插入或更新机器人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int InsertOrUpdateRobot(InsertOrUpdateRobotInput input);

        /// <summary>
        /// 删除机器人
        /// </summary>
        /// <param name="input"></param>
        void DeleteRobot(List<int> input);
    }

    /// <summary>
    /// 机器人服务
    /// </summary>
    public class RobotAppService : ApplicationService, IRobotAppService
    {
        private readonly IRepository<Robot> _robotRepo;

        public RobotAppService(IRepository<Robot> repository)
        {
            _robotRepo = repository;
        }

        /// <summary>
        /// 删除机器人
        /// </summary>
        /// <param name="input"></param>
        public void DeleteRobot(List<int> input)
        {
            foreach (var aId in input)
            {
                _robotRepo.Delete(aId);
            }
        }

        /// <summary>
        /// 获取机器人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagerResult<GetRobotsOutput> GetRobots(GetRobotsInput input)
        {
            var result = new PagerResult<GetRobotsOutput>();

            var robots = _robotRepo.GetAll()
                .WhereIf(input.DepartmentId.HasValue, p => p.DepartmentId == input.DepartmentId)
                .WhereIf(!string.IsNullOrEmpty(input.Name), p => p.Name.Contains( input.Name))
                .Page(input, result)
                .ToList();

            result.Result = robots.MapTo<List<GetRobotsOutput>>();
            return result;
        }

        /// <summary>
        /// 插入或更新机器人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int InsertOrUpdateRobot(InsertOrUpdateRobotInput input)
        {
            return _robotRepo.InsertOrUpdateAndGetId(input.MapTo<Robot>());
        }
    }
}
