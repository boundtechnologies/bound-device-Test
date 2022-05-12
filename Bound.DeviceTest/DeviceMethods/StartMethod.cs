using Bound;
using Device;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WorkoutData.Managers;

namespace DeviceManager.Device.NewFolder
{
    public static class StartMethod
    {
 
        public static async Task<MethodResponse> OnStart(MethodRequest methodRequest, object userContext)
        {
            Program.IsRunning = true;
            Program.DeviceData = JsonConvert.DeserializeObject<DeviceData>(methodRequest.DataAsJson);
            Program.DeviceData.TrainingData = new List<TrainingData>();

            while (Program.IsRunning)
            {
                Thread.Sleep(100);
                var x = new Random().Next(0, 256);
                var y = new Random().Next(0, 256);
                var z = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

                Console.WriteLine($"{x}, {y}, {z}");

                Program.DeviceData.TrainingData.Add(new TrainingData() { X = x, Y = y, Z = z });
            }

            return null;
        }
    }
}
