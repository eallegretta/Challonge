using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Challonge.WebSite.Backend
{
	public class WebClient
	{
		private static readonly bool _servicePointManagerInitialized = false;

		static WebClient()
		{
			if (!_servicePointManagerInitialized)
			{
				ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
				_servicePointManagerInitialized = true;
			}
		}


		private string Serialize(object instance)
		{
			StringBuilder serialized = new StringBuilder();

			//var dataContractAttribute = (DataContractAttribute)instance.GetType().GetCustomAttributes(typeof(DataContractAttribute), true)[0];

			//serialized.AppendFormat(@"<?xml version=""1.0"" encoding=""UTF-8""?><{0}>", dataContractAttribute.Name);
			foreach (var property in instance.GetType().GetProperties())
			{
				var attrs = property.GetCustomAttributes(typeof(JsonProperty), true);
				if (attrs != null && attrs.Length == 1)
				{
					var jsonPropertyAttr = attrs[0] as JsonProperty;
                    if(jsonPropertyAttr != null)
    					serialized.AppendFormat("{0}={1}&", jsonPropertyAttr.PropertyName, property.GetValue(instance, null));
					//serialized.AppendFormat("<{0}>{1}</{0}>", dataMemberAttr.Name, property.GetValue(instance, null));
				}
			}
			//serialized.AppendFormat("</{0}>", dataContractAttribute.Name);

			return serialized.ToString();
		}

		private HttpWebRequest CreateRequest(string url, string method)
		{
			url = "https://ffva:7xeh8dsmbnag6j77h3v6vxctp3n8wd36bd8d6mv4@challonge.com/api/" + url;
			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = method;
			request.Credentials = new NetworkCredential("ffva", "7xeh8dsmbnag6j77h3v6vxctp3n8wd36bd8d6mv4");
			return request;
		}

		public T Get<T>(string url) where T: class
		{
			HttpWebRequest request = CreateRequest(url, "GET");

			using (Stream response = request.GetResponse().GetResponseStream())
			{
				string json;
				using (StreamReader reader = new StreamReader(response))
				{
					json = reader.ReadToEnd();
				}

				return JsonConvert.DeserializeObject(json, typeof(T)) as T;
			}
		}

		public TResponse Put<T, TResponse>(string url, T instance)
			where T : class
			where TResponse : class
		{
			return PostOrPut<T, TResponse>(url, instance, "PUT");
		}

		private TResponse PostOrPut<T, TResponse>(string url, T instance, string method)
			where T : class
			where TResponse : class
		{
			HttpWebRequest request = CreateRequest(url, method);

			string serializedData = Serialize(instance);

			request.ContentLength = serializedData.Length;
			request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
			using (Stream requestStream = request.GetRequestStream())
			{
				using (StreamWriter requestWriter = new StreamWriter(requestStream))
				{
					requestWriter.Write(serializedData.ToString());
				}
			}


			try
			{
				using (var response = request.GetResponse())
				{
					string json;
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						json = reader.ReadToEnd();
					}

					return JsonConvert.DeserializeObject(json, typeof(TResponse)) as TResponse;
				}
			}
			catch (WebException ex)
			{
				using (var reader = new StreamReader(ex.Response.GetResponseStream()))
				{
					throw new Exception(reader.ReadToEnd(), ex);
				}
			}
		}

		public TResponse Post<T, TResponse>(string url, T instance) 
			where T : class
			where TResponse: class
		{
			return PostOrPut<T, TResponse>(url, instance, "POST");
		}
	}
}
