//namespace Hishop.Weixin.MP.Util
//{
//    using Hishop.Weixin.MP.Domain;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reflection;
//    using System.Xml.Linq;

//    public static class EntityHelper
//    {
//        public static XDocument ConvertEntityToXml<T>(T entity) where T: class, new()
//        {
//            if (entity == null)
//            {
//            }
//            entity = Activator.CreateInstance<T>();
//            XDocument document = new XDocument();
//            document.Add(new XElement("xml"));
//            XElement root = document.Root;
//            List<string> list = new string[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Content", "ArticleCount", "Articles", "FuncFlag", "Title ", "Description ", "PicUrl", "Url" }.ToList<string>();
//            Func<string, int> orderByPropName = new Func<string, int>(list.IndexOf);
//            List<PropertyInfo> list2 = (from p in entity.GetType().GetProperties()
//                orderby orderByPropName(p.Name)
//                select p).ToList<PropertyInfo>();
//            foreach (PropertyInfo info in list2)
//            {
//                DateTime time;
//                string name = info.Name;
//                if (name == "Articles")
//                {
//                    XElement content = new XElement("Articles");
//                    List<Article> list3 = info.GetValue(entity, null) as List<Article>;
//                    foreach (Article article in list3)
//                    {
//                        IEnumerable<XElement> enumerable = ConvertEntityToXml<Article>(article).Root.Elements();
//                        content.Add(new XElement("item", enumerable));
//                    }
//                    root.Add(content);
//                }
//                else
//                {
//                    string str2 = info.PropertyType.Name;
//                    if (str2 == null)
//                    {
//                        goto Label_0353;
//                    }
//                    if (!(str2 == "String"))
//                    {
//                        if (str2 == "DateTime")
//                        {
//                            goto Label_026D;
//                        }
//                        if (str2 == "Boolean")
//                        {
//                            goto Label_02A6;
//                        }
//                        if (str2 == "ResponseMsgType")
//                        {
//                            goto Label_02F9;
//                        }
//                        if (str2 == "Article")
//                        {
//                            goto Label_0326;
//                        }
//                        goto Label_0353;
//                    }
//                    root.Add(new XElement(name, new XCData((info.GetValue(entity, null) as string) ?? "")));
//                }
//                continue;
//            Label_026D:
//                time = (DateTime) info.GetValue(entity, null);
//                root.Add(new XElement(name, time.Ticks));
//                continue;
//            Label_02A6:
//                if (name == "FuncFlag")
//                {
//                    root.Add(new XElement(name, ((bool) info.GetValue(entity, null)) ? "1" : "0"));
//                    continue;
//                }
//                goto Label_0353;
//            Label_02F9:
//                root.Add(new XElement(name, info.GetValue(entity, null).ToString().ToLower()));
//                continue;
//            Label_0326:
//                root.Add(new XElement(name, info.GetValue(entity, null).ToString().ToLower()));
//                continue;
//            Label_0353:
//                root.Add(new XElement(name, info.GetValue(entity, null)));
//            }
//            return document;
//        }

//        public static void FillEntityWithXml<T>(T entity, XDocument doc) where T: AbstractRequest, new()
//        {
//            if (entity == null)
//            {
//            }
//            entity = Activator.CreateInstance<T>();
//            XElement root = doc.Root;
//            PropertyInfo[] properties = entity.GetType().GetProperties();
//            foreach (PropertyInfo info in properties)
//            {
//                string name = info.Name;
//                if (root.Element(name) != null)
//                {
//                    string str2 = info.PropertyType.Name;
//                    if (str2 == null)
//                    {
//                        goto Label_01CC;
//                    }
//                    if (!(str2 == "DateTime"))
//                    {
//                        if (str2 == "Boolean")
//                        {
//                            goto Label_00FA;
//                        }
//                        if (str2 == "Int64")
//                        {
//                            goto Label_014B;
//                        }
//                        if (str2 == "RequestEventType")
//                        {
//                            goto Label_0176;
//                        }
//                        if (str2 == "RequestMsgType")
//                        {
//                            goto Label_01A1;
//                        }
//                        goto Label_01CC;
//                    }
//                    info.SetValue(entity, new DateTime(long.Parse(root.Element(name).Value)), null);
//                }
//                continue;
//            Label_00FA:
//                if (name == "FuncFlag")
//                {
//                    info.SetValue(entity, root.Element(name).Value == "1", null);
//                    continue;
//                }
//                goto Label_01CC;
//            Label_014B:
//                info.SetValue(entity, long.Parse(root.Element(name).Value), null);
//                continue;
//            Label_0176:
//                info.SetValue(entity, EventTypeHelper.GetEventType(root.Element(name).Value), null);
//                continue;
//            Label_01A1:
//                info.SetValue(entity, MsgTypeHelper.GetMsgType(root.Element(name).Value), null);
//                continue;
//            Label_01CC:
//                info.SetValue(entity, root.Element(name).Value, null);
//            }
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Hishop.Weixin.MP.Domain;

