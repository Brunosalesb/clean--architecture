using Application.Articles.GetArticleById;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries;
public sealed class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleResponse?>
{
    private readonly ApplicationDbContext _dbContext;

    public GetArticleByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleResponse?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var articleResponse = await _dbContext
            .Articles
            .AsNoTracking()
            .Where(article => article.Id == request.Id)
            .Select(article => new ArticleResponse
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Tags = article.Tags,
                CreatedOnUtc = article.CreatedOnUtc,
                PublishedOnUtc = article.PublishedOnUtc
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return articleResponse;
    }
}
