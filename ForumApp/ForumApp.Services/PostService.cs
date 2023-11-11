using ForumApp.ViewModels.Post;
using ForumApp.Models;
using ForumApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumApp.Data.Models;

namespace ForumApp.Services
{
	public class PostService : IPostService
	{
		private readonly ForumAppDbContext dbContext;

		public PostService(ForumAppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddPostAsync(PostFormViewModel postViewModel)
		{
			var newPost = new Post()
			{
				Content = postViewModel.Content,
				Title = postViewModel.Title,
			};

			await dbContext.AddAsync(newPost);
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(string id)
		{
			Post postToDelete = await dbContext
				.Posts
				.FirstAsync(p => p.Id.ToString() == id);

			dbContext.Posts.Remove(postToDelete);
			await dbContext.SaveChangesAsync();
		}

		public async Task EditByIdAsync(string id, PostFormViewModel postEditedModel)
		{
            Post postToEdit = await dbContext
                .Posts
                .FirstAsync(p => p.Id.ToString() == id);

            postToEdit.Title = postEditedModel.Title;
            postToEdit.Content = postEditedModel.Content;

            await dbContext.SaveChangesAsync();
        }

		public async Task<PostFormViewModel> GetForEditOrDeleteByIdAsync(string id)
		{
            Post postToEdit = await dbContext
                .Posts
                .FirstAsync(p => p.Id.ToString() == id);

            return new PostFormViewModel()
            {
                Title = postToEdit.Title,
                Content = postToEdit.Content
            };

        }

		public async Task<IEnumerable<PostListViewModel>> ListAllAsync()
		{
			IEnumerable<PostListViewModel> allPosts = await dbContext
				.Posts
				.Select(p => new PostListViewModel()
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					Content = p.Content
				})
				.ToArrayAsync();
			return allPosts;
		}
	}
}
