using PublisherData;
using PublisherDomain;

/*using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}*/

// AJOUTER UN AUTEUR
/*AddAuthor("Anthony", "Caron");
AddAuthor("Magalie", "Sassi");
AddAuthor("Merzak", "Naili");
AddAuthor("Marilyne", "Naili");
AddAuthor("Michele", "Charles");
AddAuthor("Aylan", "Dupont");*/
void AddAuthor(string firstName, string lastName)
{
    var author = new Author { FirstName = firstName, LastName = lastName };
    // firstname et lastname sere exécuter ç la fin de la requête
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

// RECUPERER LA LISTE DES AUTEURS
GetAuthors();
void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
  
    foreach (var item in authors.Skip(0).Take(3))
    {
        Console.WriteLine($"AUTEUR {item.Id} : {item.LastName} {item.FirstName} ");
    }
}
