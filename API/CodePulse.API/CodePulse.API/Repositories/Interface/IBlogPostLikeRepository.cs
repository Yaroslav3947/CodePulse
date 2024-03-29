﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IBlogPostLikeRepository {
        Task<BlogPostLike> AddLikeForBlogPost(BlogPostLike blogPostLike);

        Task<BlogPostLike> RemoveLikeForBlogPost(BlogPostLike blogPostLike);
        Task<IEnumerable<Guid>> GetUsersLikingBlogPostByIdAsync(Guid blogPostId);
        Task DeleteAsync(Guid blogPostId);
    }
}
