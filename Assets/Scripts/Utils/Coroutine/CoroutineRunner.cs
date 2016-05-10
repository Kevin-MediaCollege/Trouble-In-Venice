using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// Coroutine runner dependency, allows you to run coroutines on non-MonoBehaviours.
	/// </summary>
	public class CoroutineRunner : IDependency
	{
		private CoroutineRunnerHelper coroutineRunnerHelper;
		private CoroutineRunnerHelper CoroutineRunnerHelper
		{
			get
			{
				if(coroutineRunnerHelper == null)
				{
					GameObject coroutineRunnerObject = new GameObject("CoroutineRunner");
					//Object.DontDestroyOnLoad(coroutineRunnerObject);

					coroutineRunnerHelper = coroutineRunnerObject.AddComponent<CoroutineRunnerHelper>();
				}

				return coroutineRunnerHelper;
			}
		}

		/// <summary>
		/// Dispose of the <see cref="CoroutineRunner"/>
		/// </summary>
		public void Dispose()
		{
			if(coroutineRunnerHelper != null)
			{
				Object.Destroy(coroutineRunnerHelper.gameObject);
			}
		}

		/// <summary>
		/// Start a coroutine.
		/// </summary>
		/// <param name="_routine">The coroutine to start.</param>
		/// <returns>The coroutine.</returns>
		public Coroutine StartCoroutine(IEnumerator _routine)
		{
			return CoroutineRunnerHelper.StartCoroutine(_routine);
		}

		/// <summary>
		/// Stop a coroutine.
		/// </summary>
		/// <param name="routine">The coroutine to stop.</param>
		public void StopCoroutine(IEnumerator routine)
		{
			CoroutineRunnerHelper.StopCoroutine(routine);
		}

		/// <summary>
		/// Stop a coroutine.
		/// </summary>
		/// <param name="routine">The coroutine to stop.</param>
		public void StopCoroutine(Coroutine routine)
		{
			CoroutineRunnerHelper.StopCoroutine(routine);
		}
	}
}
