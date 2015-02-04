using IBatisNet.Common.Utilities;
using Spring.Core.IO;
using Spring.Objects.Factory.Config;
using System;
using System.IO;

namespace Spring.Data.IBatis
{
    public abstract class AbstractIBatisFactoryObject : AbstractFactoryObject
    {
        private IResource configuration;
        private ConfigureHandler configWatcher;

        protected AbstractIBatisFactoryObject()
        {
        }

        protected override object CreateInstance()
        {
            if (this.Config == null)
            {
                return this.CreateUsingDefaultConfig();
            }
            return this.CreateUsingCustomConfig();
        }

        protected abstract object CreateUsingCustomConfig();
        protected abstract object CreateUsingDefaultConfig();
        protected virtual string GetConfigFileName()
        {
            FileInfo info = null;
            try
            {
                info = this.Config.File;
            }
            catch (IOException)
            {
                throw new ArgumentException("The 'Config' property cannot be resolved to an iBatis.NET config file that physically exists on the filesystem.");
            }
            return info.Name;
        }

        public IResource Config
        {
            get
            {
                return this.configuration;
            }
            set
            {
                this.configuration = value;
            }
        }

        public ConfigureHandler ConfigWatcher
        {
            get
            {
                return this.configWatcher;
            }
            set
            {
                this.configWatcher = value;
            }
        }
    }
}

