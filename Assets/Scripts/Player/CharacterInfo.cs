using System.Collections;
using Utils;

namespace Proeve
{
	public enum CharacterID
	{
		Male,
		Female
	}

	public class CharacterInfo : IDependency
	{
		public CharacterID Player { set; get; }

		public CharacterID Target
		{
			get
			{
				return Player == CharacterID.Female ? CharacterID.Male : CharacterID.Female;
			}
		}

		public CharacterInfo()
		{
			// Default to male model
			Player = CharacterID.Male;
		}
	}
}
