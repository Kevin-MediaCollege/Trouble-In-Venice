namespace Proeve
{
	/// <summary>
	/// An AI command, any classes inheriting from this will have to implement the <see cref="Execute(AIBase)"/> method,
	/// it is called by <see cref="AIBase.ExecuteCommand{T}"/>.
	/// </summary>
	public abstract class AICommand
	{
		public abstract void Execute(AIBase _ai);
	}
}
