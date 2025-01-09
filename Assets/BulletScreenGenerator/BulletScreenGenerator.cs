using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class BulletScreenGenerator : MonoBehaviour
{
	EnemyShooting enemyShooting;

	code[,] bullet2DArray = new code[13,22];

	public ColorToPrefab[] codeMappings;
	string path = "BulletScreenMap1";
	string pattern1;
	private void Awake()
    {
		pattern1 = Resources.Load(path).ToString();
		//Debug.Log(pattern1);
    }
    public void GenerateBulletMap(GameObject enemy)
	{
		ReadTextFile();
		enemyShooting = enemy.GetComponent<EnemyShooting>();
		enemyShooting.ClearChosenList();
        for (int x = 0; x < 13; x++)
        {
            for (int y = 0; y < 22; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
	public void ReadTextFile()
	{
		string[] splitRow =  pattern1.Split("\n");
		for (int i = 0; i < splitRow.Length; ++i)
		{
			//Debug.Log(splitRow[i]);
			string[] items = splitRow[i].Split('	');
			for (int j = 0; j < items.Length; j++)
			{
				if (Enum.TryParse(items[j], out code c))
                {
					switch (c)
					{
						case code.N:
							bullet2DArray[i, j] = code.N;
							break;
						case code.A:
							bullet2DArray[i, j] = code.A;
							break;
					}
				}
			}
		}
	}

	void GenerateTile(int x, int y)
	{
		code bulletCode = bullet2DArray[x,y];

		if (bulletCode ==code.N)
		{
			return;
		}

        foreach (ColorToPrefab codeMapping in codeMappings)
        {
            if (codeMapping.code ==code.A)
            {
				enemyShooting.AddChosen2DArrayToList(x, y);
            }
        }
    }
}
