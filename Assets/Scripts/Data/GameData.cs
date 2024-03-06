using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
	public string name;
	public int level;
	public int gold;
	public int exp;
	//public float speed;
	//public bool check;

	// public Vector3 position;
	// public GameObject target;

	public bool[] SceneSaved;
	public GameSceneData gameSceneData;
	// Serializable을 붙여주어야 직렬화된다.

	public GameData() // 초기값
	{
		name = "Player";
		level = 1;
		gold = 0;
		exp = 0;
		//speed = 3;
		//check = false;

		SceneSaved = new bool[32];
		gameSceneData = new GameSceneData();
	}
}
