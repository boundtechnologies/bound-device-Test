// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Bound;
using DeviceManager.Device.DeviceMethods;
using DeviceManager.Device.NewFolder;
using Microsoft.Azure.Devices.Client;
using System;

namespace Device
{
    public class Program
    {
        public static bool IsRunning = false;

        public static DeviceData DeviceData { get; set; }
        static string ChestMachine = "HostName=boundiothub.azure-devices.net;DeviceId=ChestMachine;SharedAccessKey=Rro705GoUrJk4lN/KMynIVctyGSDX83I6x+tKdJACiY=";
        static string ShoulderMachine = "HostName=boundiothub.azure-devices.net;DeviceId=ShoulderMachine;SharedAccessKey=n9bN2ZfYCNa217ErZeBD7Jg7j5URMVslgLl1OuKGVoo=";
        static string BackMachine = "HostName=boundiothub.azure-devices.net;DeviceId=BackMachine;SharedAccessKey=82nYznA9sdamc8MXDPTtsDmyRpBk48EJ6rpx4hdCgSE=";

        public static void Main(string[] args)
        {
            Console.WriteLine("Device started");
            DeviceClient Client = DeviceClient.CreateFromConnectionString(ShoulderMachine, TransportType.Mqtt);

            Client.SetMethodHandlerAsync("start", StartMethod.OnStart, null).Wait();
            Client.SetMethodHandlerAsync("stop", StopMethod.OnStop, null).Wait();
            Console.ReadLine();

            Client.SetMethodHandlerAsync("start", null, null).Wait();
            Client.SetMethodHandlerAsync("stop", null, null).Wait();
            Client.CloseAsync().Wait();
        }
    }
}
