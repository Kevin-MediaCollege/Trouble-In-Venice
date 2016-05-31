using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Character ID.
	/// </summary>
	public enum CharacterID
	{
		/// <summary>
		/// Male character.
		/// </summary>
		Male,

		/// <summary>
		/// Female character.
		/// </summary>
		Female
	}

	/// <summary>
	/// Selected character information, <see cref="Player"/> is set to <see cref="CharacterID.Male"/> by default.
	/// </summary>
	public class CharacterInfo : IDependency
	{
		/// <summary>
		/// The <see cref="CharacterID"/> of the player.
		/// </summary>
		public CharacterID Player { set; get; }

		/// <summary>
		/// The <see cref="CharacterID"/> of the target (male if <see cref="Player"/> is female, female if <see cref="Player"/> is male).
		/// </summary>
		public CharacterID Target
		{
			get
			{
				return Player == CharacterID.Female ? CharacterID.Male : CharacterID.Female;
			}
		}

		/// <summary>
		/// Create an instance of this class.
		/// </summary>
		public CharacterInfo()
		{
			// Default to male model
			Player = CharacterID.Male;
		}
	}
}
