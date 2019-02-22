﻿using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;

namespace Clear.UserPermission.Application.Dtos.Modules
{
    [AutoMapTo(typeof(Module))]
    public class UpdateModuleAuthInput
    {
        /// <summary>
        /// 父级主键
        /// </summary> 
        public Guid Id { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary> 
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public int SortCode { get; set; }
        /// <summary>
        /// 图标
        /// </summary> 
        public string Icon { get; set; }
        /// <summary>
        /// 导航地址
        /// </summary> 
        public string UrlAddress { get; set; }
        /// <summary>
        /// 导航目标
        /// </summary> 
        public string Target { get; set; }
        /// <summary>
        /// 允许自动展开
        /// </summary> 
        public bool AllowAutoExpand { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 模块权限
        /// </summary>
        public List<ModuleAuthChildDto> ModuleAuths { get; set; }
    }
}
