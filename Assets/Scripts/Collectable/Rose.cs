using Utils;

namespace Proeve
{
	/// <summary>
	/// A rose collectable.
	/// </summary>
	public class Rose : GridNodeObject
	{
		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				GlobalEvents.Invoke(new RosePickedUpEvent());
				Destroy(gameObject);
			}
		}
	}
}
