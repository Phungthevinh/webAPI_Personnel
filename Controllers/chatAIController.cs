
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Files;
using System.ClientModel;
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
            _dbContext.Add(new ai_prompts
            {
                created_by_user_id = promts.created_by_user_id,
                prompt_name = promts.prompt_name,
                category = promts.category,
                prompt_text = promts.prompt_text,

            });
            await _dbContext.SaveChangesAsync();
            return Results.Ok(200);
        }
        public async Task<IResult> chatvoiAIGPT([FromBody] chatAI message)
        {
            string systemPrompt = @"
            Bạn là một trợ lý ảo tên là Nguyễn Thị Anh Thư, bạn chỉ được phép trả lời dựa trên 'BỘ QUY TRÌNH' được cung cấp dưới đây.
            Nhiệm vụ của bạn là trả lời các câu hỏi của người dùng và chỉ dựa vào nội dung trong bộ tài liệu này.
            Nếu thông tin không có trong tài liệu, hãy trả lời chính xác là: 'Thông tin này tôi không nắm rõ!'
            Nghiêm cấm tuyệt đối việc sử dụng kiến thức bên ngoài hoặc tự suy diễn.
            lưu ý chỉ đưa ra tối đa 5 gạch đầu dòng cho các ý";
            
                var chatMessages = new List<ChatMessage>() {
                ChatMessage.CreateSystemMessage(systemPrompt),
                ChatMessage.CreateUserMessage(message.Input)
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages: chatMessages, new ChatCompletionOptions
            {
                Temperature = 0
            });
            foreach (var msg in chatMessages)
            {
                Console.WriteLine(msg.Content[0].Text);
            }

            return Results.Ok(new { response = completion.Content[0].Text });
        }
    }
}
