using Core.Entities;
using Core.Interfaces;

namespace Endpoints;
public static class BooksEndpoints
{
    public static RouteGroupBuilder MapBooks(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/books").WithTags("Books");
        group.MapGet("/", GetBooks);
        return group;
    }

    public static async Task<IResult> GetBooks(IUnitOfWork unit)
    {
        return TypedResults.Ok(await unit.Books.GetAll());
    }
}