using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hidistro.Core
{
    public sealed class Serializer
    {
        static bool CanBinarySerialize;

        static Serializer()
        {
            SecurityPermission permission = new SecurityPermission(SecurityPermissionFlag.SerializationFormatter);
            try
            {
                permission.Demand();
                CanBinarySerialize = true;
            }
            catch (SecurityException)
            {
                CanBinarySerialize = false;
            }
        }

        Serializer()
        {
        }

        public static object ConvertFileToObject(string path, Type objectType)
        {
            //object myObject = null;
            if ((path == null) || (path.Length <= 0))
            {
                return null;
            }
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(objectType);
                return serializer.Deserialize(stream);
            }
        }

        public static void ConvertFromNameValueCollection(NameValueCollection nvc, ref string keys, ref string values)
        {
            if ((nvc != null) && (nvc.Count != 0))
            {

                StringBuilder keystr = new StringBuilder();

                StringBuilder valstr = new StringBuilder();

                int sum = 0;

                string val = "";

                foreach (string item in nvc.AllKeys)
                {

                    if (item.IndexOf(':') != -1)
                    {
                        throw new ArgumentException("ExtendedAttributes Key can not contain the character \":\"");
                    }

                    val = nvc[item];

                    if (!string.IsNullOrEmpty(val))
                    {
                        keystr.AppendFormat("{0}:S:{1}:{2}:", item, sum, val.Length);

                        valstr.Append(val);

                        sum += val.Length;
                    }
                }

                keys = keystr.ToString();

                values = valstr.ToString();

            }
        }

        public static byte[] ConvertToBytes(object objectToConvert)
        {
            byte[] buffer = null;
            if (CanBinarySerialize)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, objectToConvert);
                    stream.Position = 0;
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }
            }
            return buffer;
        }

        public static NameValueCollection ConvertToNameValueCollection(string keys, string values)
        {
            NameValueCollection nvc = new NameValueCollection();

            if ((((keys != null) && (values != null)) && (keys.Length > 0)) && (values.Length > 0))
            {
                char[] separator = new char[] { ':' };

                string[] keyArray = keys.Split(separator);

                for (int i = 0; i < (keyArray.Length / 4); i++)
                {
                    int startIndex = int.Parse(keyArray[(i * 4) + 2]);

                    int length = int.Parse(keyArray[(i * 4) + 3]);

                    string str = keyArray[i * 4];

                    if ((((keyArray[(i * 4) + 1] == "S") && (startIndex >= 0)) && (length > 0)) && (values.Length >= (startIndex + length)))
                    {
                        nvc[str] = values.Substring(startIndex, length);
                    }
                }
            }
            return nvc;
        }

        public static object ConvertToObject(byte[] byteArray)
        {
            object myObject = null;
            if ((CanBinarySerialize && (byteArray != null)) && (byteArray.Length > 0))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, byteArray.Length);

                    stream.Position = 0;

                    if (byteArray.Length > 4)
                    {
                        myObject = formatter.Deserialize(stream);
                    }

                }

            }

            return myObject;
        }

        public static object ConvertToObject(string xml, Type objectType)
        {
            //System.IO.File.AppendAllText(@"D:\wwwroot\hankhans\log.txt", xml);
            object myobject = null;

            if (string.IsNullOrEmpty(xml))
            {
                return myobject;
            }

            //xml = xml.Replace("__", "");

            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(objectType);
                myobject = serializer.Deserialize(reader);
            }

            return myobject;

        }

        public static object ConvertToObject(XmlNode node, Type objectType)
        {

            object myObject = null;

            if (null != node)
            {

                using (StringReader reader = new StringReader(node.OuterXml))
                {
                    XmlSerializer serializer = new XmlSerializer(objectType);
                    myObject = serializer.Deserialize(reader);
                }

            }

            return myObject;
        }

        public static string ConvertToString(object objectToConvert)
        {
            string xmlstr = null;
            if (objectToConvert == null)
            {
                return xmlstr;
            }
            XmlSerializer serializer = new XmlSerializer(objectToConvert.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize((TextWriter)writer, objectToConvert);
                xmlstr = writer.ToString();
            }
            return xmlstr;
        }

        public static string ConvertToString(object objectToConvert, params Type[] extra)
        {
            string xmlstr = null;
            if (objectToConvert == null)
            {
                return xmlstr;
            }
            XmlSerializer serializer = new XmlSerializer(objectToConvert.GetType(), extra);
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize((TextWriter)writer, objectToConvert);
                xmlstr = writer.ToString();
            }
            return xmlstr;
        }

        public static object LoadBinaryFile(string path)
        {
            byte[] buffer;
            if (!File.Exists(path))
            {
                return null;
            }
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    buffer = new byte[stream.Length];
                    reader.Read(buffer, 0, (int)stream.Length);
                }
            }
            return ConvertToObject(buffer);
        }

        public static bool SaveAsBinary(object objectToSave, string path)
        {
            if ((objectToSave != null) && CanBinarySerialize)
            {
                byte[] buffer = ConvertToBytes(objectToSave);
                if (buffer != null)
                {
                    using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            writer.Write(buffer);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void SaveAsXML(object objectToConvert, string path)
        {
            if (objectToConvert != null)
            {
                XmlSerializer serializer = new XmlSerializer(objectToConvert.GetType());
                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize((TextWriter)writer, objectToConvert);
                }
            }
        }
    }
}

