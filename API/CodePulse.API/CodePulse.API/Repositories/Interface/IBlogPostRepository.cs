﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IBlogPostRepository {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}