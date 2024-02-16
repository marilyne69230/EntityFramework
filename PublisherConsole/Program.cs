using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System;
using System.Runtime.CompilerServices;

/*using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}*/

// AJOUTER UN AUTEUR
/*AddAuthor("Anthony", "Caron");
AddAuthor("Magalie", "Sassi");
AddAuthor("Merzak", "Naili");
AddAuthor("Marilyne", "Naili");
AddAuthor("Michele", "Charles");*/
AddAuthor("Aylan", "Dupont");
void AddAuthor(string firstName, string lastName)
{
    var author = new Author { FirstName = firstName, LastName = lastName };
    // firstname et lastname sere exécuter ç la fin de la requête
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

// AJOUTER UN AUTEUR ET LES LIVRES QUI LUI SONT ASSOCIES
//AddAuthorWithBooks();
void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Pablo", LastName = "Coelho" };
    author.Books.Add(new Book { Title = "L'alchimiste", BasePrice = 20.50m, PublishDate = new DateOnly(1988, 1, 1) });
    author.Books.Add(new Book { Title = "Le livre de la jungle", BasePrice = 11.20m, PublishDate = new DateOnly(1999, 1, 1) });

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

// AJOUTER DES LIVRES A UN AUTEUR QUI EXISTE DEJA
//AddBookForExistingAuthor();
void AddBookForExistingAuthor()
{
    var book = new Book { Title = "Bonjour", BasePrice = 20.50m, PublishDate = new DateOnly(1988, 1, 1), AuthorId = 2 };

    using var context = new PubContext();
    context.Books.Add(book);
    context.SaveChanges();

}


// AFFICHER NOM DES AUTEURS AVEC SES LIVRES
/*GetAllAuthorsWithBooks();*/
void GetAllAuthorsWithBooks()
{
    using var context = new PubContext();
    var authorsWithBooks = context.Authors.Include(a => a.Books).ToList();

    foreach (var author in authorsWithBooks)
    {
        Console.WriteLine($"Auteur : {author.FirstName} {author.LastName}");
        Console.WriteLine();
        foreach (var book in author.Books)
        {
            Console.WriteLine($" \t Livre :{book.Title}");
        }
    }
}

// RECUPERER LA LISTE DES AUTEURS
//GetAuthors();
void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();

    foreach (var item in authors)
    {
        Console.WriteLine($"AUTEUR {item.AuthorId} : {item.LastName} {item.FirstName} ");
    }
}

// MODIFIER UN PRENOM DE PLUSIEURS AUTEUR AVEC LE MEME NOM
//UpdateAuthorNames("Caron", "Caronne");

void UpdateAuthorNames(string authorName, string newAuthorName)
{
    using var context = new PubContext();
    var authorsToUpdate = context.Authors.Where(a => a.LastName == authorName).ToList();
    foreach (var author in authorsToUpdate)
    {
        author.LastName = newAuthorName;
    }
    Console.WriteLine("Avant l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("Après l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.SaveChanges();
    Console.WriteLine("Après l'appel de la méthode SaveChanges() : \n" + context.ChangeTracker.DebugView.ShortView);
}

// MODIFIER UN PRENOM D'UN AUTEUR AVEC LE TRACKING
//UpdateAuthorNameWithoutTracking("Caron", "Caronne");

void UpdateAuthorNameWithoutTracking(string authorName, string newAuthorName)
{
    using var context = new PubContext();
    var authorsToUpdate = context.Authors.Where(a => a.LastName == authorName).AsNoTracking().ToList();
    foreach (var author in authorsToUpdate)
    {
        author.LastName = newAuthorName;
        //context.Update(author);
    }
    Console.WriteLine("Avant l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("Après l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.UpdateRange(authorsToUpdate);
    Console.WriteLine("Après l'appel de la méthode SaveChanges() : \n" + context.ChangeTracker.DebugView.ShortView);
}

// MODIFIER LE NOM D'UN SEUL AUTEUR
//UpdateAuthorName("Naili", "Rolando");

void UpdateAuthorName(string authorName, string newAuthorName)
{
    using var context = new PubContext();
    var authorToUpdate = context.Authors.FirstOrDefault(a => a.LastName == authorName);
    if (authorToUpdate != null)
    {
        authorToUpdate.LastName = newAuthorName;
        context.SaveChanges();
        Console.WriteLine($"La modifcation a bien été effectué : {authorName} --> {newAuthorName}");
    }
    else
    {
        Console.WriteLine($"Aucun nom {authorName} n'a été trouvé");
    }
}

// INSERTION ET MODIFICATION EN 1
//VariousOperations();
void VariousOperations()
{
    using var context = new PubContext();
    var author = context.Authors.Find(2);
    author.LastName = "Aylan";
    var newAuthor = new Author { LastName = "Monoroe", FirstName = "Marilyne" };
    context.Authors.Add(newAuthor);
    context.SaveChanges();
}

// SUPPRIMER UN AUTEUR 
DeleteAnAuthor(2);
void DeleteAnAuthor(int authorId)
{
    using var context = new PubContext();

    var author = context.Authors.Find(authorId);

    if (author != null)
    {
        context.Authors.Remove(author);
        context.SaveChanges();
        Console.WriteLine("L'auteur a été supprimé");
    }
    else
    {
        Console.WriteLine("Aucun auteur avec cet ID n'a été trouvé !");
    }
}



// FAIRE UNE PAGINATION SUR LA LISTE DES AUTEURS
//GetAuthorsWithPagination(3);
void GetAuthorsWithPagination(int numberPerPage)
{
    using var context = new PubContext();
    int numberOfAuthors = context.Authors.Count();
    int numberOfAuthorsPerPage = (numberPerPage <= 0) ? 1 : numberPerPage;

    for (int i = 0; i < Math.Ceiling((decimal)numberOfAuthors / numberOfAuthorsPerPage); i++)
    {
        var authors = context.Authors.Skip(numberOfAuthorsPerPage).Take(numberPerPage).ToList();
        Console.WriteLine("Page" + (i + 1));

        foreach (var author in authors)
        {
            Console.WriteLine($"Auteur {author.AuthorId} : {author.FirstName} {author.LastName}");
        }
    }
}

