using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Loggers
{
	public class LogSettings : ConfigurationSection
	{
		public static LogSettings Settings { get; } = ConfigurationManager.GetSection("LogSettings") as LogSettings;

		[ConfigurationProperty("isEnabled", DefaultValue = true, IsRequired = false)]
		public bool IsEnabled
		{
			get { return (bool)this["isEnabled"]; }
			set { this["isEnabled"] = value; }
		}

		[ConfigurationProperty("path", DefaultValue = @"D:\logs.xml", IsRequired = false)]
		public string Path
		{
			get { return (string)this["path"]; }
			set { this["path"] = value; }
		}
	}
}