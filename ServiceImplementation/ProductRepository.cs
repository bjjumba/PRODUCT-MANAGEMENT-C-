using Dapper;
using Microsoft.Extensions.Configuration;
using ProductManagement.Application;
using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public  class ProductRepository : IProductRepository
    {   private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Product entity)
        {
           
            String sql = "Insert into Products ([Name],Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"))) {
                connection.Open();
                entity.AddedOn = DateTime.Now;
                
                //var AddProduct = new SqlCommand();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;

            }
                ///throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                string sql = "DELETE FROM Products  WHERE Id = @Id";
                var result = await connection.ExecuteAsync(sql, new {Id=id });
                return result;

            }
          

        }
        //read only async
        public async Task<IReadOnlyList<Product>> GetAllAsync() {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"))) {
                connection.Open();
                string sql = "Select * From Products";
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id) {
            using (var connection= new SqlConnection(_configuration.GetConnectionString("SqlConnection"))) {
                connection.Open();
                string sql = "Select * From Products Where Id = @Id";
                var result = await connection.QueryFirstOrDefaultAsync<Product>(sql, new {Id =id});
                return result;
            }
        }
        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE [Products] SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

       
    }
}
