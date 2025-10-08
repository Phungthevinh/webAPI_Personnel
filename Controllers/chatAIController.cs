
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Files;
using System.ClientModel;
using System.Reflection.Metadata;
using System.Text.Json;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class chatAIController
    {
        private readonly ChatClient _chatClient;
        private readonly dbContext _dbContext;

        public chatAIController(ChatClient chatClient, dbContext dbContext)
        {
            _chatClient = chatClient;
            _dbContext = dbContext;
        }
        public chatAIController( dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //lưu promts vào database theo các chức năng khác nhau của promts và theo ngời dùng lưu vào
        public async Task<IResult> themPromts(ai_prompts promts)
        {
            DateTime now = DateTime.UtcNow;
            _dbContext.Add(new ai_prompts
            {
                created_by_user_id = promts.created_by_user_id,
                prompt_name = promts.prompt_name,
                category = promts.category,
                prompt_text = promts.prompt_text,
                created_at = now,

            });
            await _dbContext.SaveChangesAsync();
            return Results.Ok(200);
        }

        //lấy ra tất cả các promt đã tạo
        public async Task<IResult> tatcapromts(int page, int limit)
        {
            try
            {
                var promt = await _dbContext.ai_prompts
                .OrderBy(AI => AI.id)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
                var promtAI = JsonSerializer.Serialize(promt);
                int totalPromts = await _dbContext.ai_prompts.CountAsync();

                return Results.Ok(new { promtAI, totalPromts });
            }
            catch(Exception e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        //xóa promts đã tạo
        public async Task<IResult> xoaPromts( long id)
        {
            try
            {
                var promts = _dbContext.ai_prompts.Find(id);
                if (promts != null)
                {
                    _dbContext.ai_prompts.Remove(promts);
                    _dbContext.SaveChanges();
                }

                return Results.Ok(200);
            }catch(Exception e)
            {
                return Results.BadRequest(new { e.Message });
            }
        }

        //chỉnh sửa promts
        public async Task<IResult> capNhapPromt (chinh_sua_promts prompts)
        {
            try
            {

                var updatePromts = await _dbContext.ai_prompts
                    .Where(ai => ai.id == prompts.id)
                    .FirstOrDefaultAsync();
                if(updatePromts != null)
                {
                    updatePromts.prompt_text = prompts.prompt_text;
                    updatePromts.prompt_name = prompts.prompt_name;
                    updatePromts.category = prompts.category;
                    await _dbContext.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound(new {err = "id truyền không hợp lệ"});

            }catch(Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }
        //chatAI với AI
        public async Task<IResult> chatvoiAIGPT([FromBody] chatAI message)
        {
            var ai_promt = await _dbContext.ai_prompts
                .Where(ai => ai.id == 4)
                .FirstOrDefaultAsync();
            string systemPrompt = @"
            Bạn là một trợ lý ảo tên là DT-BOT, bạn chỉ được phép trả lời dựa trên 'BỘ QUY TRÌNH' được cung cấp dưới đây.
            Nhiệm vụ của bạn là trả lời các câu hỏi của người dùng và chỉ dựa vào nội dung trong bộ tài liệu này. bạn có thể sáng tạo nhưng chỉ cho phép sáng tạo giới hạn trong 10% khả năng của bạn, chủ yếu cần tập trung vào tài liệu
            Nếu thông tin không có trong tài liệu, hãy trả lời chính xác là: 'Thông tin này tôi không nắm rõ!'
            những câu hỏi bên ngoài tài liệu, bạn có thể trả lời, nhưng nếu là câu hỏi bên trong tài liệu, tuyệt đối bạn ko được suy diễn, hoặc đề cập sai dữ liệu trong tài liệu.
            lưu ý chỉ đưa ra tối đa 5 gạch đầu dòng cho các ý";
            
                var chatMessages = new List<ChatMessage>() {
                ChatMessage.CreateSystemMessage(systemPrompt),
                ChatMessage.CreateSystemMessage(ai_promt.prompt_text),
                ChatMessage.CreateUserMessage(message.Input)
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages: chatMessages, new ChatCompletionOptions
            {
                Temperature = 0
            });
            

            return Results.Ok(new { response = completion.Content[0].Text });
        }
    }
}
