using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace CopyBackupTool.Helpers
{
    public static class HelpersStatic
    {
        public static bool CheckFileExists(string valuePath)
        {
            return (File.Exists(valuePath) ? true : false);
        }
        public static  bool CheckFolderExists(string valuePath)
        {
            return (Directory.Exists(valuePath) ? true : false);
        }
        public static bool IsJsonValid<TSchema>(this string value)
            where TSchema : new()
        {
            bool res = true;
            //this is a .net object look for it in msdn
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //first serialize the string to object.
            var obj = ser.Deserialize<TSchema>(value);

            //get all properties of schema object
            var properties = typeof(TSchema).GetProperties();
            //iterate on all properties and test.
            foreach (PropertyInfo info in properties)
            {
                // i went on if null value then json string isnt schema complient but you can do what ever test you like her.
                var valueOfProp = obj.GetType().GetProperty(info.Name).GetValue(obj, null);
                if (valueOfProp == null)
                    res = false;
            }

            return res;
        }
    }
}
