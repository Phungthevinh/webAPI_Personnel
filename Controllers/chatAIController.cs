using Microsoft.AspNetCore.Mvc;
using System.ClientModel;
using WebAPI.models;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Files;

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
            
            ChatCompletion completion = await _chatClient.CompleteChatAsync(message.Input);
            return Results.Ok(new { response = completion.Content[0].Text });
        }
    }
}
