using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static DeveloperCert.Globals;

namespace DeveloperCert
{
    class Program
    {
        private static string userName, password;
        private static string accessToken;
        private static string firstName, lastName, pp, dpPercent, rate,term;
        private static string loanNumber, appId, checkingBalance, mutualFundBalance;
        private static string loanId;
        static void Main(string[] args)
        {
            CompleteAuthentication();
            CreateLoan();
        }

        private static void CreateLoan()
        {
            Console.WriteLine("First Name:");
            firstName = Console.ReadLine();
            Console.WriteLine("Last Name:");
            lastName = Console.ReadLine();
            Console.WriteLine("Purchase Price:");
            pp = Console.ReadLine();
            Console.WriteLine("Down Payment Percentage:");
            dpPercent = Console.ReadLine();
            Console.WriteLine("Note Rate:");
            rate = Console.ReadLine();
            Console.WriteLine("Loan Term:");
            term = Console.ReadLine();
            var task2 = CreatePurchaseLoan(firstName, lastName, pp, dpPercent, rate, term);
            Task.WaitAll(task2);
            loanId = GetLoanId();
            Console.WriteLine("\nThe loan ID (GUID) is: " + loanId);
            Console.ReadLine();
        }
        private static void CompleteAuthentication()
        {
            Console.WriteLine("User Name:");
            userName = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            var task1 = SetAccessToken(userName, password);

            Task.WaitAll(task1);
            accessToken = GetAccessToken();
            Console.WriteLine("The access token is: " + accessToken);
            Console.ReadLine();
        }
        private static void AddVOD()
        {
            Console.WriteLine("Loan Number:");
            loanNumber = Console.ReadLine();
            Console.WriteLine("Application ID:");
            appId = Console.ReadLine();
            Console.WriteLine("Checking Account Balance:");
            checkingBalance = Console.ReadLine();
            Console.WriteLine("Mutual Fund Balance:");
            mutualFundBalance = Console.ReadLine();
            var task3 = AddVODExisting(loanNumber, appId, checkingBalance, mutualFundBalance);
            Task.WaitAll(task3);
        }
        
    }
}
