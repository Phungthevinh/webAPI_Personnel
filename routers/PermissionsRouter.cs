using Microsoft.AspNetCore.Authorization;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class PermissionsRouter
    {
        public PermissionsRouter(WebApplication app)
        {
            //them mới quyền hạn
            app.MapPost("/them-moi-quyen-han", [Authorize] (Permissions permissions, dbContext dbContext) =>
            {
                PermissionsController themmoiquyen = new PermissionsController(dbContext);
                return themmoiquyen.themQuyenHan(permissions);
            });

            //xem tất cả quyền hạn
            app.MapGet("/xem-tat-ca-quyen-han", [Authorize] (dbContext dbContext) =>
            {
                PermissionsController xemTatCaQuyenHan = new PermissionsController(dbContext);
                return xemTatCaQuyenHan.xemquyenhan();
            });

            //xóa quyền hạn
            app.MapDelete("/xoa-quyen-han", [Authorize] (dbContext dbContext, int id) =>
            {
                PermissionsController xoaquyenhan = new PermissionsController(dbContext);
                return xoaquyenhan.xoaQuyenHan(id);
            });

            //sửa quyền hạn
            app.MapPatch("/sua-quyen-han", [Authorize] (Permissions permissions, dbContext dbContext) =>
            {
                PermissionsController suaquyenhan = new PermissionsController(dbContext);
                return suaquyenhan.suaquyenhan(permissions);
            });
        }
    }
}
