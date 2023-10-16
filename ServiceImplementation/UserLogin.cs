using Dapper;
using Microsoft.Extensions.Configuration;
using ProductManagement.Application;
using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    internal class UserLogin : IUserLoginService
    {
        static readonly HttpClient client = new HttpClient();
        public readonly IConfiguration _configuration;
        public UserLogin(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public async Task<UserAuth> GetCredentialAsync (string userName, string password)
        {
            using(var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"))) { 
                connection.Open();
                string sql = "Select UserName,Password  From Login Where UserName = @UserName and Password = @Password";
                var result = await connection.QuerySingleOrDefaultAsync<UserAuth>(sql, new { UserName = userName, Password = password });
                
                if (result != null)
                {
                    return (UserAuth)result;
                }else
                {
                    return null;
                }
               



            }
          
        }

       public async  Task<int> AddUserAsync(UserAuth userAuth)
        {


           
            string UserName = userAuth.UserName;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"))) { 
                connection.Open();
                string sql1 = "Select UserName from Login where UserName=@UserName";
                var userNameCheck= await connection.QuerySingleOrDefaultAsync<UserAuth>(sql1, new { UserName = userAuth.UserName });
                if (userNameCheck != null)
                {
                    return 0;
                }
                else {
                    
                        using HttpResponseMessage response = await client.GetAsync($"http://apilayer.net/api/validate?access_key=a8611d9605a89d333b58eea694cd18f4&number={userAuth.PhoneNumber}&country_code={userAuth.CountryCode}&format=1");

                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        Console.WriteLine(responseBody);
                       if(responseBody.Contains("false"))
                        {
                            return 2;
                        }else
                        {
                            string sql = "Insert into Login (UserName,Password,PhoneNumber,CountryCode) VALUES (@UserName,@Password,@PhoneNumber,@CountryCode)";

                            var result = await connection.ExecuteAsync(sql, userAuth);
                            return result;

                        }
                    
                   
                 
                }
                
            }
           // throw new NotImplementedException();
        }

        
    }
}
