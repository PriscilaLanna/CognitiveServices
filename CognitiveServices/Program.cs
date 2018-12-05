using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;

namespace CognitiveServices
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "002d9cf2be714679abeedb67f0bf137a";
            var imageUrl = "https://i.pinimg.com/originals/26/c4/53/26c453cfa34d3d293398de7687ddfc8c.jpg";

            ComputerVisionClient client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey));

            client.Endpoint ="https://brazilsouth.api.cognitive.microsoft.com/";

            var features = new List<VisualFeatureTypes> {
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Color
      
            };

            var result = client.AnalyzeImageAsync(imageUrl).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.ReadLine();
        }
    }
}
