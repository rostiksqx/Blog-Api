﻿using Blog.Core.Models;
using Blog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Blog.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _dbContext;

    public PostRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Post post, Guid userId)
    {
        var postEntity = new PostEntity
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            UserId = userId
        };
        
        await _dbContext.Posts.AddAsync(postEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Post> Get(Guid id)
    {
        var postEntity = await _dbContext.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found.");
        
        return new Post(postEntity.Id, postEntity.Title, postEntity.Content, postEntity.UserId);
    }
    
    public async Task<IEnumerable<Post>> GetAll()
    {
        var postEntities = await _dbContext.Posts
            .AsNoTracking()
            .ToListAsync();
        
        var posts = postEntities
            .Select(x => new Post(x.Id, x.Title, x.Content, x.UserId));
        
        return posts;
    }

    public async Task Delete(Guid id)
    {
        var postEntity = await _dbContext.Posts
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found.");
        
        _dbContext.Posts.Remove(postEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Post> Update(Guid id, string title, string content)
    {
        var postEntity = await _dbContext.Posts
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found.");
        
        postEntity.Title = title;
        postEntity.Content = content;
        
        await _dbContext.SaveChangesAsync();
        
        return new Post(postEntity.Id, postEntity.Title, postEntity.Content, postEntity.UserId);
    }
}