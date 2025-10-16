using Microsoft.EntityFrameworkCore;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class campaignsController
    {
        dbContext _dbContext;
        public campaignsController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //tạo mới sự kiện
        public async Task<IResult> taoSuKienDeCTVacpMa(campaigns campaigns)
        {
            try
            {
                _dbContext.Add(new campaigns
                {
                    name = campaigns.name,
                    description = campaigns.description,
                    start_date = campaigns.start_date,
                    end_date = campaigns.end_date,
                    discount_type = campaigns.discount_type,
                    discount_value = campaigns.discount_value,
                    commission_type = campaigns.commission_type,
                    commission_value = campaigns.commission_value,
                });
                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);
            }catch (Exception ex)
            {
                return Results.BadRequest(ex.InnerException?.Message);
            }
            
        }

        //xem tất cả sự kiện
        public async Task<IResult> xemtatcasukien()
        {
            try
            {
                var xemsukien = await _dbContext.campaigns
                    .OrderBy(c => c.id)
                    .ToListAsync();

                return Results.Ok(new {xemsukien});
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        //xem chi tiết sự kiện
        public async Task<IResult> xemchitietsukien(int id)
        {
            try
            {
                var xemchitietsukien = from db in _dbContext.campaigns
                                       where db.id == id
                                       select db;
                return Results.Ok(new {xemchitietsukien});
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
