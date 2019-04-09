using System.Configuration;

namespace FileDistributor.Configuration
{
	public class DirectoryNode : ConfigurationElement
	{
		[ConfigurationProperty("path", IsRequired = true, IsKey = true)]
		public string Path => (string)this["path"];
	}
}
