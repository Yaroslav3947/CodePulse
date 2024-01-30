namespace CodePulse.API.Repositories.Interface {
    public interface IBlogPostLikeRepository {
        Task<int> GetTotalLikes(Guid blogPostId);
    }
}
