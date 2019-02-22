﻿using Abp.AutoMapper;
using Clear.CommonContext.Domain.DataItemAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos
{
    [AutoMap(typeof(DataItem))]
    public class UpdateDataItemInput: DataItemDto
    {
        public string Id { get; set; }
        
    }
}
