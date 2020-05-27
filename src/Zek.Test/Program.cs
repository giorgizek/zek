using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

            var mailSender = new EmailSender(new EmailSenderOptions
            {
                Host = "mail.mercatus.mba",
                Port = 587,
                EnableSsl = false,
                UserName = "no-reply@mercatus.mba",
                Password = "Atr10nsyst3ms",
                FromEmail = "no-reply@mercatus.mba",
            });



            mailSender = new EmailSender(new EmailSenderOptions
            {
                Host = "uashared10.twinservers.net",
                Port = 465,
                EnableSsl = true,
                UserName = "no-reply@mercatus.mba",
                Password = "Atr10nsyst3ms",
                FromEmail = "no-reply@mercatus.mba",
            });

            
            //mailSender = new EmailSender(new EmailSenderOptions
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    UserName = "atrion.app@gmail.com",
            //    Password = "Atri0n.@pp123",
            //    FromEmail = "atrion.app@gmail.com",
            //});

            mailSender.SendEmailAsync("giorgizek@gmail.com", "hello", "test body").RunSync();





            Console.ReadKey();

            return;





            var sw = new Stopwatch();
            var count = 100000000;
            for (var i = 0; i < count; i++)
            {
                var t1 = (int)CRUD.Create;
            }
            for (var i = 0; i < count; i++)
            {
                var t2 = CRUD.Create.ToInt32();
            }


            sw.Start();
            for (var i = 0; i < count; i++)
            {
                var t1 = (int)CRUD.Create;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();


            sw.Start();
            for (var i = 0; i < count; i++)
            {
                var t2 = CRUD.Create.ToInt32();
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