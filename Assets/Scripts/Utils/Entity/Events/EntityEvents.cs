namespace Utils
{
	/// <summary>
	/// Send when an entity has been activated.
	/// </summary>
	public class EntityActivatedEvent : IEvent
	{
		/// <summary>
		/// 
		/// </summary>
		public Entity Entity { private set; get; }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
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
		/// 
		/// </summary>
		public Entity Entity { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
		public EntityDeactivatedEvent(Entity _entity)
		{
			Entity = _entity;
		}
	}
}
