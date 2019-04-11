using System.Configuration;

namespace FileDistributor.Configuration
{
	public class DestinationsNode : ConfigurationElementCollection
	{
		[ConfigurationProperty("defaultDirectory", IsRequired = true)]
		public string DefaultDirectory => (string)this["defaultDirectory"];

		protected override ConfigurationElement CreateNewElement()
		{
			return new DestinationNode();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DestinationNode)element).SearchPattern;
		}
	}
}
