using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            //lấy ra tất cả các promts
            app.MapGet("/promts", (dbContext db, int page, int limit) =>
            {
                chatAIController layRaTatCaPromts = new chatAIController(db);
                return layRaTatCaPromts.tatcapromts(page, limit);
            });

            //chỉnh sửa promts
            app.MapPatch("/chinh-sua-promts", (dbContext db, chinh_sua_promts promts) =>
            {
                chatAIController chinh_sua_promts = new chatAIController(db);
                return chinh_sua_promts.capNhapPromt(promts);
            });

            //xóa promts
            app.MapDelete("/xoa-promts", ( long id, dbContext db) =>
            {
                chatAIController xoapromts = new chatAIController(db);
                return xoapromts.xoaPromts(id);
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
