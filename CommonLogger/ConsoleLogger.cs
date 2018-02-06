using System;
using System.Collections.Generic;

namespace Redspell.Logging
{
	public class ConsoleLogger : ILogger
	{
		private string owner;
		private IDefaultTagProvider defaultTagProvider;
		private IEnumerable<string> tags;
		
		public ConsoleLogger (
			string owner, IDefaultTagProvider defaultTagProvider, IEnumerable<string> tags)
		{
			this.owner = owner;
			this.defaultTagProvider = defaultTagProvider;
			this.tags = tags;
		}
		
		private bool IsTagLoggable (string tag)
		{
			foreach (var loggableTag in tags) {
				if (loggableTag == tag)
					return true;
			}

			return false;
		}
	
		public void Log (string tag, Message message)
		{
			bool isTabLoggable = IsTagLoggable (tag);
			if (!isTabLoggable)
				return;
				
			Console.WriteLine (string.Format ("[{0}({1})]: {2}", tag, owner, message ()));
		}

		public void Log (Message message)
		{
			Log (defaultTagProvider.DefaultTag, message);
		}

		public void LogWarning (string tag, Message message)
		{
			Log (tag, () => "WARNING: " + message ());
		}

		public void LogWarning (Message message)
		{
			Log (() => "WARNING: " + message ());
		}

		public void LogError (string tag, Message message)
		{
			Log (tag, () => "ERROR: " + message ());
		}

		public void LogError (Message message)
		{
			Log (() => "ERROR: " + message ());
		}
	}
}
