using Bound;
using Device;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Device.NewFolder
{
    public static class StartMethod
    {

        public static async Task<MethodResponse> OnStart(MethodRequest methodRequest, object userContext)
        {
            if (!Program.DeviceIsInUse && !Program.IsRunning)
            {
                Program.DeviceIsInUse = true;
                Program.IsRunning = true;
                Program.UserData = JsonConvert.DeserializeObject<UserData>(methodRequest.DataAsJson);
                Program.UserData.TrainingData = new List<TrainingData>();

                while (Program.IsRunning)
                {
                    Thread.Sleep(100);
                    var x = new Random().Next(0, 256);
                    var y = new Random().Next(0, 256);
                    var z = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

                    Console.WriteLine($"{x}, {y}, {z}");

                    Program.UserData.TrainingData.Add(new TrainingData() { X = x, Y = y, Z = z });
                }
            }
            return null;
        }


    }
}
