namespace Utils
{
	/// <summary>
	/// Send when an entity has been activated.
	/// </summary>
	public class EntityActivatedEvent : IEvent
	{
		/// <summary>
		/// The Entity.
		/// </summary>
		public Entity Entity { private set; get; }
		
		/// <summary>
		/// Create a new <see cref="EntityActivatedEvent"/>.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		public EntityActivatedEvent(Entity _entity)
		{
			Entity = _entity;
		}
	}

	/// <summary>
	/// Send when an entity has been deactivated.
	/// </summary>
	public class EntityDeactivatedEvent : IEvent
	{
		/// <summary>
		/// The entity.
		/// </summary>
		public Entity Entity { private set; get; }

		/// <summary>
		/// Create a new <see cref="EntityDeactivatedEvent"/>.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		public EntityDeactivatedEvent(Entity _entity)
		{
			Entity = _entity;
		}
	}
}
