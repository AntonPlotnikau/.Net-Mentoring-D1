using System.Configuration;

namespace FileDistributor.Configuration
{
	public class DestinationNode : ConfigurationElement
	{
		[ConfigurationProperty("searchPattern", IsRequired = true, IsKey = true)]
		public string SearchPattern => (string)this["searchPattern"];

		[ConfigurationProperty("destinationFolder", IsRequired = true)]
		public string DestinationFolder => (string)this["destinationFolder"];

		[ConfigurationProperty("addDate", IsRequired = false, DefaultValue = false)]
		public bool AddDate => (bool)this["addDate"];

		[ConfigurationProperty("addNumber", IsRequired = false, DefaultValue = false)]
		public bool AddNumber => (bool)this["addNumber"];
	}
}
