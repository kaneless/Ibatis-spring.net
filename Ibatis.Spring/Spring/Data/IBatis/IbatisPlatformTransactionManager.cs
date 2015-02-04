using IBatisNet.DataMapper;
using Spring.Objects.Factory;
using Spring.Transaction;
using Spring.Transaction.Support;
using System;

namespace Spring.Data.IBatis
{
    public class IbatisPlatformTransactionManager : AbstractPlatformTransactionManager, IInitializingObject
    {
        private ISqlMapper sqlMap;

        public IbatisPlatformTransactionManager()
        {
        }

        public IbatisPlatformTransactionManager(ISqlMapper sqlMap)
        {
            this.sqlMap = sqlMap;
        }

        public void AfterPropertiesSet()
        {
        }

        protected override void DoBegin(object transaction, ITransactionDefinition definition)
        {
            if (this.sqlMap == null)
            {
                throw new ArgumentException("SqlMap is required to be set on IbatisPlatformTransactionManager");
            }
            base.log.Debug("开始事务");
            this.sqlMap.BeginTransaction();
        }

        protected override void DoCommit(DefaultTransactionStatus status)
        {
            base.log.Debug("提交事务");
            this.sqlMap.CommitTransaction();
        }

        protected override object DoGetTransaction()
        {
            IbatisTransactionObject obj2 = new IbatisTransactionObject();
            obj2.SavepointAllowed = base.NestedTransactionsAllowed;
            SqlMapHolder resource = (SqlMapHolder) TransactionSynchronizationManager.GetResource(this.sqlMap);
            obj2.SetSqlMapHolder(resource, false);
            return obj2;
        }

        protected override void DoRollback(DefaultTransactionStatus status)
        {
            base.log.Debug("回滚事务");
            this.sqlMap.RollBackTransaction();
        }

        protected override bool IsExistingTransaction(object transaction)
        {
            IbatisTransactionObject obj2 = (IbatisTransactionObject) transaction;
            return ((obj2.SqlMapHolder != null) && obj2.SqlMapHolder.TransactionActive);
        }

        public ISqlMapper SqlMap
        {
            get
            {
                return this.sqlMap;
            }
            set
            {
                this.sqlMap = value;
            }
        }

        private class IbatisTransactionObject : IbatisTransactionObjectSupport
        {
            private bool newSqlMapHolder;

            public void SetRollbackOnly()
            {
                base.SqlMapHolder.RollbackOnly = true;
            }

            public void SetSqlMapHolder(SqlMapHolder sqlMapHolder, bool newSqlMap)
            {
                base.SqlMapHolder = sqlMapHolder;
                this.newSqlMapHolder = newSqlMap;
            }

            public bool HasTransaction
            {
                get
                {
                    return ((base.SqlMapHolder != null) && base.SqlMapHolder.TransactionActive);
                }
            }

            public bool NewSqlMapHolder
            {
                get
                {
                    return this.newSqlMapHolder;
                }
            }

            public bool RollbackOnly
            {
                get
                {
                    return base.SqlMapHolder.RollbackOnly;
                }
            }
        }
    }
}

