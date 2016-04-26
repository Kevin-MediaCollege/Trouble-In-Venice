using UnityEngine;
using System.Collections;
using System;

public class LinkPickupConnectionDoor : LinkPickupConnection
{
	[SerializeField] private bool startOpen;

	private EntityNodeTracker nodeTracker;

	protected void Awake()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
		nodeTracker.CurrentNode.Active = startOpen;
		GetComponent<Renderer>().enabled = !startOpen;
	}

	public override void OnPickup()
	{
		nodeTracker.CurrentNode.Active = !nodeTracker.CurrentNode.Active;
		GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
	}

	protected void OnValidate()
	{
		GetComponent<Renderer>().enabled = !startOpen;
	}
}