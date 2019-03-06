using System;
using static System.Console;
using System.Text.RegularExpressions;

namespace _09_Regex
{
    class Program
    {
        static void Main(string[] args)
        {

            //Validate Ali's phone numbre
            WriteLine("=====================   Validating Phone Numbers  =================");
            var phoneNumber = "(+92) 336-071-7272";
            if(IsValidPhone (phoneNumber))
            {
                WriteLine($"Phone Number: {phoneNumber}, Is Valid");
            }
            else
            {
                WriteLine($"Phone Number: {phoneNumber}, Is NOT Valid");
            }

            phoneNumber = "(+56) 9 5871 2295";
            if(IsValidPhone (phoneNumber))
            {
                WriteLine($"Phone Number: {phoneNumber}, Is Valid");
            }
            else
            {
                WriteLine($"Phone Number: {phoneNumber}, Is NOT Valid");
            }

            WriteLine("\n===================   Validating Email Address  =====================");
            //Validate Email Address
            var emailAddress = "dbarraza.mg@gmail.com";
            if(isValidateEmailAddress(emailAddress))
            {
                WriteLine($"Email address : {emailAddress}, Is Valid");
            }
            else
            {
                WriteLine($"Email address : {emailAddress}, Is NOT Valid");    
            }

            emailAddress = "myEmail@";
            if(isValidateEmailAddress(emailAddress))
            {
                WriteLine($"Email address : {emailAddress}, Is Valid");
            }
            else
            {
                WriteLine($"Email address : {emailAddress}, Is NOT Valid");    
            }
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            //Pattern form matching phone
            string pattern = @"\(\+92\)\s\d{3}-\d{3}-\d{4}";
            /*
            \(     : match with '('
            \+     : match with '+'
            92     : match with '92'
            \)     : match with ')'
            \s     : match with ' '
            \d{3}  : match with digits three times, it's equivalent to '456'
            -      : match with '-'
            \d{3}  : match with digits three times, it's equivalent to '456'
            -      : match with '-'
            \d{4}  : match with digits four times, it's equivalent to '4567'
             */


            bool isMatch = Regex.IsMatch(phone,pattern);

            return isMatch;
        }

        private static bool isValidateEmailAddress(string email)
        {
            //Pattern form matching email
            string pattern = @"^\w+[a-zA-Z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+)\.\w{2,4}";
            /*
            ^                   :   matches everything from the start
            \w+                 :   tells there must be at least one or more alpabhets (debe comenzar con una letra???)
            [a-zA-Z0-9]+        :   tells there must be one or more alphanumeric
            ([-._][a-z0-9]+)*   :   tells there can be a special character and alphanumeric
            @                   :   match with '@'
            ([a-z0-9])+         :   tells there must
            .                   :   match with '.'
            \w{2,4}             :   tells there must be a minimun 2 or maximun 4 words
             */



            var isMatch = Regex.IsMatch(email,pattern);

            return isMatch;
        }
    }
}
