using System;
using System.Collections.Generic;

public interface ITracker
{
	void OnEnable();

	void OnDisable();

	object GetValue();
}

public interface ITracker<T> : ITracker
{
	new T GetValue();
}