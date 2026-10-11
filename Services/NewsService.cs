using ContactApp.Contracts.DTOs;
using System.Net;

namespace ContactApp.Services;

public class NewsService : INewsService
{
    private readonly HttpClient _htttpClient;
    private readonly ILogger<NewsService> _logger;

    public NewsService(HttpClient htttpClient, ILogger<NewsService> logger)
    {
        _htttpClient = htttpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<NewsArticleDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var list = await _htttpClient
                .GetFromJsonAsync<List<NewsArticleDto>>("api/NewsArticles", cancellationToken);
            return list ?? new List<NewsArticleDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "API isteği başarısız oldu");
            return Array.Empty<NewsArticleDto>();
        }
    }

    public async Task<NewsArticleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _htttpClient
                .GetAsync($"api/NewsArticles/{id}", cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) //404
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NewsArticleDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Id: {id} ilgili haber okunamadı. ");
            return null;
        }
    }
}
