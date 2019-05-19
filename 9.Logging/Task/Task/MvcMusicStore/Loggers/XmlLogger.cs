using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MvcMusicStore.Loggers
{
	public class XmlLogger : ILogger
	{
		private readonly string path;
		private readonly bool isEnabled;

		public XmlLogger(LogSettings settings)
		{
			this.path = settings.Path;
			this.isEnabled = settings.IsEnabled;
		}

		public void Log(string message)
		{
			Log(message, LoggingLevel.Info);
		}

		public void Log(string message, LoggingLevel level)
		{
			if (isEnabled)
			{
				XDocument xdoc;

				if (!File.Exists(path))
				{
					xdoc = new XDocument();
				}
				else
				{
					xdoc = XDocument.Load(path);
				}

				if (xdoc.Element("logs") == null)
				{
					xdoc.Add(new XElement("logs"));
				}

				xdoc.Element("logs").Add(new XElement("log",
					new XAttribute("created", DateTime.Now),
					new XElement("loglevel", level),
					new XElement("message", message)));

				xdoc.Save(path);
			}
		}
	}
}