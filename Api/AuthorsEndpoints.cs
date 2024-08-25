using Core.Entities;
using Core.Constants;
using Core.Interfaces;
using Data;

namespace Endpoints;
public static class AuthorsEndpoints
{
    public static RouteGroupBuilder MapAuthors(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/authors");
        group.WithTags("Authors");
        group.MapGet("/", GetAuthors);
        group.MapGet("/{id:int}", GetAuthorById);
        group.MapGet("/{name}", GetAuthorByName);
        group.MapGet("/search/{name}", GetAllAuthorsByName);
        group.MapPost("/", AddAuthor);
        group.MapPut("/{id:int}", UpdateAuthor);
        group.MapDelete("/{id:int}", DeleteAuthor);
        return group;
    }

    private static async Task<IResult> DeleteAuthor(IUnitOfWork unit, int id)
    {
        var author = await unit.Authors.GetById(id);
        if (author is null) return TypedResults.NotFound();
        await unit.Authors.Delete(id);
        await unit.Commit();
        return TypedResults.NoContent();
    }

    private static async Task<IResult> UpdateAuthor(IUnitOfWork unit, int id, Author input)
    {
        var author = await unit.Authors.GetById(id);
        if (author is null) return TypedResults.NotFound();
        author.Name = input.Name;
        await unit.Commit();
        return TypedResults.NoContent();
    }

    private static async Task<IResult> AddAuthor(IUnitOfWork unit, Author author)
    {
        await unit.Authors.Add(author);
        await unit.Commit();
        return TypedResults.Created($"/authors/{author.Id}", author);
    }

    //inject IBaseRepository<Author>
    public static async Task<IResult> GetAuthors(IUnitOfWork unit)
    {
        return TypedResults.Ok(await unit.Authors.GetAll());
    }

    public static async Task<IResult> GetAuthorById(IUnitOfWork unit, int id)
    {
        return TypedResults.Ok(await unit.Authors.GetById(id));
    }

    public static async Task<IResult> GetAuthorByName(
        IUnitOfWork unit, string name)
    {
        return TypedResults.Ok(await unit.Authors
            .Find(a => a.Name == name, ["Books"]));
    }

    public static async Task<IResult> GetAllAuthorsByName(
        IUnitOfWork unit, string name)
    {
        return TypedResults.Ok(await unit.Authors
            .FindAll(a => a.Name.Contains(name),
            0, 10,
            ["Books"],
            a => a.Name, OrderBy.Ascending));
    }
}
