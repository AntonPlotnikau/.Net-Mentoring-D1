using System;
using System.Configuration;
using System.Globalization;

namespace FileDistributor.Configuration
{
	public class FileDistributorConfigurationSection: ConfigurationSection
	{
		[ConfigurationProperty("culture", DefaultValue = "en-EN", IsRequired = false)]
		public CultureInfo Culture => (CultureInfo)this["culture"];

		[ConfigurationCollection(typeof(DirectoryNode), AddItemName = "directory")]
		[ConfigurationProperty("directories", IsRequired = false)]
		public DirectoriesNode Directories => (DirectoriesNode)this["directories"];

		[ConfigurationCollection(typeof(DestinationNode), AddItemName = "destination")]
		[ConfigurationProperty("destinations", IsRequired = true)]
		public DestinationsNode Destinations => (DestinationsNode)this["destinations"];
	}
}
