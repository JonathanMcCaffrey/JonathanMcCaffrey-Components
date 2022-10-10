using Markdig;
using Microsoft.AspNetCore.Components;

namespace Components;

public partial class MarkdownComponent : ComponentBase
{
    private string? _content;

    [Inject] protected HttpClient HttpClient { get; set; } = default!;

    [Parameter] public string FilePath { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _content = await LoadMarkdown(HttpClient, FilePath);
    }

    private static readonly MarkdownPipeline Pipeline
        = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .UseAdvancedExtensions()
            .Build();

    private static async Task<string> LoadMarkdown(HttpClient httpClient, string filepath)
    {
        return await httpClient.GetStringAsync(filepath);
    }
}