namespace Hishop.Weixin.MP.Util
{

    public static class EntityHelper
    {
        public static XDocument ConvertEntityToXml<T>(T entity) where T : class, new()
        {

            if (entity == null)
            {
                entity = Activator.CreateInstance<T>();
            }

            XDocument document = new XDocument();

            document.Add(new XElement("xml"));

            XElement root = document.Root;

            List<string> list = new string[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Content", "ArticleCount", "Articles", "FuncFlag", "Title ", "Description ", "PicUrl", "Url" }.ToList<string>();
            
            Func<string, int> orderByPropName = new Func<string, int>(list.IndexOf);

            List<PropertyInfo> list2 = (from p in entity.GetType().GetProperties() orderby orderByPropName(p.Name) select p).ToList<PropertyInfo>();

            string propName = "";//属性名称
            //DateTime time;
            string dataTypeName = "";//属性数据类型

            foreach (PropertyInfo info in list2)
            {
                propName = info.Name;//获取属性名称

                if ("Articles" == propName)
                {
                    XElement content = new XElement("Articles");
                    List<Article> list3 = info.GetValue(entity, null) as List<Article>;
                    foreach (Article article in list3)
                    {
                        IEnumerable<XElement> enumerable = ConvertEntityToXml<Article>(article).Root.Elements();
                        content.Add(new XElement("item", enumerable));
                    }
                    root.Add(content);
                }
                else
                {

                    dataTypeName = info.PropertyType.Name;

                    switch (dataTypeName)
                    {
                        case "String":
                            {
                                root.Add(new XElement(propName, new XCData((info.GetValue(entity, null) as string) ?? "")));
                                break;
                            }
                        case "DateTime":
                            {
                                //time = (DateTime)info.GetValue(entity, null);
                                root.Add(new XElement(propName, ((DateTime)info.GetValue(entity, null)).Ticks));
                                break;
                            }
                        case "Boolean":
                            {
                                if (propName == "FuncFlag")
                                {
                                    root.Add(new XElement(propName, ((bool)info.GetValue(entity, null)) ? "1" : "0"));
                                }
                                else
                                {
                                    root.Add(new XElement(propName, info.GetValue(entity, null)));
                                }
                                break;
                            }
                        case "ResponseMsgType":
                            {
                                root.Add(new XElement(propName, info.GetValue(entity, null).ToString().ToLower()));
                                break;
                            }
                        case "Article":
                            {
                                root.Add(new XElement(propName, info.GetValue(entity, null).ToString().ToLower()));
                                break;
                            }
                        case "Single":
                            {
                                float floatVal = 0f;
                                float.TryParse(root.Element(propName).Value, out floatVal);
                                info.SetValue(entity, floatVal, null);
                                break;
                            }
                        default:
                            {
                                root.Add(new XElement(propName, info.GetValue(entity, null)));
                                break;
                            }
                    }

                }


            }

            #region 注释
            /*
            foreach (PropertyInfo info in list2)
            {
                DateTime time;
                string name = info.Name;
                if (name == "Articles")
                {
                    XElement content = new XElement("Articles");
                    List<Article> list3 = info.GetValue(entity, null) as List<Article>;
                    foreach (Article article in list3)
                    {
                        IEnumerable<XElement> enumerable = ConvertEntityToXml<Article>(article).Root.Elements();
                        content.Add(new XElement("item", enumerable));
                    }
                    root.Add(content);
                }
                else
                {
                    string str2 = info.PropertyType.Name;
                    if (str2 == null)
                    {
                        goto Label_0353;
                    }
                    if (!(str2 == "String"))
                    {
                        if (str2 == "DateTime")
                        {
                            goto Label_026D;
                        }
                        if (str2 == "Boolean")
                        {
                            goto Label_02A6;
                        }
                        if (str2 == "ResponseMsgType")
                        {
                            goto Label_02F9;
                        }
                        if (str2 == "Article")
                        {
                            goto Label_0326;
                        }
                        goto Label_0353;
                    }
                    // root.Add(new XElement(name, new XCData((info.GetValue(entity, null) as string) ?? "")));
                }
                continue;
            Label_026D:
                // time = (DateTime)info.GetValue(entity, null);
                // root.Add(new XElement(name, time.Ticks));
                continue;
            Label_02A6:
                //if (name == "FuncFlag")
                //{
                //    root.Add(new XElement(name, ((bool)info.GetValue(entity, null)) ? "1" : "0"));
                //    continue;
                //}
                goto Label_0353;
            Label_02F9:
                //root.Add(new XElement(name, info.GetValue(entity, null).ToString().ToLower()));
                continue;
            Label_0326:
                //root.Add(new XElement(name, info.GetValue(entity, null).ToString().ToLower()));
                continue;
            Label_0353:
                root.Add(new XElement(name, info.GetValue(entity, null)));
            }*/
            #endregion

            return document;
        }

        public static void FillEntityWithXml<T>(T entity, XDocument doc) where T : AbstractRequest, new()
        {

            if (entity == null)
            {
                entity = Activator.CreateInstance<T>();
            }

            XElement root = doc.Root;

            PropertyInfo[] properties = entity.GetType().GetProperties();

            //属性名
            string propName = "";
            //数据类型名
            string dataTypeName = "";

            foreach (PropertyInfo info in properties)
            {
                propName = info.Name;//取出属性名称
                try
                {


                    if (root.Element(propName) != null)
                    {

                        dataTypeName = info.PropertyType.Name;

                        switch (dataTypeName)
                        {
                            case "DateTime":
                                {
                                    info.SetValue(entity, new DateTime(long.Parse(root.Element(propName).Value)), null);
                                    break;
                                }
                            case "Boolean":
                                {
                                    if (propName == "FuncFlag")
                                    {
                                        info.SetValue(entity, (root.Element(propName).Value == "1"), null);
                                    }
                                    else
                                    {
                                        info.SetValue(entity, root.Element(propName).Value, null);
                                    }
                                    break;
                                }
                            case "Int64":
                                {
                                    info.SetValue(entity, long.Parse(root.Element(propName).Value), null);
                                    break;
                                }
                            case "RequestEventType":
                                {
                                    info.SetValue(entity, EventTypeHelper.GetEventType(root.Element(propName).Value), null);
                                    break;
                                }
                            case "RequestMsgType":
                                {
                                    info.SetValue(entity, MsgTypeHelper.GetMsgType(root.Element(propName).Value), null);
                                    break;
                                }
                            case "Single":
                                {
                                    float floatVal = 0f;
                                    float.TryParse(root.Element(propName).Value, out floatVal);
                                    info.SetValue(entity, floatVal, null);
                                    break;
                                }
                            default:
                                {
                                    info.SetValue(entity, root.Element(propName).Value, null);
                                    break;
                                }

                        }


                    }
                }
                catch //(Exception ex)
                {
                    // System.IO.File.AppendAllText(@"E:\home\LocalUser\580442\www\humowang\api\err.txt", "type:" + entity.GetType().FullName + ";属性名称：" + propName + ";" + ex.Message + "\r\n");
                }

            }

            #region 注释
            /*
            foreach (PropertyInfo info in properties)
            {
                string name = info.Name;
                if (root.Element(name) != null)
                {
                    string str2 = info.PropertyType.Name;
                    if (str2 == null)
                    {
                        goto Label_01CC;
                    }
                    if (!(str2 == "DateTime"))
                    {
                        if (str2 == "Boolean")
                        {
                            goto Label_00FA;
                        }
                        if (str2 == "Int64")
                        {
                            goto Label_014B;
                        }
                        if (str2 == "RequestEventType")
                        {
                            goto Label_0176;
                        }
                        if (str2 == "RequestMsgType")
                        {
                            goto Label_01A1;
                        }
                        goto Label_01CC;
                    }
                    // info.SetValue(entity, new DateTime(long.Parse(root.Element(name).Value)), null);
                }
                continue;
            Label_00FA:
                if (name == "FuncFlag")
                {
                    //info.SetValue(entity, root.Element(name).Value == "1", null);
                    continue;
                }
                goto Label_01CC;
            Label_014B:
                //info.SetValue(entity, long.Parse(root.Element(name).Value), null);
                continue;
            Label_0176:
                //info.SetValue(entity, EventTypeHelper.GetEventType(root.Element(name).Value), null);
                continue;
            Label_01A1:
                info.SetValue(entity, MsgTypeHelper.GetMsgType(root.Element(name).Value), null);
                continue;
            Label_01CC:
                info.SetValue(entity, root.Element(name).Value, null);

            }*/
            #endregion

        }

    }


}

