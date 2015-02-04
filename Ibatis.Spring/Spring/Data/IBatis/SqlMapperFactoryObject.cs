using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using Spring.Context;
using System;
using System.IO;

namespace Spring.Data.IBatis
{
    public class SqlMapperFactoryObject : AbstractIBatisFactoryObject, IApplicationContextAware
    {
        private IApplicationContext springContext;
        private DomSqlMapBuilder sqlMapBuilder;

        protected override object CreateUsingCustomConfig()
        {
            if (base.ConfigWatcher != null)
            {
                return this.SqlMapBuilder.ConfigureAndWatch(base.Config.File, base.ConfigWatcher);
            }
            using (Stream stream = base.Config.InputStream)
            {
                return this.SqlMapBuilder.Configure(stream);
            }
        }

        protected override object CreateUsingDefaultConfig()
        {
            if (base.ConfigWatcher != null)
            {
                return this.SqlMapBuilder.ConfigureAndWatch(base.ConfigWatcher);
            }
            return this.SqlMapBuilder.Configure();
        }

        public IApplicationContext ApplicationContext
        {
            get
            {
                return this.springContext;
            }
            set
            {
                this.springContext = value;
            }
        }

        public override Type ObjectType
        {
            get
            {
                return typeof(ISqlMapper);
            }
        }

        public DomSqlMapBuilder SqlMapBuilder
        {
            get
            {
                if (this.sqlMapBuilder == null)
                {
                    this.sqlMapBuilder = new DomSqlMapBuilder();
                }
                return this.sqlMapBuilder;
            }
            set
            {
                this.sqlMapBuilder = value;
            }
        }
    }
}

