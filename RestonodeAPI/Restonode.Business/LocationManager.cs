using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Restonode.Business.Interfaces;
using Restonode.Common.Settings;

namespace Restonode.Repositories
{
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast2 northeast { get; set; }
        public Southwest2 southwest { get; set; }
    }

    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

    public class LocationManager : ILocationManager
    {
        public readonly GoogleSettings _googleSettings;

        public LocationManager(GoogleSettings googleSettings)
        {
            _googleSettings = googleSettings;
        }

        
        public async Task<RootObject> GetLocationAsync(string address)
        {
            return await Task.Run(async () => {
                var response = new RootObject();

                var requestUrl = string.Format(_googleSettings.MapsApiTemplate, address, _googleSettings.ApiKey);

                var request = (HttpWebRequest)WebRequest.Create(requestUrl);

                var result = await request.GetResponseAsync();

                using (var streamreader = new StreamReader(((HttpWebResponse)result).GetResponseStream()))
                {
                    var reponseText = streamreader.ReadToEndAsync();

                    if (!string.IsNullOrWhiteSpace(reponseText.Result))
                    {
                        response = JsonConvert.DeserializeObject<RootObject>(reponseText.Result);
                    }
                }

                return response;
            });            
        }

        public void CalculateETA(RootObject origin, RootObject destination)
        {
            var requestUrl = string.Format(_googleSettings.DistanceApiTemplate, origin.results[0].place_id, destination.results[0].place_id, _googleSettings.ApiKey);

            var request = (HttpWebRequest)WebRequest.Create(requestUrl);

            var res = (HttpWebResponse)request.GetResponse();

            using (var streamreader = new StreamReader(res.GetResponseStream()))
            {
                var result = streamreader.ReadToEnd();
            }        
        }
    }
}