using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace crawler

{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("You should write URL address");
            }

            string url = args[0];
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                throw new ArgumentException("Invalid URL parameter.");
            }

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = client.GetAsync(uriResult).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    HashSet<string> emailAddresses = new HashSet<string>();
                    MatchCollection matches = Regex.Matches(content, @"[\w.+-]+@[\w-]+\.[\w.-]+");

                    foreach (Match match in matches)
                    {
                        string emailAddress = match.Value.ToLower();
                        if (!emailAddresses.Contains(emailAddress))
                        {
                            emailAddresses.Add(emailAddress);
                        }
                    }

                    if (emailAddresses.Count > 0)
                    {
                        Console.WriteLine("Emails ");
                        foreach (string emailAddress in emailAddresses)
                        {
                            Console.WriteLine(emailAddress);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no email addresses");
                    }
                }
                else
                {
                    Console.WriteLine("Error while downloading the page");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
