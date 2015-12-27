using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BrainStorage
{
	public static List<NPCBrain> allBrains { get; private set; }

	private static int brainTick = 1;
	private static bool needToThink = true;

	static BrainStorage()
	{
		allBrains = new List<NPCBrain>();
		Loom.RunAsync(ConsciousnessLoop);
	}

	public static NPCBrain CreateBrain(BaseCharacter character)
	{
		NPCBrain brain = new NPCBrain();
		brain.Init();
		brain.InitRandomBrain(character);
		allBrains.Add(brain);
		return brain;
	}


	public static void EndGame()
	{
		needToThink = false;

		foreach (NPCBrain brain in allBrains)
		{
			brain.StopThink();
		}
	}

	private static void ConsciousnessLoop()
	{
		do
		{
			if (allBrains != null && allBrains.Count > 0)
			{
				int count = allBrains.Count;
				if (brainTick < count)
				{
					brainTick = count;
				}
				try
				{
					foreach (NPCBrain brain in allBrains)
					{
						if (!needToThink)
						{
							break;
						}

						brain.Think();
						if (TestGameController.Instance != null && !TestGameController.Instance.speedUp)
						{
							System.Threading.Thread.Sleep(brainTick / allBrains.Count);
						}

						//Need to break. Otherwise I will get exception
						if (count != allBrains.Count)
						{
							break;
						}
					}

					if (TestGameController.Instance != null && TestGameController.Instance.speedUp)
					{
						System.Threading.Thread.Sleep(100);
					}
				}
				catch (System.Exception ex)
				{
					Debug.LogException(ex);
				}
			}

		} while (needToThink);
	}
}
