using System.Configuration;


namespace FileDistributor.Configuration
{
	public class DirectoriesNode : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new DirectoryNode();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DirectoryNode)element).Path;
		}
	}
}
