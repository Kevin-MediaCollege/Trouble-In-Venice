using System.Collections;

public abstract class PickupBehaviour
{
	private CoroutineRunner coroutineRunner;
	private bool running;

	public PickupBehaviour()
	{
		coroutineRunner = Dependency.Get<CoroutineRunner>();
		coroutineRunner.StartCoroutine(InternalUpdate());
	}

	protected virtual IEnumerator Update()
	{
		yield return null;
	}

	protected void Stop()
	{
		running = false;

		GlobalEvents.Invoke(new PickupStopEvent());
	}

	private IEnumerator InternalUpdate()
	{
		running = true;

		while(running)
		{
			yield return coroutineRunner.StartCoroutine(Update());
		}
	}
}