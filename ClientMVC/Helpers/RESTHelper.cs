using ClientMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientMVC
{
  public class RESTHelper
  {
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public RESTHelper(ILogger logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;
      _apiBaseUrl = _configuration.GetValue<string>("WebAPIBaseUrl");
    }

    //HTTP GET
    public async Task<IList<TResultModel>> GetIList<TResultModel>(string endpoint)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(_apiBaseUrl);

        var result = await client.GetAsync(endpoint);
        if (result.IsSuccessStatusCode)
        {
          var readTask = await result.Content.ReadAsAsync<IList<TResultModel>>();
          return readTask;
        }
        else //web api sent error response
        {
          _logger.LogError("Server error during GET request");
          return (IList<TResultModel>)Enumerable.Empty<TResultModel>();
        }
      }
    }

    public async Task<Response<TResultModel>> POST<TResultModel, TRequest>(string endpoint, TRequest request)
    {
      var response = new Response<TResultModel>();
      using (var client = new HttpClient())
      {
        var serilizedContent = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(serilizedContent, Encoding.UTF8, "application/json");
        client.BaseAddress = new Uri(_apiBaseUrl);

        var result = await client.PostAsync(endpoint, content);
        response.IsSuccessfull = result.IsSuccessStatusCode;
        if (result.IsSuccessStatusCode)
        {
          var readTask = await result.Content.ReadAsAsync<TResultModel>();
          response.Data = readTask;
          return response;
        }
        else
        {
          _logger.LogError("Smg Bad happend");
          return response;
        }
      }
    }

    public async Task<Response<TResultModel>> PUT<TResultModel, TRequest>(string endpoint, TRequest request)
    {
      var response = new Response<TResultModel>();
      using (var client = new HttpClient())
      {
        var serilizedContent = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(serilizedContent, Encoding.UTF8, "application/json");
        client.BaseAddress = new Uri(_apiBaseUrl);

        var result = await client.PutAsync(endpoint, content);
        response.IsSuccessfull = result.IsSuccessStatusCode;
        if (result.IsSuccessStatusCode)
        {
          var readTask = await result.Content.ReadAsAsync<TResultModel>();
          response.Data = readTask;
          return response;
        }
        else
        {
          _logger.LogError("Smg Bad happend");
          return response;
        }
      }
    }
  }
}