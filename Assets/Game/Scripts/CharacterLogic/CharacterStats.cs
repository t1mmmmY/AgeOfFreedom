using UnityEngine;
using System.Collections;
using System.Text;

[System.Serializable]
public class CharacterStats 
{
	//Mental parameters
	public int ambitions { get; private set; }
	public int charisma { get; private set; }
	public int reputation { get; private set; }
	public int discipline { get; private set; }
	public int loyalty { get; private set; }
	public int fear { get; private set; }
	public int permanence { get; private set; }

	//Skills
	public int fencing { get; private set; }
	public int pistol { get; private set; }
	public int sailing { get; private set; }
	public int artillery { get; private set; }

	//Items
	//etc...


	public CharacterStats()
	{
	}

	public void InitRandomCharacter()
	{
		System.Random rand = new System.Random();
		ambitions = rand.Next(1, 11);
		charisma = rand.Next(1, 11);
		reputation = rand.Next(-5, 6);
		discipline = rand.Next(1, 11);
		loyalty = rand.Next(1, 11);
		fear = rand.Next(1, 5);
		permanence = rand.Next(1, 11);
		fencing = rand.Next(1, 11);
		pistol = rand.Next(1, 11);
		sailing = rand.Next(1, 11);
		artillery = rand.Next(1, 11);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine(string.Format("ambitions {0}", ambitions));
		sb.AppendLine(string.Format("charisma {0}", charisma));
		sb.AppendLine(string.Format("reputation {0}", reputation));
		sb.AppendLine(string.Format("discipline {0}", discipline));
		sb.AppendLine(string.Format("loyalty {0}", loyalty));
		sb.AppendLine(string.Format("fear {0}", fear));
		sb.AppendLine(string.Format("permanence {0}", permanence));
		sb.AppendLine(string.Format("fencing {0}", fencing));
		sb.AppendLine(string.Format("pistol {0}", pistol));
		sb.AppendLine(string.Format("sailing {0}", sailing));
		sb.AppendLine(string.Format("artillery {0}", artillery));

		return sb.ToString();
	}

}
