using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DeveloperCert
{

    internal class Globals
    {
        private static string token;
        private static string lnId = "";

        public static string GetAccessToken()
        {
            return token;
        }

        public static async Task SetAccessToken(string un, string pw)
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/oauth2/v1/token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", "iceStudent"+"@encompass:BE11166969");
            request.AddParameter("password", "student@DEV22");
            request.AddParameter("client_id", "ruc51qr");
            request.AddParameter("client_secret", "ysli#MSdlbyHngOyctiJmK$0l82^9!o$dzSwvhW612!GPoi38loF4CFQ8ZiJTu$@");
            RestResponse response = await client.ExecuteAsync(request);
            //Console.WriteLine(response.Content);
            //Console.ReadLine();
            JObject respContent = JObject.Parse(response.Content);
            //Instantiate a JToken object with the extracted access token
            JToken aToken = respContent.SelectToken("$.access_token");
            //Set the token variable to the extracted access token as a string
            token = aToken.ToString();
        }
        public static string GetLoanId()
        {
            return lnId;
        }
        public static void SetLoanId(string id)
        {
            id = lnId;
        }
        public static async Task CreatePurchaseLoan(string fname, string lname, string ppamt, string dppercent, string noterate, string term)
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/encompass/v3/loans?loanFolder=DevEssentialsCert&view=entity", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            var body = @"{" + "\n" +
            @"    ""applications"": [" + "\n" +
            @"        {" + "\n" +
            @"            ""borrower"": {" + "\n" +
            @"                ""firstName"": """ + fname + @" "", " + "\n" +
            @"                ""lastName"": """ + lname + @" "" "+"\n"+ 
            @"            }" + "\n" +
            @"        }" + "\n" +
            @"    ]," + "\n" +
            @"    ""property"": {" + "\n" +
            @"        ""loanPurposeType"": ""Cash-Out Refinance""" + "\n" +
            @"    }," + "\n" +
            @"    ""purchasePriceAmount"": """ + ppamt + @" ""," + "\n" +
            @"    ""downPaymentPercent"": """ + dppercent + @" ""," + "\n" +
            @"    ""requestedInterestRatePercent"": """ + noterate + @" ""," + "\n" +
            @"    ""loanAmortizationTermMonths"": """ + term + @" "" "+ "\n" +
            @"" + "\n" +
            @"    }" + "\n" +
            @"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            //Parse the response content
            JObject respContent = JObject.Parse(response.Content);
            //Instantiate a JToken object with the extracted lnId
            JToken loanGuid = respContent.SelectToken("$.id");
            //Set the lnId variable to the extracted lnId as a string
            lnId = loanGuid.ToString();
        }
        public static async Task AddVODExisting(string loanNumber, string appId, string checkingBalance, string mutualFundBalance)
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/encompass/v3/loans/{loan}/applications/{app}/vods?action=add&view=entity", Method.Patch)
                .AddUrlSegment("loan", loanNumber)
                .AddUrlSegment("app", appId);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            var body = @"[ {
            " + "\n" +
                        @"                    ""holderName"": ""ICE Lending"",
            " + "\n" +
                        @"                    ""holderAddressStreetLine1"": ""2210 Main Street"",
            " + "\n" +
                        @"                    ""holderAddressCity"": ""Waukesha"",
            " + "\n" +
                        @"                    ""holderAddressState"": ""WI"",
            " + "\n" +
                        @"                    ""holderAddressPostalCode"": 53188,
            " + "\n" +
                        @"                    ""owner"": ""Borrower"",
            " + "\n" +
                        @"                    ""items"": [
            " + "\n" +
                        @"            {
            " + "\n" +
                        @"                ""itemNumber"": 1,
            " + "\n" +
                        @"                ""type"": ""CheckingAccount"",
            " + "\n" +
                        @"                ""accountIdentifier"": ""128450-25"",
            "
            + "\n" +
                        @"                ""urla2020CashOrMarketValueAmount"":""" + checkingBalance + @" "",
            " + "\n" +
                        @"                ""depositoryAccountName"": ""Krish Tester""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            {
            " + "\n" +
                        @"                ""itemNumber"": 2,
            " + "\n" +
                        @"                ""type"": ""Mutual Fund"",
            " + "\n" +
                        @"                ""accountIdentifier"": ""128450-26"",
            " + "\n" +
                        @"                ""urla2020CashOrMarketValueAmount"": """ + mutualFundBalance + @" "",
            " + "\n" +
                        @"                ""depositoryAccountName"": ""Krish Tester""
            " + "\n" +
                        @"            }
            " + "\n" +
                        @"          
            " + "\n" +
                        @"        ]
            " + "\n" +
                        @"    }
            " + "\n" +
                        @"]";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadLine();
        }
    }
}
