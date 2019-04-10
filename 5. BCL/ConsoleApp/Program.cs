using FileDistributor.Configuration;
using FileDistributor.Interfaces;
using FileDistributor.Models;
using FileDistributor.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace ConsoleApp
{
	class Program
	{
		private static ManualResetEvent waitHandle = new ManualResetEvent(false);

		static void Main(string[] args)
		{
			FileDistributorConfigurationSection section = ConfigurationManager.GetSection("fileDistributorSection") as FileDistributorConfigurationSection;

			var directories = new List<string>();
			var destinations = new List<Destination>();

			foreach(DirectoryNode directory in section.Directories)
			{
				directories.Add(directory.Path);
			}

			foreach(DestinationNode destination in section.Destinations)
			{
				destinations.Add(new Destination
				{
					SearchPattern = destination.SearchPattern,
					DestinationFolder = destination.DestinationFolder,
					AddDate = destination.AddDate,
					AddNumber = destination.AddNumber
				});
			}

			CultureInfo.DefaultThreadCurrentCulture = section.Culture;
			CultureInfo.DefaultThreadCurrentUICulture = section.Culture;
			CultureInfo.CurrentUICulture = section.Culture;
			CultureInfo.CurrentCulture = section.Culture;

			ILogger logger = new Logger();
			IFileDistributor distributor = new FileDistributor.Services.FileDistributor(destinations, 
																						section.Destinations.DefaultDirectory,
																						logger);

			IDirectoryWatcher watcher = new DirectoryWatcher(directories, logger);

			watcher.FileCreated += (o, e) => { distributor.MoveFile(e.File); };

			Console.CancelKeyPress += (o, e) =>
			{
				waitHandle.Set();
			};

			waitHandle.WaitOne();
		}
	}
}
