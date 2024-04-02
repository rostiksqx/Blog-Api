﻿using AuthCookies.Core.Models;
using AuthCookies.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthCookies.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _dbContext;

    public PostRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Post post)
    {
        var postEntity = new PostEntity
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content
        };
        
        await _dbContext.Posts.AddAsync(postEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Post> Get(Guid id)
    {
        var postEntity = await _dbContext.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found.");
        
        return new Post(postEntity.Id, postEntity.Title, postEntity.Content);
    }
    
    public async Task<IEnumerable<Post>> GetAll()
    {
        var postEntities = await _dbContext.Posts
            .AsNoTracking()
            .ToListAsync();
        
        return postEntities.Select(x => new Post(x.Id, x.Title, x.Content));
    }
}