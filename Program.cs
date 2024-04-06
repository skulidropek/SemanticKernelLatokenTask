using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelLatokenTask.Plugins;

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(
      "gpt-4-1106-preview",
         Environment.GetEnvironmentVariable("openai-api-key"));

builder.Plugins.AddFromType<MathPlugin>();

var kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

ChatHistory chatMessages = new ChatHistory();

while (true)
{
    System.Console.Write("User > ");

    chatMessages.AddUserMessage(Console.ReadLine()!);

    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        chatMessages,
        executionSettings: new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            Temperature = 0
        },
        kernel: kernel);

    string fullMessage = "";

    await foreach (var content in result)
    {
        if (content.Role.HasValue)
            System.Console.Write("\nAssistant > ");
        
        System.Console.Write(content.Content);
        fullMessage += content.Content;
    }

    System.Console.WriteLine();
    chatMessages.AddAssistantMessage(fullMessage);
}