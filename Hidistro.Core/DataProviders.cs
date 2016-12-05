using System;
using Hidistro.Core.Configuration;

namespace Hidistro.Core
{
    /// <summary>
    /// 数据提供者
    /// </summary>
    public sealed class DataProviders
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        DataProviders() { }

        /// <summary>
        /// 创建数据提供者实例
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        public static object CreateInstance(Provider dataProvider)
        {
            object targetInstance = null;

            if (null != dataProvider)
            {
                //取出类型
                Type type = Type.GetType(dataProvider.Type);

                if (type != null)
                {
                    targetInstance = Activator.CreateInstance(type);
                }

            }

            return targetInstance;

        }

        public static T CreateInstance<T>()
        {
            T myObjectInst = default(T);

            myObjectInst = Activator.CreateInstance<T>();

            return myObjectInst;
        }

        /// <summary>
        /// 创建数据提供者实例
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        public static object CreateInstance(string typeName)
        {

            object targetInstance = null;

            if (!string.IsNullOrEmpty(typeName))
            {

                //取出类型
                Type targetType = Type.GetType(typeName);

                if (null != targetType)
                {
                    //反射、创建类型实例
                    targetInstance = Activator.CreateInstance(targetType);
                }
            }

            return targetInstance;

        }
    }
}

