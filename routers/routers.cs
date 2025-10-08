using Npgsql;
using System;
using WebAPI.models;


namespace WebAPI.routers
{
    public class routers
    {
        public routers(WebApplication app, NpgsqlDataSource db, string keyJWT, string Issuer, string Audience) {
            UsersRouter users = new UsersRouter(app, keyJWT, Issuer, Audience);
            KOL_ProfilesRouter KOI_Profiles = new KOL_ProfilesRouter(app);
            Discount_CodesRouter discount_CodesRouter = new Discount_CodesRouter(app);
            chatAIRouter ChatAIRouter = new chatAIRouter(app);
            RolesRouter rolesRouter = new RolesRouter(app);
            PermissionsRouter permissionsRouter = new PermissionsRouter(app);
        }
    }
}
