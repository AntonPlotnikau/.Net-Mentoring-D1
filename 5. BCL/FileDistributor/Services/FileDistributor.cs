using FileDistributor.Interfaces;
using FileDistributor.LocalizationResources;
using FileDistributor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FileDistributor.Services
{
	public class FileDistributor : IFileDistributor
	{
		private readonly IEnumerable<Destination> destinations;
		private readonly string defaultDestination;
		private readonly ILogger logger;

		public FileDistributor(IEnumerable<Destination> destinations, string defaultDestination, ILogger logger)
		{
			this.destinations = destinations;
			this.defaultDestination = defaultDestination;
			this.logger = logger;
		}

		public void MoveFile(FileModel file)
		{
			string from = file.FullPath;
			string to = Path.Combine(defaultDestination, file.Name);

			foreach (var destination in destinations) 
			{
				if(Regex.IsMatch(file.Name, destination.SearchPattern))
				{
					logger.Log(string.Format(Resource.RuleFound, destination.SearchPattern));
					to = this.CreateDestinationPath(file, destination);
					this.MoveFile(from, to);
					logger.Log(string.Format(Resource.FileMoved, from, to));
					return;
				}
			}

			logger.Log(Resource.FileMovedDefault);
			this.MoveFile(from, to);
		}

		private void MoveFile(string from, string to)
		{
			string dir = Path.GetDirectoryName(to);
			Directory.CreateDirectory(dir);

			try
			{
				if (File.Exists(to))
				{
					File.Delete(to);
				}

				File.Move(from, to);
			}
			catch (FileNotFoundException)
			{
				logger.Log(Resource.FileNotFound, LoggingLevel.Warn);
			}
			catch (IOException ex)
			{
				logger.Log(ex.Message, LoggingLevel.Fatale);
			}
		}

		private string CreateDestinationPath(FileModel file, Destination destination)
		{
			var destinationPath = new StringBuilder();
			var fileName = Path.GetFileNameWithoutExtension(file.Name);
			var extension = Path.GetExtension(file.Name);
			var directoryPath = destination.DestinationFolder;
			destinationPath.Append(Path.Combine(directoryPath, fileName));

			if (destination.AddDate)
			{
				var dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;

				dateFormat.DateSeparator = ".";

				destinationPath.Append($"_{DateTime.Now.ToLocalTime().ToString(dateFormat.ShortDatePattern)}");
			}

			if (destination.AddNumber)
			{
				var count = Directory.GetFiles(destination.DestinationFolder, $"{fileName}_*{extension}").Length;

				destinationPath.Append($"_{count}");
			}

			destinationPath.Append(extension);

			return destinationPath.ToString();
		}
	}
}
