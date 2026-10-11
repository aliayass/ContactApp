using ContactApp.Contracts.DTOs;

namespace ContactApp.Services;

public interface INewsService
{
    Task<IReadOnlyList<NewsArticleDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<NewsArticleDto?> GetByIdAsync(int id, CancellationToken cancellationToken);

}
