/*
 * Created by SharpDevelop.
 * User: Paleta
 * Date: 10/12/2012
 * Time: 00:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Web;
using System.Web.Caching;

namespace Challonge.WebSite.Backend.Caching
{
	/// <summary>
	/// Description of CacheManager.
	/// </summary>
	public static class CacheManager
	{
		private static Cache Cache {get { return HttpContext.Current.Cache; } }
		
		public static void Clear()
		{
			foreach(string key in Cache)
			{
				Cache.Remove(key);
			}
		}
		
		public static T GetOrSet<T>(string key, Func<T> action, int duration = 60)
		{
			object ret = Cache[key];
			
			if(ret == null)
			{
				ret = action();
				
				Cache.Add(key, ret, null, DateTime.Now.AddMinutes(duration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
			
			return (T)ret;
		}
	}
}
