using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Axon.Application.Common.Extentions
{
    public static class Extentions
    {
        public static readonly string SAFE_PASSWORD_PATTERN = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})";
        public static readonly string WHOLE_NUMBER_PATTERN = @"/^\d+$/";
        public static readonly string ALPHANUMERIC_PATTERN = @"/^[a-zA-Z0-9 ]*$/";
        public static readonly string EMAIL_PATTERN = @"/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})*$/";
        public static readonly string URL_PATTERN = @"/(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/";
        public static readonly string IP_PATTERN = @"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))";
        public static readonly string DATE_PATTERN = @"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))";
        public static readonly string PHONE_PATTERN = @"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$";
        public static readonly string JS_PATTERN = @"\bon\w+=\S+(?=.*>)";
        public static readonly string HTML_PATTERN = @"<\/?[\w\s]*>|<.+[\W]>";
        public static readonly string COLOR_HEX_PATTERN = @"/^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/";

        public static string Pronounce(this string PersianText)
        {

            PersianText = PersianText.Trim();
            PersianText = PersianText.Cleanup();
            PersianText = FixPersianChars(PersianText);

            PersianText = PersianText.Replace("ا", "A");
            PersianText = PersianText.Replace("آ", "A");
            PersianText = PersianText.Replace("ب", "B");
            PersianText = PersianText.Replace("پ", "P");
            PersianText = PersianText.Replace("ت", "T");
            PersianText = PersianText.Replace("ث", "S");
            PersianText = PersianText.Replace("ج", "J");
            PersianText = PersianText.Replace("چ", "CH");
            PersianText = PersianText.Replace("ح", "H");
            PersianText = PersianText.Replace("خ", "KH");
            PersianText = PersianText.Replace("د", "D");
            PersianText = PersianText.Replace("ذ", "Z");
            PersianText = PersianText.Replace("ر", "R");
            PersianText = PersianText.Replace("ز", "Z");
            PersianText = PersianText.Replace("ژ", "ZH");
            PersianText = PersianText.Replace("س", "S");
            PersianText = PersianText.Replace("ش", "SH");
            PersianText = PersianText.Replace("ص", "S");
            PersianText = PersianText.Replace("ض", "Z");
            PersianText = PersianText.Replace("ط", "T");
            PersianText = PersianText.Replace("ظ", "Z");
            PersianText = PersianText.Replace("ع", "A");
            PersianText = PersianText.Replace("غ", "Q");
            PersianText = PersianText.Replace("ف", "F");
            PersianText = PersianText.Replace("ق", "Q");
            PersianText = PersianText.Replace("ک", "K");
            PersianText = PersianText.Replace("گ", "G");
            PersianText = PersianText.Replace("ل", "L");
            PersianText = PersianText.Replace("م", "M");
            PersianText = PersianText.Replace("ن", "N");
            PersianText = PersianText.Replace("و", "V");
            PersianText = PersianText.Replace("ه", "H");
            PersianText = PersianText.Replace("ی", "Y");
            PersianText = PersianText.Replace("ي", "Y");

            PersianText = PersianText.Replace("ۀ", "H");
            PersianText = PersianText.Replace("ی", "Y");
            PersianText = PersianText.Replace("ك", "K");


            PersianText = PersianText.Replace("۹", "NINE");
            PersianText = PersianText.Replace("۸", "EIGHT");
            PersianText = PersianText.Replace("۷", "SEVEN");
            PersianText = PersianText.Replace("۶", "SIX");
            PersianText = PersianText.Replace("۵", "FIVE");
            PersianText = PersianText.Replace("۴", "FOUR");
            PersianText = PersianText.Replace("۳", "THREE");
            PersianText = PersianText.Replace("۲", "TWO");
            PersianText = PersianText.Replace("۱", "ONE");
            PersianText = PersianText.Replace("۰", "ZERO");

            PersianText = PersianText.Replace(" ", "");
            PersianText = PersianText.Replace("ؙ", "");//zammeh
            PersianText = PersianText.Replace("ؘ", "");//fatheh
            PersianText = PersianText.Replace("ؚ", "");//kasreh

            PersianText = PersianText.ToUpper();
            return PersianText;

        }

        public static string Soundex(this string InputValue)
        {

            InputValue = Pronounce(InputValue);

            StringBuilder output = new StringBuilder();

            if (InputValue.Length > 0)
            {
                output.Append(char.ToUpper(InputValue[0]));

                for (int i = 1; i < InputValue.Length && output.Length < 4; i++)
                {
                    string c = EncodeChar(InputValue[i]);

                    if (c != EncodeChar(InputValue[i - 1]))
                    {
                        output.Append(c);
                    }
                }

                for (int i = output.Length; i < 4; i++)
                {
                    output.Append("0");
                }
            }

            return output.ToString();
        }

        private static string EncodeChar(this char c)
        {

            switch (char.ToLower(c))
            {
                case 'b':
                case 'f':
                case 'p':
                case 'v':
                    return "1";
                case 'c':
                case 'g':
                case 'j':
                case 'k':
                case 'q':
                case 's':
                case 'x':
                case 'z':
                    return "2";
                case 'd':
                case 't':
                    return "3";
                case 'l':
                    return "4";
                case 'm':
                case 'n':
                    return "5";
                case 'r':
                    return "6";
                default:
                    return string.Empty;
            }
        }

        public static bool IsValidNationalCode(this string nationalCode)
        {

            if (string.IsNullOrEmpty(nationalCode))
            {
                return false;
            }

            if (nationalCode.Length != 10)
            {
                return false;
            }

            Regex regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
            {
                return false;
            }

            string[] allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode))
            {
                return false;
            }

            char[] chArray = nationalCode.ToCharArray();
            int num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            int num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            int num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            int num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            int num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            int num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            int num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            int num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            int num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            int a = Convert.ToInt32(chArray[9].ToString());

            int b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            int c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string first = input.Substring(0, 1).ToLower();
            if (input.Length == 1)
            {
                return first;
            }

            return first + input.Substring(1);
        }

        public static string ToUpperCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string first = input.Substring(0, 1).ToUpper();
            if (input.Length == 1)
            {
                return first;
            }

            return first + input.Substring(1);
        }

        public static bool IsSheba(this string Sheba)
        {
            Sheba = Sheba.Replace(" ", "").ToLower();
            if (Sheba.Length < 26 || Sheba is null)
            {
                return false;
            }

            bool isSheba = Regex.IsMatch(Sheba, "^[a-zA-Z]{2}\\d{2} ?\\d{4} ?\\d{4} ?\\d{4} ?\\d{4} ?[\\d]{0,2}", RegexOptions.Compiled);

            if (!isSheba) { return false; }

            string right4digit = Sheba.Substring(0, 4);
            string replaceright4digit = right4digit.ToLower().Replace("i", "18").Replace("r", "27");
            string removeright4digit = Sheba.Replace(right4digit, "");
            string newSheba = removeright4digit + replaceright4digit;
            decimal finalLongData = Convert.ToDecimal(newSheba);
            decimal finalReminder = finalLongData % 97;
            if (finalReminder == 1)
            {
                return true;
            }
            return false;

        }

        public static bool IsMatchRegex(this string inputValue, string pattern)
        {
            Regex regex = new Regex(pattern);
            return (regex.IsMatch(inputValue));
        }

        public static string Encrypt(this string PlainText)
        {
            string key = "EA036776F5B603109DCFF3BC993ED4F2";
            return EncryptString(PlainText, key);
        }

        public static string Decrypt(this string PlainText)
        {
            string key = "EA036776F5B603109DCFF3BC993ED4F2";
            return DecryptString(PlainText, key);
        }
        public static string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public static string Right(this string inputText, int length)
        {
            return inputText != null && inputText.Length > length ? inputText.Substring(inputText.Length - length) : inputText;
        }

        public static string Left(this string inputText, int length)
        {
            return inputText != null && inputText.Length > length ? inputText.Substring(0, length) : inputText;
        }

        public static string ToPersianNumber(this string input)
        {
            if (input.Trim() == "")
            {
                return "";
            }

            input = input.Replace("0", "۰");
            input = input.Replace("1", "۱");
            input = input.Replace("2", "۲");
            input = input.Replace("3", "۳");
            input = input.Replace("4", "۴");
            input = input.Replace("5", "۵");
            input = input.Replace("6", "۶");
            input = input.Replace("7", "۷");
            input = input.Replace("8", "۸");
            input = input.Replace("9", "۹");
            return input;
        }


        public static string ToEnglishNumber(this string input)
        {
            if (input.Trim() == "")
            {
                return "";
            }

            input = input.Replace("۰", "0");
            input = input.Replace("۱", "1");
            input = input.Replace("۲", "2");
            input = input.Replace("۳", "3");
            input = input.Replace("۴", "4");
            input = input.Replace("۵", "5");
            input = input.Replace("۶", "6");
            input = input.Replace("۷", "7");
            input = input.Replace("۸", "8");
            input = input.Replace("۹", "9");
            return input;
        }

        public static string ToJson<T>(this T item, System.Text.Encoding encoding = null, DataContractJsonSerializer serializer = null)
        {
            encoding = encoding ?? Encoding.Default;
            serializer = serializer ?? new DataContractJsonSerializer(typeof(T));

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                serializer.WriteObject(stream, item);
                string json = encoding.GetString((stream.ToArray()));

                return json;
            }
        }

        public static bool IsNullOrEmpty<T>(this IList<T> inputIList)
        {
            return inputIList == null || !inputIList.Any() || inputIList.Count < 1;
        }

        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return (DateTime.TryParse(input, out DateTime dt));
            }
            else
            {
                return false;
            }
        }

        public static string RemoveTags(this string input)
        {
            Regex tagsStrruct = new Regex(@"</?.+?>");
            return tagsStrruct.Replace(input, " ");
        }

        public static string FixPersianChars(this string RowPersian)
        {
            return RowPersian.Replace('ي', 'ی').Replace("ك", "ک").Replace("أ", "ا");
        }

        public static string Cleanup(this string RawText)
        {
            RawText = RawText.Trim();
            RawText = FixPersianChars(RawText);
            RawText = ToEnglishNumber(RawText);
            RawText = RawText.Replace("ۀ", "ه");
            RawText = RawText.Replace("ی", "ی");
            RawText = RawText.Replace("ك", "ک");
            RawText = RemoveTags(RawText);

            return RawText;

        }

        public static bool IsValidUrl(this string inputText)
        {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(inputText);
        }

        public static string Humanize(this DateTime dateTime)
        {
            string result = "";
            TimeSpan ts = DateTime.UtcNow - dateTime;

            if (ts.Minutes < 1) { result = "چند لحظه قبل"; }
            if (ts.Minutes >= 1 && ts.Minutes < 60) { result = ts.Minutes.ToString() + " دقیقه قبل"; }
            if (ts.Hours <= 1 && ts.Hours < 24) { result = ts.Hours.ToString() + " ساعت قبل"; }
            if (ts.Days >= 1 && ts.Days < 30) { result = ts.Days.ToString() + " روز قبل"; }
            if (ts.Days >= 30 && ts.Days < 365) { result = (ts.Days / 30).ToString() + " ماه قبل"; }
            if (ts.Days >= 365 && ts.Days < 3650) { result = (ts.Days / 365).ToString() + " سال قبل"; }
            if (ts.Days >= 3650) { result = "خیلی وقت پیش"; }

            if (Math.Abs(ts.Minutes) < 1 && ts.Seconds < 0) { result = "چند لحظه بعد"; }
            if (Math.Abs(ts.Minutes) >= 1 && Math.Abs(ts.Minutes) < 60 && ts.Seconds < 0) { result = Math.Abs(ts.Minutes).ToString() + " دقیقه بعد"; }
            if (Math.Abs(ts.Hours) <= 1 && Math.Abs(ts.Hours) < 24 && ts.Seconds < 0) { result = Math.Abs(ts.Hours).ToString() + " ساعت بعد"; }
            if (Math.Abs(ts.Days) >= 1 && Math.Abs(ts.Days) < 30 && ts.Seconds < 0) { result = Math.Abs(ts.Days).ToString() + " روز بعد"; }
            if (Math.Abs(ts.Days) >= 30 && Math.Abs(ts.Days) < 365 && ts.Seconds < 0) { result = (Math.Abs(ts.Days) / 30).ToString() + " ماه بعد"; }
            if (Math.Abs(ts.Days) >= 365 && Math.Abs(ts.Days) < 3650 && ts.Seconds < 0) { result = (Math.Abs(ts.Days) / 365).ToString() + " سال بعد"; }
            if (Math.Abs(ts.Days) >= 3650 && ts.Seconds < 0) { result = "آینده دور"; }

            return result;
        }


        public static string ToXML(this string json)
        {
            // To convert JSON text contained in string json into an XML node
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
            return doc.ToString();
        }
        public static string ToJSON(this string xml)
        {
            // To convert an XML node contained in string xml into a JSON string
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(doc);
        }


        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar taghvim = new System.Globalization.PersianCalendar();
            string result = taghvim.GetYear(dateTime).ToString() + "/" + taghvim.GetMonth(dateTime).ToString() + "/" + taghvim.GetDayOfMonth(dateTime).ToString();
            return result;

        }

        public static DateTime ToGeoDate(this string persianDate)
        {
            try
            {
                persianDate = persianDate.ToEnglishNumber();
                DateTime dt = DateTime.Parse(persianDate, new CultureInfo("fa-IR"));
                return dt.ToUniversalTime();

            }
            catch (Exception)
            {
                return DateTime.UtcNow;

            }


        }

        public static string GenerateSHA256Hash(this string input)
        {
            var salt = "mAhiHFteKDbRdmmiMz6Gk7oNU9Rvsmtv2oYfXc1XFM4Ob8FX99xV8Xw4GffZm98";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static bool SendMessageToSlack(this string message)
        {
            var data = new NameValueCollection();
            data["channel"] = "pos-log";
            data["text"] = message;
            data["as_user"] = "false"; // to send this message as the user who owns the token, false by default
            data["token"] = "xoxp-358722275687-887098390295-879762001779-476d2e27b47a5f56d8a840f3d1266d8f";

            Uri slackApiUri = new Uri("https://slack.com/api/chat.postMessage");
            var client = new WebClient();
            client.UploadValuesAsync(slackApiUri, "POST", data);
            return true;
        }

        public static long GenerateId(this long oldId)
        {

            var result = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString() + DateTime.UtcNow.Day.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();
            return long.Parse(result);
        }
        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        public static string Base64Encode(this string plainText)
        {
            byte[] v = System.Text.Encoding.UTF8.GetBytes(plainText);
            var plainTextBytes = v;

            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentException("message", nameof(plainText));
            }
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                throw new ArgumentException("message", nameof(base64EncodedData));
            }

            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string GenerateInvitationCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, false));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(3, false));
            return builder.ToString();
        }
        public static string GenerateEndSessionCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(1, false));
            builder.Append(RandomNumber(1, 9));
            builder.Append(RandomString(2, false));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public static int GenerateVerificationCode()
        {
            int _min = 100000;
            int _max = 999999;
            Random rnd = new Random();
            int code = rnd.Next(_min, _max);
            return code;
        }
        public static int SetVerificationCode(this int code)
        {
            int _min = 100000;
            int _max = 999999;
            Random rnd = new Random();
            code = rnd.Next(_min, _max);
            return code;
        }
      

        public static double CalculateAverageRating(this IEnumerable<int> rates)
        {
            return rates.Where(x => x != 0).DefaultIfEmpty(0).Average();
        }

        public static T? GetValueOrNull<T>(this string valueAsString)
        where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        public static bool IsGuid(this string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }



        public static byte[] ToByteArray(this IFormFile photo)
        {
            MemoryStream ms = new MemoryStream();
            photo.CopyTo(ms);
            return ms.ToArray();
        }


        public static double MeterToDistance(this double meter)
        {
            return meter / 10000;
        }
    }
}
  