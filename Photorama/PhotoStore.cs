using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using FlickrNet;
using Foundation;

namespace Photorama
{
    public class PhotoStore
    {
        private const string ApiKey = "186a848791f5d9a1fa0ed0d5c2736292";

        public async Task FetchInterestingPhotos(Action<PhotoCollection> completion)
        {
            Flickr flickrApi = new Flickr(ApiKey);

            var collection = await flickrApi.InterestingnessGetListAsync();

            completion?.Invoke(collection);
        }
    }
}