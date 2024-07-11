using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Security.Cryptography.X509Certificates;
namespace SubDbDownload
{
	public class MyWebRequest
	{
		public static string post(string url, string strData)
		{
			string result = "";
			try
			{
				byte[] bytes = Encoding.Default.GetBytes(strData);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
				{
					ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(MyWebRequest.CheckValidationResult);
				}
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Headers.Add("Accept-Language", "zh - CN,zh; q = 0.8");
				httpWebRequest.Timeout = 30000;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream(), Encoding.UTF8);
				result = streamReader.ReadToEnd();
				streamReader.Close();
			}
			catch (Exception)
			{

			}
			return result;
		}

		public static string get(string url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
				{
					ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(MyWebRequest.CheckValidationResult);
				}
				httpWebRequest.Method = "GET";
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Headers.Add("Accept-Language", "zh - CN,zh; q = 0.8");
				httpWebRequest.Timeout = 10000;
				StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream(), Encoding.UTF8);
				result = streamReader.ReadToEnd();
				streamReader.Close();
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			return true;
		}
	}
}
