
using System;

namespace Redspell.Logging
{
	public class SilentLogger : ILogger
	{
		public void Log (string tag, Message message)
		{
		}

		public void Log (Message message)
		{
		}

		public void LogError (string tag, Message message)
		{
		}

		public void LogError (Message message)
		{
		}

		public void LogWarning (string tag, Message message)
		{
		}

		public void LogWarning (Message message)
		{
		}
	}
}
