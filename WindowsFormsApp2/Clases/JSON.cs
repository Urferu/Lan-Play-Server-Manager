using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace WindowsFormsApp2.Clases
{
    internal class JSON
    {
        public const int TOKEN_NONE = 0;

        public const int TOKEN_CURLY_OPEN = 1;

        public const int TOKEN_CURLY_CLOSE = 2;

        public const int TOKEN_SQUARED_OPEN = 3;

        public const int TOKEN_SQUARED_CLOSE = 4;

        public const int TOKEN_COLON = 5;

        public const int TOKEN_COMMA = 6;

        public const int TOKEN_STRING = 7;

        public const int TOKEN_NUMBER = 8;

        public const int TOKEN_TRUE = 9;

        public const int TOKEN_FALSE = 10;

        public const int TOKEN_NULL = 11;

        private const int BUILDER_CAPACITY = 2000;

        public static object JsonDecode(string json)
        {
            bool flag = true;
            return JSON.JsonDecode(json, ref flag);
        }

        public static object JsonDecode(string json, ref bool success)
        {
            success = true;
            if (json != null)
            {
                char[] json2 = json.ToCharArray();
                int num = 0;
                return JSON.ParseValue(json2, ref num, ref success);
            }
            return null;
        }

        public static string JsonEncode(object json)
        {
            StringBuilder stringBuilder = new StringBuilder(2000);
            if (!JSON.SerializeValue(json, stringBuilder))
            {
                return null;
            }
            return stringBuilder.ToString();
        }

        protected static stdClassCSharp ParseObject(char[] json, ref int index, ref bool success)
        {
            stdClassCSharp stdccs = new stdClassCSharp();
            JSON.NextToken(json, ref index);
            bool flag = false;
            while (!flag)
            {
                int num = JSON.LookAhead(json, index);
                if (num == 0)
                {
                    success = false;
                    return null;
                }
                if (num == 6)
                {
                    JSON.NextToken(json, ref index);
                }
                else
                {
                    if (num == 2)
                    {
                        JSON.NextToken(json, ref index);
                        return stdccs;
                    }
                    string key = JSON.ParseString(json, ref index, ref success);
                    if (!success)
                    {
                        success = false;
                        return null;
                    }
                    num = JSON.NextToken(json, ref index);
                    if (num != 5)
                    {
                        success = false;
                        return null;
                    }
                    var value = JSON.ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        success = false;
                        return null;
                    }
                    stdccs[key.ToString()] = value;
                }
            }
            return stdccs;
        }

        protected static stdClassCSharp ParseArray(char[] json, ref int index, ref bool success)
        {
            stdClassCSharp arrayList = new stdClassCSharp(true);
            JSON.NextToken(json, ref index);
            bool flag = false;
            int indiceArreglo = 0;
            while (!flag)
            {
                int num = JSON.LookAhead(json, index);
                if (num == 0)
                {
                    success = false;
                    return null;
                }
                if (num == 6)
                {
                    JSON.NextToken(json, ref index);
                }
                else
                {
                    if (num == 4)
                    {
                        JSON.NextToken(json, ref index);
                        break;
                    }
                    var value = JSON.ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        return null;
                    }
                    arrayList[indiceArreglo] = value;
                    indiceArreglo++;
                }
            }
            return arrayList;
        }

        protected static dynamic ParseValue(char[] json, ref int index, ref bool success)
        {
            switch (JSON.LookAhead(json, index))
            {
                case 1:
                    return JSON.ParseObject(json, ref index, ref success);
                case 3:
                    return JSON.ParseArray(json, ref index, ref success);
                case 7:
                    return JSON.ParseString(json, ref index, ref success);
                case 8:
                    return JSON.ParseNumber(json, ref index, ref success);
                case 9:
                    JSON.NextToken(json, ref index);
                    return true;
                case 10:
                    JSON.NextToken(json, ref index);
                    return false;
                case 11:
                    JSON.NextToken(json, ref index);
                    return null;
            }
            success = false;
            return null;
        }

        protected static string ParseString(char[] json, ref int index, ref bool success)
        {
            StringBuilder stringBuilder = new StringBuilder(2000);
            JSON.EatWhitespace(json, ref index);
            char c = json[index++];
            bool flag = false;
            while (!flag && index != json.Length)
            {
                c = json[index++];
                if (c == '"')
                {
                    flag = true;
                    break;
                }
                if (c == '\\')
                {
                    if (index == json.Length)
                    {
                        break;
                    }
                    c = json[index++];
                    if (c == '"')
                    {
                        stringBuilder.Append('"');
                    }
                    else if (c == '\\')
                    {
                        stringBuilder.Append('\\');
                    }
                    else if (c == '/')
                    {
                        stringBuilder.Append('/');
                    }
                    else if (c == 'b')
                    {
                        stringBuilder.Append('\b');
                    }
                    else if (c == 'f')
                    {
                        stringBuilder.Append('\f');
                    }
                    else if (c == 'n')
                    {
                        stringBuilder.Append('\n');
                    }
                    else if (c == 'r')
                    {
                        stringBuilder.Append('\r');
                    }
                    else if (c == 't')
                    {
                        stringBuilder.Append('\t');
                    }
                    else if (c == 'u')
                    {
                        int num = json.Length - index;
                        if (num < 4)
                        {
                            break;
                        }
                        uint utf;
                        if (!(success = uint.TryParse(new string(json, index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out utf)))
                        {
                            return "";
                        }
                        stringBuilder.Append(char.ConvertFromUtf32((int)utf));
                        index += 4;
                    }
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }
            if (!flag)
            {
                success = false;
                return null;
            }
            return stringBuilder.ToString();
        }

        protected static double ParseNumber(char[] json, ref int index, ref bool success)
        {
            JSON.EatWhitespace(json, ref index);
            int lastIndexOfNumber = JSON.GetLastIndexOfNumber(json, index);
            int length = lastIndexOfNumber - index + 1;
            double result;
            success = double.TryParse(new string(json, index, length), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            index = lastIndexOfNumber + 1;
            return result;
        }

        protected static int GetLastIndexOfNumber(char[] json, int index)
        {
            int num = index;
            while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
            {
                num++;
            }
            return num - 1;
        }

        protected static void EatWhitespace(char[] json, ref int index)
        {
            while (index < json.Length)
            {
                if (" \t\n\r".IndexOf(json[index]) == -1)
                {
                    return;
                }
                index++;
            }
        }

        protected static int LookAhead(char[] json, int index)
        {
            int num = index;
            return JSON.NextToken(json, ref num);
        }

        protected static int NextToken(char[] json, ref int index)
        {
            JSON.EatWhitespace(json, ref index);
            if (index == json.Length)
            {
                return 0;
            }
            char c = json[index];
            index++;
            char c2 = c;
            switch (c2)
            {
                case '"':
                    return 7;
                case '#':
                case '$':
                case '%':
                case '&':
                case '\'':
                case '(':
                case ')':
                case '*':
                case '+':
                case '.':
                case '/':
                    break;
                case ',':
                    return 6;
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return 8;
                case ':':
                    return 5;
                default:
                    switch (c2)
                    {
                        case '[':
                            return 3;
                        case '\\':
                            break;
                        case ']':
                            return 4;
                        default:
                            switch (c2)
                            {
                                case '{':
                                    return 1;
                                case '}':
                                    return 2;
                            }
                            break;
                    }
                    break;
            }
            index--;
            int num = json.Length - index;
            if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
            {
                index += 5;
                return 10;
            }
            if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
            {
                index += 4;
                return 9;
            }
            if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
            {
                index += 4;
                return 11;
            }
            return 0;
        }

        protected static bool SerializeValue(object value, StringBuilder builder)
        {
            bool result = true;
            if (value is string)
            {
                result = JSON.SerializeString((string)value, builder);
            }
            else if (value is Hashtable)
            {
                result = JSON.SerializeObject((Hashtable)value, builder);
            }
            else if (value is ArrayList)
            {
                result = JSON.SerializeArray((ArrayList)value, builder);
            }
            else if (value is bool && (bool)value)
            {
                builder.Append("true");
            }
            else if (value is bool && !(bool)value)
            {
                builder.Append("false");
            }
            else if (value is ValueType)
            {
                result = JSON.SerializeNumber(Convert.ToDouble(value), builder);
            }
            else if (value == null)
            {
                builder.Append("null");
            }
            else
            {
                result = false;
            }
            return result;
        }

        protected static bool SerializeObject(Hashtable anObject, StringBuilder builder)
        {
            builder.Append("{");
            IDictionaryEnumerator enumerator = anObject.GetEnumerator();
            bool flag = true;
            while (enumerator.MoveNext())
            {
                string aString = enumerator.Key.ToString();
                object value = enumerator.Value;
                if (!flag)
                {
                    builder.Append(", ");
                }
                JSON.SerializeString(aString, builder);
                builder.Append(":");
                if (!JSON.SerializeValue(value, builder))
                {
                    return false;
                }
                flag = false;
            }
            builder.Append("}");
            return true;
        }

        protected static bool SerializeArray(ArrayList anArray, StringBuilder builder)
        {
            builder.Append("[");
            bool flag = true;
            for (int i = 0; i < anArray.Count; i++)
            {
                object value = anArray[i];
                if (!flag)
                {
                    builder.Append(", ");
                }
                if (!JSON.SerializeValue(value, builder))
                {
                    return false;
                }
                flag = false;
            }
            builder.Append("]");
            return true;
        }

        protected static bool SerializeString(string aString, StringBuilder builder)
        {
            builder.Append("\"");
            char[] array = aString.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                char c = array[i];
                if (c == '"')
                {
                    builder.Append("\\\"");
                }
                else if (c == '\\')
                {
                    builder.Append("\\\\");
                }
                else if (c == '\b')
                {
                    builder.Append("\\b");
                }
                else if (c == '\f')
                {
                    builder.Append("\\f");
                }
                else if (c == '\n')
                {
                    builder.Append("\\n");
                }
                else if (c == '\r')
                {
                    builder.Append("\\r");
                }
                else if (c == '\t')
                {
                    builder.Append("\\t");
                }
                else
                {
                    int num = Convert.ToInt32(c);
                    if (num >= 32 && num <= 126)
                    {
                        builder.Append(c);
                    }
                    else
                    {
                        builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
                    }
                }
            }
            builder.Append("\"");
            return true;
        }

        protected static bool SerializeNumber(double number, StringBuilder builder)
        {
            builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
            return true;
        }
    }
}
