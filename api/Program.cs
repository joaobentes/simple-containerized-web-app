using Microsoft.EntityFrameworkCore;
using BookApi;

/*
  TODO: 
    - Do the server up time with web sockets
    - Structure the project a bit better. Organize the project better. Too many things in the main file.
*/

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(document =>
{
  document.Title = "Books API";
  document.Version = "v1";
});

// Add CORS services
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod();
  });
});

var app = builder.Build();

// Ensure the database is created and add initial data
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<BookDb>();
  db.Database.EnsureCreated();

  if (!db.Books.Any())
  {

    db.Books.Add(new Book { Title = "La casa de papel" });
    db.Books.Add(new Book { Title = "The fall of the house of Usher" });
    db.Books.Add(new Book { Title = "Wuthering Heights" });
    db.Books.Add(new Book { Title = "The Gambler" });

    db.SaveChanges();
  }
}

app.UseCors();

if (app.Environment.IsDevelopment())
{
  app.UseOpenApi();
  app.UseSwaggerUi(config =>
  {
    config.DocumentTitle = "Books API";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
  });
}
app.MapGet("/health", () => Results.Ok("Server is running"));

app.MapGet("/book", async (BookDb db) =>
{
  return Results.Ok(await db.Books.ToListAsync());
});

app.MapGet("/book/{id}", async (int id, BookDb db) =>
{
  var book = await db.Books.FindAsync(id);
  if (book == null)
  {
    return Results.NotFound();
  }
  return Results.Ok(book);
});

app.MapPut("/book/{id}", async (int id, Book book, BookDb db) =>
{
  var existingBook = await db.Books.FindAsync(id);

  if (existingBook == null)
  {
    return Results.NotFound();
  }

  if (id != book.Id)
  {
    return Results.BadRequest();
  }

  existingBook = book;

  db.Entry(existingBook).State = EntityState.Modified;
  await db.SaveChangesAsync();

  return Results.Ok();
});

app.MapPost("/book", async (Book book, BookDb db) =>
{
  db.Books.Add(book);
  await db.SaveChangesAsync();
  return Results.Created($"/book/{book.Id}", book);
});

app.MapDelete("/book/{id}", async (int id, BookDb db) =>
{
  var book = await db.Books.FindAsync(id);
  if (book == null)
  {
    return Results.NotFound();
  }
  db.Books.Remove(book);
  await db.SaveChangesAsync();
  return Results.Ok();
});

app.Run();