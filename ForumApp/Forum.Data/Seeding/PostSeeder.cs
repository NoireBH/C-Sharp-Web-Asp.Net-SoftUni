using ForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Data.Seeding
{
	public class PostSeeder
	{
		public Post[] GeneratePosts()
		{
			ICollection<Post> posts = new HashSet<Post>();
			Post currentPost;

			currentPost = new Post()
			{
				Title = "my first post",
				Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer facilisis nisi non elementum vehicula. Nunc scelerisque erat urna, at mattis."
			};

			posts.Add(currentPost);

			currentPost = new Post()
			{
				Title = "my second post",
				Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra erat ante, ac finibus massa sodales vehicula. Ut iaculis tortor quis imperdiet imperdiet. In euismod."
			};

			posts.Add(currentPost);

			currentPost = new Post()
			{
				Title = "my third post",
				Content = "Translations: Can you help translate this site into a foreign language ? Please email us with details if you can help."
			};

			posts.Add(currentPost);

			currentPost = new Post()
			{
				Title = "my fourth post",
				Content = "Section 1.10.32 of \"de Finibus Bonorum et Malorum\", written by Cicero in 45 BC\r\n\"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. "
			};

			posts.Add(currentPost);

			return posts.ToArray();
		}
	}
}
