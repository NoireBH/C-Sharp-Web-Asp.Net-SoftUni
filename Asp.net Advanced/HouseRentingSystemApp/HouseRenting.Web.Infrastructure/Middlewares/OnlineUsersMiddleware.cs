using HouseRenting.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace HouseRenting.Web.Infrastructure.Middlewares
{
	using static Common.GeneralConstants;
	public class OnlineUsersMiddleware
	{
		private readonly RequestDelegate next;
		private readonly string cookieName;
		private readonly int lastActivityInMinutes;

		private static readonly ConcurrentDictionary<string, bool> Keys = new ConcurrentDictionary<string, bool>();

		public OnlineUsersMiddleware(
			RequestDelegate next,
			int lastActivityInMinutes = lastActivityBeforeGoingOfflineInMinutes,
			string cookieName = OnlineUsersCookieName)
		{
			this.next = next;
			this.cookieName = cookieName;
			this.lastActivityInMinutes = lastActivityInMinutes;
		}

		public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
		{
			if (context.User.Identity?.IsAuthenticated ?? false)
			{
				if (!context.Request.Cookies.TryGetValue(cookieName, out string userId))
				{
					userId = context.User.GetId()!;

					context.Response.Cookies.Append(cookieName, userId, new CookieOptions
					{
						HttpOnly = true,
						MaxAge = TimeSpan.FromDays(30)
					});
				}

				memoryCache.GetOrCreate(userId, cacheEntry =>
				{
					if (!Keys.TryAdd(userId, true))
					{
						cacheEntry.AbsoluteExpiration = DateTimeOffset.MinValue;
					}
					else
					{
						cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(lastActivityBeforeGoingOfflineInMinutes);
						cacheEntry.RegisterPostEvictionCallback(RemoveKeyWhenExpired);
					}

					return string.Empty;

				});
			}
			else
			{
				if (context.Request.Cookies.TryGetValue(cookieName, out string userId))
				{
					if (!Keys.TryRemove(userId, out _))
					{
						Keys.TryUpdate(userId, false, true);
					}

					context.Response.Cookies.Delete(cookieName);
				}
			}

			return next(context);
		}

		public static bool CheckIfUserIsOnline(string userId)
		{
			bool value = Keys.TryGetValue(userId.ToLower(), out bool success);

			return success && value;
		}

		private void RemoveKeyWhenExpired(object key, object value, EvictionReason reason, object state)
		{
			string keyStr = (string)key;

			if (!Keys.TryRemove(keyStr, out _))
			{
				Keys.TryUpdate(keyStr, false, true);
			}
		}
	}
}
