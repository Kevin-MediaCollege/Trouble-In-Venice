using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// 
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
		/// 
		/// </summary>
		public void Dispose()
		{
			if(coroutineRunnerHelper != null)
			{
				Object.Destroy(coroutineRunnerHelper.gameObject);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_routine"></param>
		/// <returns></returns>
		public Coroutine StartCoroutine(IEnumerator _routine)
		{
			return CoroutineRunnerHelper.StartCoroutine(_routine);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="routine"></param>
		public void StopCoroutine(IEnumerator routine)
		{
			CoroutineRunnerHelper.StopCoroutine(routine);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="routine"></param>
		public void StopCoroutine(Coroutine routine)
		{
			CoroutineRunnerHelper.StopCoroutine(routine);
		}
	}
}
