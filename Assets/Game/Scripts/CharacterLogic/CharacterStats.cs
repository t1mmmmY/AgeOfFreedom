using UnityEngine;
using System.Collections;
using System.Text;

[System.Serializable]
public class CharacterStats 
{
	//Mental parameters
	[SerializeField] int _charisma;
	[SerializeField] int _reputation;
	[SerializeField] int _discipline;
	[SerializeField] int _loyalty;
	[SerializeField] int _fear;

	//Skills
	[SerializeField] int _fencing;
	[SerializeField] int _pistol;
	[SerializeField] int _sailing;
	[SerializeField] int _artillery;

	//Items
	[SerializeField] int money;
	//etc...

	public int charisma
	{
		get { return _charisma; }
	}

	public int reputation
	{
		get { return _reputation; }
	}

	public int discipline
	{
		get { return _discipline; }
	}

	public int loyalty
	{
		get { return _loyalty; }
	}

	public int fear
	{
		get { return _fear; }
	}

	public int fencing
	{
		get { return _fencing; }
	}

	public int pistol
	{
		get { return _pistol; }
	}

	public int sailing
	{
		get { return _sailing; }
	}

	public int artillery
	{
		get { return _artillery; }
	}

	public CharacterStats()
	{
	}

	public void InitRandomCharacter()
	{
		System.Random rand = new System.Random();
		_charisma = rand.Next(1, 11);
		_reputation = rand.Next(-5, 6);
		_discipline = rand.Next(1, 11);
		_loyalty = rand.Next(1, 11);
		_fear = rand.Next(1, 5);
		_fencing = rand.Next(1, 11);
		_pistol = rand.Next(1, 11);
		_sailing = rand.Next(1, 11);
		_artillery = rand.Next(1, 11);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine(string.Format("charisma {0}", charisma));
		sb.AppendLine(string.Format("reputation {0}", reputation));
		sb.AppendLine(string.Format("discipline {0}", discipline));
		sb.AppendLine(string.Format("loyalty {0}", loyalty));
		sb.AppendLine(string.Format("fear {0}", fear));
		sb.AppendLine(string.Format("fencing {0}", fencing));
		sb.AppendLine(string.Format("pistol {0}", pistol));
		sb.AppendLine(string.Format("sailing {0}", sailing));
		sb.AppendLine(string.Format("artillery {0}", artillery));

		return sb.ToString();
	}

}
