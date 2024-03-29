﻿using Movies.Repository.Entities;
using Movies.Repository.Repositories;
using NUnit.Framework;

namespace Movies.Repository.Tests.Rating
{
    public class RatingRepositoryBase: TestRepositoryBase
    {
        [SetUp]
        protected new void Setup()
        {
            base.Setup();
        }

        protected RatingRepository GetRepository()
        {
            return new RatingRepository(GetUnitOfWork());
        }

        protected void InsertRating(MovieRating rating)
        {
            using (var context = GetContext())
            {
                context.Add(rating);
                context.SaveChanges();
            }
        }

    }
}
