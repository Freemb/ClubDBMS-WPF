using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.Cache;
using DataLibrary.Operations;
using System.Runtime.Caching;
using System.Reflection;


namespace DataLibrary.Cache
{
	public static class CacheOps
	{
		// TODO Load members list. switch to new thread or async wait to continue loading the user interface
		private static readonly object lockMe = new object();
		
		public static DataTable GetFromCache<T>(string key, IModelConnector<T> connector = null)
		{
			DataTable data = MemoryCache.Default.Get(key) as DataTable;
			if(data == null)
			{
				lock (lockMe)
				{
					data = MemoryCache.Default.Get(key) as DataTable;
					if(data != null)
					{
						return data;
					}
					data = connector?.Load(null);
					//data = connector.Load(null).Tables[key]; //key should be same as column name in table
					
					if(connector != null)
					{
						var duration = DateTimeOffset.UtcNow.AddMinutes(30);
						MemoryCache.Default.AddOrGetExisting(key, data, duration);
					}
					
					
				}
			}

			return data; //returns null if cache has expired
		}



	}
}
