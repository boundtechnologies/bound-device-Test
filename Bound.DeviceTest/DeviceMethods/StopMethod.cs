using Bound;
using Device;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkoutData.Managers;

namespace DeviceManager.Device.DeviceMethods
{
    public class StopMethod
    {
        static string BlobsConnectionString = "DefaultEndpointsProtocol=https;AccountName=boundalgorithmapi;AccountKey=hYgYq3IOQw+FSYxnl+1pXoYsSOlskLVLZQ11GUmDSmOBwzioLg4OvUL4P4pG/PQ7/lbRiNarZ1/42WfYEJ3AMQ==;EndpointSuffix=core.windows.net";
        public static BlobsManager blobsManager = new BlobsManager(BlobsConnectionString);

        public static async Task<MethodResponse> OnStop(MethodRequest methodRequest, object userContext)
        {
            if (Program.DeviceData != null)
            {
                Program.IsRunning = false;
                var filePath = CreateBlobPath(Program.DeviceData);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                string trainingData = JsonConvert.SerializeObject(Program.DeviceData.TrainingData);
                _ = await blobsManager.AppendDataInBlob(filePath, trainingData);

                Console.WriteLine(stopwatch.ElapsedMilliseconds + " millisekunder tog det att ladda upp datan");
                Console.WriteLine("Lägnden på listan: " + Program.DeviceData.TrainingData.Count + " rader");
                Console.WriteLine("Device stopped");

                Program.DeviceData.TrainingData.Clear();
            }

            return null;
        }
        static string CreateBlobPath(DeviceData deviceData)
        {
            string blobPath = deviceData.ObjectId + "/";
            blobPath += deviceData.MachineName.ToLower() + "/";
            blobPath += DateTime.Now.ToString("yyyyMMdd") + ".txt";

            return blobPath;
        }
    }
}
