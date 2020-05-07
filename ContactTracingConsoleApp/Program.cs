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
            API.BaseAddress = new Uri("https://contacttracingapp.azurewebsites.net");
            API.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GetAllContacts().Wait();
            AddAContact().Wait();
            GetContactsById(2).Wait();
            Console.ReadLine();
        }
        private static async Task AddAContact()
        {
            Contact c1 = new Contact()
            {
                Id = 2,
                FirstName = "John",
                LastName = "Smart",
                Mobile = "555-333",
                Email = "stuff@nonsense",
                ContactId = 12312,
                DateMet = DateTime.Now,
                DistanceKept = 10,
                PersonId = 1231,
                TimeSpent = 2
            };
            HttpResponseMessage response = await API.PostAsJsonAsync("/api/Contacts/AddContact", c1);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Contact Added " + "Name: "+ c1.FirstName +" "+ c1.LastName + "\nEmail: " +c1.Email);
            }
            else
            {
                Console.WriteLine("AddAContact - " + response.StatusCode + " " + response.ReasonPhrase);
            }
        }


        private static async Task GetContactsById(int id)
        {
            string addedId = "?id=" + id;
            HttpResponseMessage response = await API.GetAsync("/api/Contacts/GetContactsById"+ addedId);
            if (response.IsSuccessStatusCode)
            {
                // read results
                Console.WriteLine("---------------------------Get Contacts By Id--------------------------");
                var contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
                foreach (var c in contacts)
                {
                    if (c.Id == id)
                    {
                        Console.WriteLine("Contact: " + c.FirstName + " " + c.LastName);
                    }
                    else
                    {
                        Console.WriteLine("---------------No User found with that ID------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("---------------No User found with that ID------------------");
            }
        }
        private static async Task GetAllContacts()
        {
            HttpResponseMessage response = await API.GetAsync("/api/Contacts/GetContacts");
            if (response.IsSuccessStatusCode)
            {
                // read results
                Console.WriteLine("---------------------------Get All Contacts --------------------------");
                var contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
                foreach (var c in contacts)
                {
                    Console.WriteLine("----------------\nFirst Name: " + c.FirstName + "\nLast Name: " + c.LastName + "\nId Number: " + c.Id + "\nEmail: " + c.Email + "----------------");
                }
            }
            else
            {
                Console.WriteLine("GetAllContacts - " + response.StatusCode + " " + response.ReasonPhrase);
            }
        }
    }
}