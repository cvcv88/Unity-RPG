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
	// Serializable�� �ٿ��־�� ����ȭ�ȴ�.

	public GameData() // �ʱⰪ
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
