using System;
using UnityEngine;

public interface IInfectible
{
	public void Infect(GameObject player);
	public void Abandon(GameObject player);
	public void AttemptHostAction();
	public float GetHostSpeed();
}
