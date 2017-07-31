using System;
using UnityEngine;

namespace Redspell.Logging
{
	public interface ITag
	{
		string Id { get; }
		Color32 Color { get; }
	}
}
