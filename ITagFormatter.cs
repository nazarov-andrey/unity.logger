using UnityEngine;

namespace Redspell.Logging {
	public interface ITagFormatter
	{
		string GetTag (string owner, ITag tag = null);
	}
}