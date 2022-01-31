using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Zek.Extensions;
using Zek.Model;
using Zek.Model.Config;
using Zek.Model.DTO.Email;
using Zek.Services;
using Zek.Utils;

namespace Zek.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            const string zzzz = "გიორგიgiorgi а";


            zzzz.RemoveDiacritics();

            var sw = new Stopwatch();
            var count = 10000000;
            sw.Start();
            for (var i = 0; i < count; i++)
            {
                zzzz.ToUpper(new CultureInfo("en-US"));
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();


            sw.Start();
            for (var i = 0; i < count; i++)
            {
                zzzz.SafeToUpper();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            return;



            //var x = (ISO4217.ISO4217?)z;

            //var s = x.ToString();

            //Console.WriteLine(s);

            //var roles = new[] { "Admin", "Accountant", "Accountant1", "Accountant2" };

            //var adminRoles = new[] { "Admin2222222222222222222", "Accountant222" };
            ////var canEdit = roles.Any(x => adminRoles.Contains(x, StringComparer.OrdinalIgnoreCase));

            //sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 1000000; i++)
            //{
            //    var canEdit = roles.Any(x => adminRoles.Contains(x, StringComparer.OrdinalIgnoreCase));
            //}
            //sw.Stop();

            //Console.WriteLine(sw.ElapsedMilliseconds);

            //var sender = new EmailSender(new EmailSenderOptions
            //{
            //    Host = "10.60.37.2",
            //    //Host = "10.60.40.117",
            //    Port = 25,
            //    UserName = "tagalogre@tstmailsrv.local",
            //    Password = "123321aA",
            //    EnableSsl = false
            //});


            //try
            //{
            //    sender.SendEmailAsync(new EmailDTO
            //    {
            //        From = new EmailAddressDTO
            //        {
            //            Address = "tagalogre@tstmailsrv.local",
            //            Name = "Tako Golden Girl"
            //        },
            //        To = new List<EmailAddressDTO>()
            //        {
            //            new EmailAddressDTO { Address = "ia.javakhishvili@tstmailsrv.local" }
            //        },

            //        Subject = "Test",
            //        Body = "Hello"
            //    }).RunSync();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}


            Console.ReadKey(false);
        }

        private static ILdapConnection _conn;

        static ILdapConnection GetConnection()
        {
            LdapConnection ldapConn = _conn as LdapConnection;

            if (ldapConn == null)
            {
                // Creating an LdapConnection instance 
                ldapConn = new LdapConnection() { SecureSocketLayer = false };

                //Connect function will create a socket connection to the server - Port 389 for insecure and 3269 for secure    
                ldapConn.Connect("uadevdc", 389);

                //Bind function with null user dn and password value will perform anonymous bind to LDAP server 
                ldapConn.Bind(@"uadev\ia.javakhishvili", "Zxcvbnm1!");
            }

            return ldapConn;
        }

    }
}