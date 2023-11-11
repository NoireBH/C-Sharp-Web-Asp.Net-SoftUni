using ForumApp.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Services.Interfaces
{
	public interface IPostService
	{
		Task<IEnumerable<PostListViewModel>> ListAllAsync();

		Task AddPostAsync(PostFormViewModel postViewModel);

		Task<PostFormViewModel> GetForEditOrDeleteByIdAsync(string id);

		Task EditByIdAsync(string id, PostFormViewModel postEditedModel);

		Task DeleteByIdAsync(string id);
	}
}
