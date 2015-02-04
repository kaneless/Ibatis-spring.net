using IBatisNet.DataMapper;

using Spring.Transaction.Support;

namespace Spring.Data.IBatis
{
    public class SqlMapHolder : ResourceHolderSupport
    {
        private ISqlMapper currentSqlMap;
        private ISqlMapSession currentSqlMapSession;
        private bool transactionActive = false;

        public SqlMapHolder(ISqlMapper sqlMap, ISqlMapSession transaction)
        {
            this.currentSqlMap = sqlMap;
            this.currentSqlMapSession = transaction;
        }

        public override void Clear()
        {
            base.Clear();
            this.transactionActive = false;
        }

        public bool HasSqlMap
        {
            get
            {
                return (this.currentSqlMap != null);
            }
        }

        public ISqlMapper SqlMap
        {
            get
            {
                return this.currentSqlMap;
            }
            set
            {
                this.currentSqlMap = value;
            }
        }

        public ISqlMapSession Transaction
        {
            get
            {
                return this.currentSqlMapSession;
            }
            set
            {
                this.currentSqlMapSession = value;
            }
        }

        public bool TransactionActive
        {
            get
            {
                return this.transactionActive;
            }
            set
            {
                this.transactionActive = value;
            }
        }
    }
}

