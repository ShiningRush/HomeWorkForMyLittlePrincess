using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MySql.Data.MySqlClient;
using PlatformService.BridgeComponent.Domain.Sequence;
using PlatformService.BridgeComponent.Domain.Sequence.Algorithm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Transactions;

namespace PlatformService.BridgeComponent.Domain.Sequence
{
    public class SequenceGenerator : ISequenceGenerator,Abp.Dependency.ITransientDependency
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SequenceGenerator(ISequenceRepository sequenceRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _sequenceRepository = sequenceRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public string Next(string sequenceName)
        {
            return Next(sequenceName,new PadLeftSequenceAlgorithm());
        }

        public string Next(string sequenceName, SequenceAlgorithm sequenceAlgorithm)
        {
            string sequenceNo = string.Empty;
            using (var unitWork = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                var sequence = _sequenceRepository.FindOrCreate(sequenceName);
                sequenceNo = sequenceAlgorithm.NextSequenceNo(sequence);
                if (sequenceAlgorithm is IAlgorithmNeedStorage)
                {
                    SaveSequence(sequence);
                }
                unitWork.Complete();
            }
            return sequenceNo;
        }

        private void SaveSequence(Sequence sequence)
        {
            _sequenceRepository.Save(sequence);
        }
    }
}
