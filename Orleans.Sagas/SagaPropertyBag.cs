using Newtonsoft.Json;
using System.Collections.Generic;

namespace Orleans.Sagas
{
	[GenerateSerializer]
	class SagaPropertyBag : ISagaPropertyBag
	{
		[Id(0)]
		private readonly Dictionary<string, string> existingProperties;

		[Id(1)]
		public Dictionary<string, string> ContextProperties { get; }

		public SagaPropertyBag() : this(new Dictionary<string, string>())
		{
		}

		public SagaPropertyBag(Dictionary<string, string> existingProperties)
		{
			this.existingProperties = existingProperties;
			ContextProperties = new Dictionary<string, string>();
		}

		public void Add<T>(string key, T value)
		{
			if (typeof(T) == typeof(string))
			{
				ContextProperties[key] = (string)(dynamic)value;
				return;
			}

			ContextProperties[key] = JsonConvert.SerializeObject(value);
		}

		public T Get<T>(string key)
		{
			if (typeof(T) == typeof(string))
			{
				return (T)(dynamic)existingProperties[key];
			}

			return JsonConvert.DeserializeObject<T>(existingProperties[key]);
		}

		public bool ContainsKey(string key)
		{
			return existingProperties.ContainsKey(key);
		}
	}
}
