using Microsoft.AspNetCore.Authorization;
using Npgsql;
using OpenAI.Chat;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class chatAIRouter
    {
        public chatAIRouter(WebApplication app)
        {
            //thêm promts vào 
            app.MapPost("/them-promts", (ai_prompts promts, dbContext db) =>
            {
                chatAIController themmoi = new chatAIController(db);
                return themmoi.themPromts(promts);
            });

            //hỏi câu trả lời với chat
            app.MapPost("/chatAI", [Authorize] (chatAI chatai, ChatClient chatClient, dbContext db) =>
            {
               chatAIController chatAI = new chatAIController(chatClient, db);
               return chatAI.chatvoiAIGPT(chatai);
            });
        }
    }
}
