using Npgsql;
using System;


namespace WebAPI.routers
{
    public class routers
    {
        public routers(WebApplication app, NpgsqlDataSource db, string keyJWT, string Issuer, string Audience) {
            UsersRouter users = new UsersRouter(app, keyJWT, Issuer, Audience);
            //KOL_ProfilesRouter KOI_Profiles = new KOL_ProfilesRouter(app, db);
            Discount_CodesRouter discount_CodesRouter = new Discount_CodesRouter(app, db);
            chatAIRouter ChatAIRouter = new chatAIRouter(app);
        }
    }
}
