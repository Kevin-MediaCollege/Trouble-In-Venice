/// <summary>
/// Send when an entity has been activated.
/// </summary>
public class EntityActivatedEvent : IEvent
{
	public Entity Entity { private set; get; }

	public EntityActivatedEvent(Entity _entity)
	{
		Entity = _entity;
	}
}