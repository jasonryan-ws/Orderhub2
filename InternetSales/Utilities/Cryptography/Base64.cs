using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Cryptography
{
    public static class Base64
    {
        public static string Encode(string data)
        {
            try
            {
                var encData_byte = new byte[data.Length];
                encData_byte = Encoding.UTF8.GetBytes(data);
                var encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string Decode(string data)
        {
            try
            {
                var encoder = new UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();
                var todecode_byte = Convert.FromBase64String(data);
                var charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                var decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                var result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
