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
using Zek.Cryptography;
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
            


            var sw = new Stopwatch();
            var count = 1000000;
            sw.Start();
            for (var i = 0; i < count; i++)
            {
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();


            sw.Start();
            for (var i = 0; i < count; i++)
            {
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);


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