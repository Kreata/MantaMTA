﻿using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using Colony101.MTA.Library;
using Colony101.MTA.Library.Client;
using Colony101.MTA.Library.MtaIpAddress;
using Colony101.MTA.Library.Server;

namespace MTA_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Logging.Info("MTA Started");

			AppDomain.CurrentDomain.FirstChanceException += delegate(object sender, FirstChanceExceptionEventArgs e)
			{
				Logging.Warn("", e.Exception);
			};

			MtaIpAddressCollection ipAddresses = IpAddressManager.GetIPsForListeningOn();

			// Array will hold all instances of SmtpServer, one for each port we will be listening on.
			ArrayList smtpServers = new ArrayList();
			
			// Create the SmtpServers
			for (int c = 0; c < ipAddresses.Count; c++)
			{
				MtaIpAddress ipAddress = ipAddresses[c];
				for (int i = 0; i < MtaParameters.ServerListeningPorts.Length; i++)
					smtpServers.Add(new SmtpServer(ipAddress.IPAddress, MtaParameters.ServerListeningPorts[i]));
			}

			// Start the SMTP Client
			SmtpClient.Start();
			
			bool quit = false;
			while (!quit)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if (key.KeyChar == 'q' || key.KeyChar == 'Q')
					quit = true;
			}

			// Need to wait while servers & client shutdown.
			SmtpClient.Stop();
			for (int i = 0; i < smtpServers.Count; i++)
				(smtpServers[i] as SmtpServer).Dispose();

			Logging.Info("MTA Stopped");
		}
	}
}
