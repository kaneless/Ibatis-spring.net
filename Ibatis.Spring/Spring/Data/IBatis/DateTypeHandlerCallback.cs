using IBatisNet.DataMapper.TypeHandlers;
using System;

namespace Spring.Data.IBatis
{
    public class DateTypeHandlerCallback : ITypeHandlerCallback
    {
        object ITypeHandlerCallback.GetResult(IResultGetter getter)
        {
            if ((getter.Value == null) || (getter.Value == DBNull.Value))
            {
                return DateTime.MinValue;
            }
            return getter.Value;
        }

        void ITypeHandlerCallback.SetParameter(IParameterSetter setter, object parameter)
        {
            if (parameter is DateTime)
            {
                DateTime time = (DateTime) parameter;
                if (time == DateTime.MinValue)
                {
                    setter.Value=null;
                    return;
                }
            }
            setter.Value=parameter;
        }

        object ITypeHandlerCallback.ValueOf(string s)
        {
            return DateTime.Parse(s);
        }

        object ITypeHandlerCallback.NullValue
        {
            get
            {
                return DBNull.Value;
            }
        }
    }
}

