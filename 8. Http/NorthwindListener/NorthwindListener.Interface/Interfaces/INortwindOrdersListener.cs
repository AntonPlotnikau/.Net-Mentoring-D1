using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindListener.Interface.Interfaces
{
	public interface INortwindOrdersListener
	{
		void StartListen(string prefix);
		void StopListen();

		void ProcessRequest();
	}
}
