using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionData.Handler
{
    public class ErrorCode
    {
        public static string ErrorDetails(int errorCode)
        {
            string details = "";
            if (errorCode == 1)
            {
                details = "One or more input values are wrongly typed";
            }
            if (errorCode == 2)
            {
                details = "One or more input values are null or empty";
            }
            else if (errorCode == 3)
            {
                details = "The currency code is not ISO 4217 valid";
            }
            else if (errorCode == 4)
            {
                details = "The amount is not a valid number";
            }
            return details;
        }
    }
}