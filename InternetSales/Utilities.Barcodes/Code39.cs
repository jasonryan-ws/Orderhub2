using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Barcodes
{
    /// <summary>
    /// Code 39
    /// Convert an input string to the equivilant string including start and stop characters.
    /// </summary>
    public static class Code39
    {
        /// <summary>
        /// Converts an input string to the equivilant string, that need to be produced using the 'Code 3 de 9' font.
        /// </summary>
        /// <param name="value">String to be encoded</param>
        /// <returns>Encoded string start/stop characters included</returns>
        public static string StringToBarcode(string value)
        {
            return StringToBarcode(value, false);
        }
        /// <summary>
        /// Converts an input string to the equivilant string, that need to be produced using the 'Code 3 de 9' font.
        /// </summary>
        /// <param name="value">String to be encoded</param>
        /// <param name="addChecksum">Is checksum to be added</param>
        /// <returns>Encoded string start/stop and checksum characters included</returns>
        public static string StringToBarcode(string value, bool addChecksum)
        {
            // Parameters : a string
            // Return     : a string which give the bar code when it is dispayed with CODE128.TTF font
            // 			 : an empty string if the supplied parameter is no good
            bool isValid = true;
            char currentChar;
            string returnValue = string.Empty;
            int checksum = 0;
            if (value.Length > 0)
            {
                //Check for valid characters
                for (int CharPos = 0; CharPos < value.Length; CharPos++)
                {
                    currentChar = char.Parse(value.Substring(CharPos, 1));
                    if (!((currentChar >= '0' && currentChar <= '9') || (currentChar >= 'A' && currentChar <= 'Z') ||
                        currentChar == ' ' || currentChar == '-' || currentChar == '.' || currentChar == '$' ||
                        currentChar == '/' || currentChar == '+' || currentChar == '%'))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    // Add start char
                    returnValue = "*";
                    // Add other chars, and calc checksum
                    for (int CharPos = 0; CharPos < value.Length; CharPos++)
                    {
                        currentChar = char.Parse(value.Substring(CharPos, 1));
                        returnValue += currentChar.ToString();
                        if (currentChar >= '0' && currentChar <= '9')
                        {
                            checksum = checksum + (int)currentChar - 48;
                        }
                        else if (currentChar >= 'A' && currentChar <= 'Z')
                        {
                            checksum = checksum + (int)currentChar - 55;
                        }
                        else
                        {
                            switch (currentChar)
                            {
                                case '-':
                                    checksum = checksum + (int)currentChar - 9;
                                    break;
                                case '.':
                                    checksum = checksum + (int)currentChar - 9;
                                    break;
                                case '$':
                                    checksum = checksum + (int)currentChar + 3;
                                    break;
                                case '/':
                                    checksum = checksum + (int)currentChar - 7;
                                    break;
                                case '+':
                                    checksum = checksum + (int)currentChar - 2;
                                    break;
                                case '%':
                                    checksum = checksum + (int)currentChar + 5;
                                    break;
                                case ' ':
                                    checksum = checksum + (int)currentChar + 6;
                                    break;
                            }
                        }
                    }
                    // Calculation of the checksum ASCII code
                    if (addChecksum)
                    {
                        checksum = checksum % 43;
                        if (checksum >= 0 && checksum <= 9)
                        {
                            returnValue += ((char)(checksum + 48)).ToString();
                        }
                        else if (checksum >= 10 && checksum <= 35)
                        {
                            returnValue += ((char)(checksum + 55)).ToString();
                        }
                        else
                        {
                            switch (checksum)
                            {
                                case 36:
                                    returnValue += "-";
                                    break;
                                case 37:
                                    returnValue += ".";
                                    break;
                                case 38:
                                    returnValue += " ";
                                    break;
                                case 39:
                                    returnValue += "$";
                                    break;
                                case 40:
                                    returnValue += "/";
                                    break;
                                case 41:
                                    returnValue += "+";
                                    break;
                                case 42:
                                    returnValue += "%";
                                    break;
                            }
                        }
                    }
                    // Add stop char
                    returnValue += "*";
                }
            }
            return returnValue;
        }
    }
}
