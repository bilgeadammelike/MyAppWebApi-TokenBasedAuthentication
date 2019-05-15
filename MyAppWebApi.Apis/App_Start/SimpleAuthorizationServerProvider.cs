using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace MyAppWebApi.Apis.App_Start
{
    public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {

        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //response header'a gelen tüm istekleri kabul etmek için
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (context.UserName == "test" && context.Password == "123")
            {
                //asp.net 3.5 ile gelen kimlik doğrulama yaklaşımına claimbased authentication diyoruz.
                //kullanıcıların claimleri yani kullanıcıya ait username,role, arkadaşları, hakkında, aklınıza gelebilecek kullanıcı ile ilgili herşey ClaimIdentity sınıfı içerisine giriyor.
                //biz örneğimizde sadece username ve role claimlerini kullanıcaz
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("username", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                //web context'in ilgili kimliği doğruladığına dair kod
                context.Validated(identity);
            }
            else
            {
                //yanlış yetkilendirme bilgileri geldiğinde apiden gönderilen hata mesajı
                context.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
            }
        }
    }
}