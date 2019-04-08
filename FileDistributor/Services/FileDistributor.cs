using FileDistributor.Interfaces;
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
					logger.Log("Rule match");
					to = this.RenderDestinationPath(file, destination);
					this.MoveFile(from, to);
					logger.Log("File was moved");
					return;
				}
			}

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
				logger.Log("File is not found", LoggingLevel.WARN);
			}
			catch (IOException)
			{
				logger.Log("IOException", LoggingLevel.FATALE);
			}
		}

		private string RenderDestinationPath(FileModel file, Destination destination)
		{
			var destinationPath = new StringBuilder();
			var fileName = Path.GetFileNameWithoutExtension(file.Name);
			var extension = Path.GetExtension(file.Name);
			var directoryPath = destination.DestinationFolder;
			destinationPath.Append(Path.Combine(directoryPath, fileName));

			if (destination.AddDate)
			{
				var dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;

				destinationPath.Append($"_{DateTime.Now.ToLocalTime().ToString(dateFormat.ShortDatePattern)}");
			}

			destinationPath.Append(extension);

			return destinationPath.ToString();
		}
	}
}
