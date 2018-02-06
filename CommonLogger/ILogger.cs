
namespace Redspell.Logging
{
	public delegate object Message ();

	public interface ILogger
	{
        void Log (string tag, Message message);
        void Log (Message message);
		void LogWarning (string tag, Message message);
		void LogWarning (Message message);
		void LogError (string tag, Message message);
		void LogError (Message message);
	}
}
