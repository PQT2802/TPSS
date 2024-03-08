using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Business.Common
{
    public class TemporaryDataStorage
    {
        private static Dictionary<string, string> confirmationCodes = new Dictionary<string, string>();

        public static void SaveConfirmationCode(string email, string confirmationCode)
        {
            // Save confirmation code to temporary storage
            confirmationCodes[email] = confirmationCode;
        }

        public static string GetConfirmationCode(string email)
        {
            // Retrieve confirmation code from temporary storage
            if (confirmationCodes.TryGetValue(email, out string storedConfirmationCode))
            {
                return storedConfirmationCode;
            }
            return null;
        }

        public static void RemoveConfirmationCode(string email)
        {
            // Remove confirmation code from temporary storage
            if (confirmationCodes.ContainsKey(email))
            {
                confirmationCodes.Remove(email);
            }
        }
    }
}
