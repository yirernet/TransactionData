﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionData.Core.Interfaces
{
    public interface ITransactionDataProvider
    {
        void Save(string[] values);
        
    }
}
