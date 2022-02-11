using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailFunction.Model
{
    public class EmailModel
    {
        public string Message { get; set; }

        public List<string> Destinataries { get; set; }
    }


    public interface IEmailSettings
    {
        string Email { get; set; }

        string Password { get; set; }

        string Host { get; set; }

        string DisplayName { get; set; }
    }
    public  class EmailSettings : IEmailSettings
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string DisplayName { get; set; }

        public EmailSettings(string displayName, string host, string email, string password)
        {
           DisplayName = displayName;
           Host = host;
           Email = email;
           Password = password;
        }
    }
}
