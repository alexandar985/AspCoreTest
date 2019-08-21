using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Business.Interfaces;
using WebApplication1.Models;
using WebApplication1.Responses;

namespace WebApplication1.Business.Services
{
    public class Service : IService
    {
        string baseUrl = "https://api.exchangeratesapi.io/";
        public ExcModel GetDatesForApi(InputDto data)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (var date in data.Dates)
            {
                dates.Add(DateTime.Parse(date));
            }

            return new ExcModel
            {
                StarDate = dates.Min().ToString("yyyy-MM-dd"),
                EndDate = dates.Max().ToString("yyyy-MM-dd")
            };
        }

        public async Task<string> GetJsonFromExternalApi(string startDate, string endDate, string baseCurrency, string targetCurrency)
        {
            var currencies = baseCurrency.ToUpper() + "," + targetCurrency.ToUpper();
            string path = baseUrl + $"history?start_at={startDate}&end_at={endDate}&symbols={currencies}";
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync(path);
                return content;
            }
        }


        public Task<ResponseDto> ReturnResultForThisTask(string json)
        {

            JObject o = JObject.Parse(json);
            JObject jsonObejct = (JObject)o["rates"];
            var dictionaryBase = new Dictionary<DateTime, float>();
            var dictionaryTarget = new Dictionary<DateTime, float>();

            foreach (var jObject in jsonObejct)
            {
                dictionaryTarget.Add(DateTime.Parse(jObject.Key), jObject.Value.First.First.Value<float>());
                dictionaryBase.Add(DateTime.Parse(jObject.Key), jObject.Value.Last.First.Value<float>());
            }



            var minTargetValue = dictionaryTarget.Values.Min();
            var maxTargetValue = dictionaryTarget.Values.Max();
            var average = dictionaryTarget.Values.Average();


            var response = new ResponseDto
            {
                MinExchangeRate = minTargetValue,
                MaxExchangeRate = maxTargetValue,
                Average = average
            };

            return Task.FromResult(response);
        }

    }
}
