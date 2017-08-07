using UnityEngine;
using System.Collections.Generic;

namespace Redspell.Logging {
	public class Logger : ILogger
	{
		private ILogger logger;
		private ITag[] tags;
		private ITagFormatter tagFormatter;
		private string owner;
		private IDefaultTagProvider defaultTagProvider;

		public Logger (
			ILogger logger,
			ITag[] tags,
			ITagFormatter tagFormatter,
			string owner,
			IDefaultTagProvider defaultTagProvider)
		{
			this.logger = logger;
			this.tags = tags;
			this.tagFormatter = tagFormatter;
			this.owner = owner;
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

		public bool IsLogTypeAllowed (LogType logType)
		{
			return logger.IsLogTypeAllowed (logType);
		}

		public void Log (LogType logType, object message)
		{
			Log (logType, GetDefaultTag (), message);
		}

		public void Log (LogType logType, object message, Object context)
		{
			Log (logType, GetDefaultTag (), message, context);
		}

		public void Log (LogType logType, string tag, object message)
		{
			if (IsTagLoggable (tag))
				logger.Log (logType, FormatTag (tag), message);
		}

		public void Log (LogType logType, string tag, object message, Object context)
		{
			if (IsTagLoggable (tag))
				logger.Log (logType, FormatTag (tag), message, context);
		}

		public void Log (object message)
		{
			Log (GetDefaultTag (), message);
		}

		public void Log (string tag, object message)
		{
			if (IsTagLoggable (tag))
				logger.Log (FormatTag (tag), message);
		}

		public void Log (string tag, object message, Object context)
		{
			if (IsTagLoggable (tag))
				logger.Log (FormatTag (tag), message, context);
		}

		public void LogWarning (string tag, object message)
		{
			if (IsTagLoggable (tag))
				logger.LogWarning (FormatTag (tag), message);
		}

		public void LogWarning (string tag, object message, Object context)
		{
			if (IsTagLoggable (tag))
				logger.LogWarning (FormatTag (tag), message, context);
		}

		public void LogError (string tag, object message)
		{
			if (IsTagLoggable (tag))
				logger.LogError (FormatTag (tag), message);
		}

		public void LogError (string tag, object message, Object context)
		{
			if (IsTagLoggable (tag))
				logger.LogError (FormatTag (tag), message, context);
		}

		public void LogFormat (LogType logType, string format, params object[] args)
		{
			Log (logType, string.Format (format, args));
		}

		public void LogException (System.Exception exception)
		{
			Log (LogType.Exception, exception);
		}

		public void LogFormat (LogType logType, Object context, string format, params object[] args)
		{
			Log (logType, (object) string.Format (format, args), context);
		}

		public void LogException (System.Exception exception, Object context)
		{
			Log (LogType.Exception, exception, context);
		}

		public ILogHandler logHandler {
			get {
				return logger.logHandler;
			}
			set {
				logger.logHandler = value;
			}
		}

		public bool logEnabled {
			get {
				return logger.logEnabled;
			}
			set {
				logger.logEnabled = value;
			}
		}

		public LogType filterLogType {
			get {
				return logger.filterLogType;
			}
			set {
				logger.filterLogType = value;
			}
		}
	}
}