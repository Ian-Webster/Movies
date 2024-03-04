using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Movies.Domain.Enums;
using dto = Movies.Domain.DTO;
using repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.DataGeneration;

public static class MovieDataGeneration
{
    public static List<repo.Movie> GetRandomMovies(int count)
    {
        return new Faker<repo.Movie>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Title, f => f.Lorem.Word())
            .RuleFor(m => m.YearOfRelease, f => f.Random.Short(1900, 2022))
            .RuleFor(m => m.RunningTime, f => f.Random.Byte(60, 180))
            .RuleFor(m => m.GenreId, f => (byte)f.PickRandom<Genres>())
            .RuleFor(m => m.AverageRating, f => f.Random.Decimal(0, 5))
            .Generate(count);
    }

    public static repo.Movie GetRandomMovie()
    {
        return GetRandomMovies(1).First();
    }
}