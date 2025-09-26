using Npgsql;
using OpenAI.Chat;
using WebAPI.Controllers;
using WebAPI.models;

namespace WebAPI.routers
{
    public class chatAIRouter
    {
        public chatAIRouter(WebApplication app, NpgsqlDataSource db)
        {
            app.MapPost("/chatAI", (chatAI chatai, ChatClient chatClient) =>
            {
               chatAIController chatAI = new chatAIController(chatClient);
               return chatAI.chatvoiAIGPT(chatai);
            });
        }
    }
}
