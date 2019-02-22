using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Clear.CommonContext.Domain.SequenceModle;
using Clear.CommonContext.Infrastructure.EntityFramework;
using MySql.Data.MySqlClient;
using PlatformService.BridgeComponent.Domain.Sequence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Transactions;

namespace Clear.UserPermission.EntityFramework
{
    public class SequenceRepository : ISequenceRepository,Abp.Dependency.ITransientDependency
    {
        private readonly IRepository<SequenceModle, string> _sequenceModleRepository;

        public SequenceRepository(IRepository<SequenceModle, string> sequenceModleRepository)
        {
            _sequenceModleRepository = sequenceModleRepository;
        }

        public Sequence FindOrCreate(string sequenceName)
        {
            return _sequenceModleRepository.FirstOrDefault(sequenceName) ?? new Sequence(sequenceName);
        }

       

        public void Save(Sequence sequence)
        {
            SequenceModle sequenceModle = _sequenceModleRepository.FirstOrDefault(sequence.SequenceName);
            if (sequenceModle != null)
            {
                sequenceModle.Parameter = sequence.Parameter;
                sequenceModle.SerialNumber = sequence.SerialNumber;
                _sequenceModleRepository.Update(sequenceModle);
            }
            else
            {
                _sequenceModleRepository.Insert(sequence);
            }
        }
      
    }
}
