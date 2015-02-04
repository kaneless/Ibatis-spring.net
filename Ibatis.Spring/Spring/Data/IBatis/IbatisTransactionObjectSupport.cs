using Common.Logging;
using IBatisNet.Common.Transaction;
using Spring.Transaction;
using System;

namespace Spring.Data.IBatis
{
    public abstract class IbatisTransactionObjectSupport : ISavepointManager
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IbatisTransactionObjectSupport));
        private IsolationLevel previousIsolationLevel;
        private bool savepointAllowed;
        private Spring.Data.IBatis.SqlMapHolder sqlMapHolder;

        protected IbatisTransactionObjectSupport()
        {
        }

        public void CreateSavepoint(string savepointName)
        {
            throw new NotImplementedException();
        }

        public void ReleaseSavepoint(string savepoint)
        {
            throw new NotImplementedException();
        }

        public void RollbackToSavepoint(string savepoint)
        {
            throw new NotImplementedException();
        }

        public bool HasSqlMapHolder
        {
            get
            {
                return (this.sqlMapHolder != null);
            }
        }

        public IsolationLevel PreviousIsolationLevel
        {
            get
            {
                return this.previousIsolationLevel;
            }
            set
            {
                this.previousIsolationLevel = value;
            }
        }

        public bool SavepointAllowed
        {
            get
            {
                return this.savepointAllowed;
            }
            set
            {
                this.savepointAllowed = value;
            }
        }

        public Spring.Data.IBatis.SqlMapHolder SqlMapHolder
        {
            get
            {
                return this.sqlMapHolder;
            }
            set
            {
                this.sqlMapHolder = value;
            }
        }
    }
}

