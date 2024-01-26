using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TPSS.Business.Common
{
    public static class Validator
    {
        private static readonly Regex regexMail = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private static readonly Regex regexUserName = new(@"^(?![-_.0-9])(?!.*[-_.][-_.])(?!.*[-_.]$)[A-Za-z0-9-_.]+$");	


        public static bool IsValidUsername(string value) 
        {
            bool result;
            Match match = regexUserName.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }



    }
}
