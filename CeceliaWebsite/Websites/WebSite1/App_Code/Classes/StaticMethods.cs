using System;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Summary description for StaticMethods
/// </summary>
/// 
namespace Cecelia {
    public static class StaticMethods {

        public static string EncryptPassword(string password) {
            string strResult;
            UTF8Encoding textConverter;
            byte[] bytePassword, byteHashedPassword;
            SHA384Managed shaEncoder;
            try {
                textConverter = new UTF8Encoding();
                bytePassword = textConverter.GetBytes(password);
                //Converts the string to byte stream
                shaEncoder = new SHA384Managed();
                byteHashedPassword = shaEncoder.ComputeHash(bytePassword);
                strResult = Convert.ToBase64String(byteHashedPassword);
            } finally {
                textConverter = null;
                bytePassword = null;
                byteHashedPassword = null;
                shaEncoder = null;
            }
            return strResult;
        }

    }
}