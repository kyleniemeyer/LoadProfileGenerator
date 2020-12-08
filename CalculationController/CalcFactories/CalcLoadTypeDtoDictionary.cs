﻿using System.Collections.Generic;
using System.Linq;
using Common.CalcDto;
using Database.Tables.BasicHouseholds;
using JetBrains.Annotations;

namespace CalculationController.CalcFactories {
    public class CalcLoadTypeDtoDictionary
    {
        public CalcLoadTypeDtoDictionary([JetBrains.Annotations.NotNull] Dictionary<VLoadType, CalcLoadTypeDto> ltdtodict)
        {
            Ltdtodict = ltdtodict;
            //_guids = new HashSet<string>(ltdtodict.Values.Select(x => x.Guid));
        }

        [JetBrains.Annotations.NotNull]
        public Dictionary<VLoadType, CalcLoadTypeDto> Ltdtodict { get; }

        //public const string TableName = "CalcLoadTypeDtoDictionary";

        /*[ItemNotNull]
        [JetBrains.Annotations.NotNull]
        private readonly HashSet<string> _guids;*/

        [JetBrains.Annotations.NotNull]
        public CalcLoadTypeDto GetLoadtypeDtoByLoadType([JetBrains.Annotations.NotNull] VLoadType loadType)
        {
            return Ltdtodict[loadType];
        }
        /*
        public bool SimulateLoadtype([JetBrains.Annotations.NotNull] StrGuid guid)
        {
            if (_guids.Contains(guid))
            {
                return true;
            }

            return false;
        }*/

        public bool SimulateLoadtype([JetBrains.Annotations.NotNull] VLoadType lt)
        {
            if (Ltdtodict.ContainsKey(lt))
            {
                return true;
            }

            return false;
        }

        [ItemNotNull]
        [JetBrains.Annotations.NotNull]
        public List<CalcLoadTypeDto> GetLoadTypeDtos()
        {
            return Ltdtodict.Values.ToList();
        }
    }
}