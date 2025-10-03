using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class PermissionsController
    {
        private readonly dbContext _dbContext;
        public PermissionsController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // thêm quyền hạn 
        public async Task<IResult> themQuyenHan(Permissions permissions)
        {
            try
            {
                _dbContext.Add(new Permissions
                {
                    name = permissions.name,
                    description = permissions.description
                });
                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.InnerException?.Message);
            }
        }

        //xem toàn bộ quyền hạn
        public async Task<IResult> xemquyenhan()
        {
            try
            {
                var permission = await _dbContext.permissions
                    .OrderBy(p => p.id)
                    .ToListAsync();
                return Results.Ok(new {data = permission });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex?.InnerException?.Message);
            }
        }

        //xóa quyền hạn
        public async Task<IResult> xoaQuyenHan(int id)
        {
            try
            {
                var blog = await _dbContext.permissions
                    .Where(p => p.id == id)
                    .FirstAsync();

                _dbContext.Remove(blog);
                await _dbContext.SaveChangesAsync();

                return Results.Ok(200);
            }
            catch (Exception ex) { return Results.BadRequest(ex?.InnerException?.Message); }
        }

        //sửa quyền hạn
        public async Task<IResult> suaquyenhan(Permissions permissions)
        {
            try
            {
                var permission = await _dbContext.permissions
                    .Where(p => p.id == permissions.id)
                    .FirstAsync();

                permission.name = permissions.name;
                permission.description = permissions.description;
                await _dbContext.SaveChangesAsync();

                return Results.Ok(200);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
