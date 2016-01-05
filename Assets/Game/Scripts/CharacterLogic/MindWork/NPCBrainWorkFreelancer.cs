using UnityEngine;
using System.Collections;

public partial class NPCBrain : BaseBrain
{

	void DoFreelancerWork()
	{
		if (DoIWantToCreateMyOwnTeam())
		{
			//			Debug.Log("I decided to create my own team! " + name);
			CreateTeam();
		}
		else
		{
			DoNothing();
		}
	}

	bool DoIWantToCreateMyOwnTeam()
	{
		System.Random rand = new System.Random();

		float randomValue = (float)((rand.NextDouble() / 2f + 0.5f) * (rand.NextDouble() * 100 + 1) * 10);

		if (randomValue < stats.ambitions)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
