namespace WeAreFootball.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WeAreFootball.Data.Models;

    public class TagsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tags.Any())
            {
                return;
            }

            await dbContext.Tags.AddAsync(new Tag { Name = "Manchester United" });
            await dbContext.Tags.AddAsync(new Tag { Name = "AC Milan" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Barcelona" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Roma" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Real Madrid" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Juventus" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Manchester City" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Leicester City" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Southampton" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Chelsea" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Tottenham Hotspur" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Sevilla" });
            await dbContext.Tags.AddAsync(new Tag { Name = "AS Monaco" });
            await dbContext.Tags.AddAsync(new Tag { Name = "RB Leipzig" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Napoli" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Paris SG" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Liverpool" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Bayern München" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Borussia Dortmund" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Arsenal" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Everton" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Leeds United" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Atalanta" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Fiorentina" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Inter Milan" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Lazio" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Sampdoria" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Torino" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Lille" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Lyon" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Marsellie" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Atletico Madrid" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Atletic Bilbao" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Celta Vigo" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Valencia CF" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Villareal FC" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Bayer Leverkusen" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Borussia Monchengladbah" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Koln FC" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Eintraht Francfurt" });
            await dbContext.Tags.AddAsync(new Tag { Name = "La Liga" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Serie A" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Bundesliga" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Ligue 1" });
            await dbContext.Tags.AddAsync(new Tag { Name = "English Premier League" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Champions League" });
            await dbContext.Tags.AddAsync(new Tag { Name = "Europa League" });


            await dbContext.SaveChangesAsync();
        }
    }
}
