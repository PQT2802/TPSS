using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TPSS.Data.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TPSS.Business.Common
{
    public static class Validator
    {
        private static readonly Regex regexMail = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private static readonly Regex regexUserName = new(@"^(?![-_.0-9])(?!.*[-_.][-_.])(?!.*[-_.]$)[A-Za-z0-9-_.]+$");
        private static readonly Regex regexName = new(@"^([a-zA-Z]+)+$");
        //Has minimum 8 characters in length. Adjust it by modifying {8,}
        // At least one uppercase English letter.You can remove this condition by removing (?=.*?[A - Z])
        //At least one lowercase English letter.You can remove this condition by removing (?=.*?[a - z])
        //At least one digit.You can remove this condition by removing (?=.*?[0 - 9])
        //At least one special character,  You can remove this condition by removing (?=.*?[#?!@$%^&*-])
        private static readonly Regex regexPassword = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        private static readonly Regex regexPhone = new("^\\+?[1-9][0-9]{7,14}$");
        private static readonly Regex regexPersonalId = new(@"^\d{12}$");


        public static bool IsValidUsername(string value) 
        {
            bool result;
            Match match = regexUserName.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }
        public static bool IsValidName(string value)
        {
            bool result;
            Match match = regexName.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool IsValidPassword(string value)
        {
            bool result;
            Match match = regexPassword.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool IsValidEmail(string value)
        {
            bool result;
            Match match = regexMail.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool IsValidPhone(string value)
        {
            bool result;
            Match match = regexPhone.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }
        public static bool IsValidPersonalId(string value)
        {
            bool result;
            Match match = regexPersonalId.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

    }
}
