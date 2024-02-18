using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

using OnlineBankingApp.Models;

namespace OnlineBankingApp.Services
{
    public class LdapDirectoryService : ILdapDirectoryService
    {
        private const string EmailAttribute = "mail";
        private const string UserNameAttribute = "uid";

        private readonly LdapConfig config;

        public LdapDirectoryService(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }

#pragma warning disable CA1416
        //public User Search(string userName)
        //{
        //    using (var entry = new DirectoryEntry(config.Path) { AuthenticationType = AuthenticationTypes.Anonymous })
        //    using (var searcher = new DirectorySearcher(entry,
        //        $"(&({UserNameAttribute}={userName}))", new[] { UserNameAttribute, EmailAttribute }))
        //    {
        //        var result = searcher.FindOne();
        //        if (result != null)
        //        {
        //            var email = result.Properties[EmailAttribute];
        //            var uid = result.Properties[UserNameAttribute];

        //            return new User
        //            {
        //                UserName = uid.Count > 0 ? uid[0].ToString() : null,
        //                Email = email.Count > 0 ? email[0].ToString() : null
        //            };
        //        }
        //    }
        //    return null;
        //}
        public User Search(string userName)
        {
            if (Regex.IsMatch(userName, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                using (var entry = new DirectoryEntry(config.Path) { AuthenticationType = AuthenticationTypes.Anonymous })
                using (var searcher = new DirectorySearcher(entry,
                    $"(&({UserNameAttribute}={userName}))", new[] { UserNameAttribute, EmailAttribute }))
                {
                    var result = searcher.FindOne();
                    if (result != null)
                    {
                        var email = result.Properties[EmailAttribute];
                        var uid = result.Properties[UserNameAttribute];

                        return new User
                        {
                            UserName = uid.Count > 0 ? uid[0].ToString() : null,
                            Email = email.Count > 0 ? email[0].ToString() : null
                        };
                    }
                }
            }
            return null;
        }
#pragma warning restore CA1416
    }

    public interface ILdapDirectoryService
    {
        User Search(string userName);
    }
}