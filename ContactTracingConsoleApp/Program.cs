using ContactTracingApp.Models;
using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;




namespace ContactTracingConsoleApp
{
    class Program
    {
        private static HttpClient API;

        private static void Main(string[] args)
        {
            API = new HttpClient();
            //API.BaseAddress = new Uri("https://contacttracingapp.azurewebsites.net");
            API.BaseAddress = new Uri("http://localhost:60601");
            API.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GetAllContacts().Wait();
            AddAContact().Wait();
            GetContactsById().Wait();
        }

        private static async Task AddAContact()
        {
            Contact c1 = new Contact() { Id = 2, FirstName = "John", LastName = "Smart", Mobile = "555-333",
                Email = "stuff@nonsense", ContactId = 12312,  DateMet = DateTime.Now, DistanceKept = 10, PersonId =1231, TimeSpent = 2 };

            HttpResponseMessage response = await API.PostAsJsonAsync("/api/Contacts/AddContact", c1);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Contact Added");
            }
            else
            {
                Console.WriteLine("AddAContact - " + response.StatusCode + " " + response.ReasonPhrase);

            }
        }
        private static async Task GetContactsById()
        {
            HttpResponseMessage response = await API.GetAsync("/api/Contacts/GetContactsById");
            if (response.IsSuccessStatusCode)
            {
                // read results 
                var contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
                foreach (var c in contacts)
                {
                    Console.WriteLine("Contact: " + c.FirstName + " " + c.LastName);
                }
            }
            else
            {
                Console.WriteLine("GetAllContacts - " + response.StatusCode + " " + response.ReasonPhrase);
            }

        }
        private static async Task GetAllContacts()
        {
            HttpResponseMessage response = await API.GetAsync("/api/Contacts/GetContacts");
            if (response.IsSuccessStatusCode)
            {
                // read results 
                var contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
                foreach (var c in contacts)
                {
                    Console.WriteLine("Contact: " + c.FirstName + " " + c.LastName);
                }
            }
            else
            {
                Console.WriteLine("GetAllContacts - " + response.StatusCode + " " + response.ReasonPhrase);
            }
        }
    }
}
