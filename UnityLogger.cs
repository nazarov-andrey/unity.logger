
using System;
using UnityEngine;

namespace Redspell.Logging
{
	public class UnityLogger : ILogger
	{
		private ITag[] tags;
		private ITagFormatter tagFormatter;
		private string owner;
		private IDefaultTagProvider defaultTagProvider;

		public UnityLogger (
			string owner,
			ITag[] tags,
			ITagFormatter tagFormatter,
			IDefaultTagProvider defaultTagProvider)
		{
			this.tags = tags;
			this.owner = owner;
			this.tagFormatter = tagFormatter;
			this.defaultTagProvider = defaultTagProvider;			
		}
		
		private string FormatTag (string tagId)
		{
			ITag tag = System.Array.Find (tags, x => x.Id == tagId);
			if (tag == null)
				throw new System.Exception ("Cannot find tag " + tagId);

			return tagFormatter.GetTag (owner, tag);
		}

		private string GetDefaultTag ()
		{
			return defaultTagProvider.DefaultTag;
		}

		private bool IsTagLoggable (string tagId)
		{
			ITag tag = System.Array.Find (tags, x => x.Id == tagId);
			bool isTabLoggable = tag != null && tag.Enabled;
			return isTabLoggable;
		}

		private void Log (LogType logType, object message)
		{
			Log (logType, GetDefaultTag (), message);
		}

		private void Log (LogType logType, object message, UnityEngine.Object context)
		{
			Log (logType, GetDefaultTag (), message, context);
		}

		private void Log (LogType logType, string tag, object message)
		{
			if (IsTagLoggable (tag))
				Debug.unityLogger.Log (logType, FormatTag (tag), message);
		}
		
		private void Log (LogType logType, string tag, object message, UnityEngine.Object context)
		{
			if (IsTagLoggable (tag))
				Debug.unityLogger.Log (logType, FormatTag (tag), message, context);
		}

	
		public void Log (string tag, Message message)
		{
			Log (LogType.Log, tag, message ());
		}

		public void Log (Message message)
		{
			Log (LogType.Log, message ());
		}

		public void LogError (string tag, Message message)
		{
			Log (LogType.Error, tag, message ());
		}

		public void LogError (Message message)
		{
			Log (LogType.Error, message ());
		}

		public void LogWarning (string tag, Message message)
		{
			Log (LogType.Warning, tag, message ());
		}

		public void LogWarning (Message message)
		{
			Log (LogType.Warning, message ());
		}
	}
}
