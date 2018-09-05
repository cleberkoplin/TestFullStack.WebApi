using System;
using TestFullStack.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using JWT.Algorithms;
using JWT.Serializers;
using JWT;

namespace TestFullStack.Domain.Utils
{
    public static class ApiToken
    {
        public static string Secret { get; set; }

        public static void GenerateSecret()
        {
            var secret = Guid.NewGuid().ToString("N").Substring(0, 20);
            Secret = secret;
        }

        public static string GenerateToken(this Token token)
        {
            if (string.IsNullOrWhiteSpace(Secret))
                throw new Exception("The secret key is absent!");
            
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var tokenString = encoder.Encode(token, Secret);

            return tokenString;
        }

        public static string GetTokenString(this HttpContext context)
        {
            return context.Request.Headers["Token"];
        }

        public static Token GetToken(this HttpContext context)
        {
            try
            {
                var tokenString = context.GetTokenString();

                var serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                var validator = new JwtValidator(serializer, provider);
                var urlEncoder = new JwtBase64UrlEncoder();
                var decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(tokenString, Secret, verify: true);

                return JsonConvert.DeserializeObject<Token>(json);
            }
            catch (SignatureVerificationException)
            {
                Console.Out.WriteLine("Invalid Token!");
            }
            return null;
        }
    }
}
