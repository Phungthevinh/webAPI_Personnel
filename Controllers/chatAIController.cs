
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Files;
using System.ClientModel;
using WebAPI.models;

namespace WebAPI.Controllers
{
    public class chatAIController
    {
        private readonly ChatClient _chatClient;

        public chatAIController(ChatClient chatClient)
        {
            _chatClient = chatClient;
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
