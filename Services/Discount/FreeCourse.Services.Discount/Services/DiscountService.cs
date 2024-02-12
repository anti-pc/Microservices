using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * From discount Where id=@Id", new { id })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount Not Found", statusCode: 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Insert Into discount(userid,rate,code) Values(@UserId,@Rate,@Code)", discount);

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An Error Occured While Adding", 500);

        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Update discount Set userid=@UserId,rate=@Rate,code=@Code Where id=@Id",
                new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code });

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount Not Found", statusCode: 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("Delete From discount Where id=@Id", new { Id=id});

            if(status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount Not Found", statusCode: 404);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount Where userid=@UserId and code=@Code",
                new { Code = code, UserId = userId });

            var hasDiscount = discount.FirstOrDefault();

            if (hasDiscount == null)
                return Response<Models.Discount>.Fail("Discount Not Found", statusCode: 404);

            return Response<Models.Discount>.Success(hasDiscount, 200);
        }
    }
}
