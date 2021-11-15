using Axon.Application.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Axon.Infrastructure.Authentication
{
    public static class JwtServiceCollectionExtention
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            JwtConfigurations _jwtConfigurations = Configuration.GetSection("TokenConfigurations").Get<JwtConfigurations>();
            services.AddSingleton(_jwtConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => 
            {
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.Secret)),
                    ValidIssuers = _jwtConfigurations.ValidIssuers,
                    ValidAudiences = _jwtConfigurations.ValidAudiences,
                    ClockSkew = TimeSpan.Zero
                };

                bearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.Headers.Add("Unauthorized", "True");
                        context.Response.StatusCode = 401;

                        switch (context.Exception)
                        {
                            case SecurityTokenExpiredException e:
                                context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("Provided Token is Expired")));
                                break;

                            case SecurityTokenInvalidAudienceException e:
                                context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("The Audience of the provided Token is not valid for this resource")));
                                break;

                            case SecurityTokenInvalidIssuerException e:
                                context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("The Issuer of the provided Token is not valid for this resource")));
                                break;

                            default:
                                context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("The request has not been applied because it lacks valid authentication credentials for the target resource")));
                                break;
                        }

                        
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        if (!context.Response.HasStarted)
                        {
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = 401;
                            context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("The request has not been applied because it lacks valid authentication credentials for the target resource")));
                        }
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 403;
                        context.Response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>("You do not have permission to acess the requested resource")));
                        return Task.CompletedTask;
                    },
                };
            });

          
            return services;
        }
    }
}