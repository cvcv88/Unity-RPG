using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR // true�� else ���� ���� ���� �� ���, false�� else ���� ����
	private string path => $"{Application.dataPath}/Data";
#else
    private string path => $"{Application.persistentDataPath}/Data";
#endif

	public GameData gameData;
	/*public int level;
    public int gold;
    public int exp;*/

	public void NewData()
	{
		gameData = new GameData();
	}

	// ��Ʈ����Ʈ attribute �Լ� �տ� �ٿ��ְ�, �̸� �����ش�.
	// ������Ʈ���� ��Ŭ���ϸ� �޴��� �����ִ�.
	[ContextMenu("SaveData")]
	public void SaveData()
	{
		// string path = Application.dataPath;
		// "C:\\Users\\��Ÿ�ù�5��\\Unity-RPG\\Test.txt";
		// Debug.Log(path);

		Debug.Log(path);
		// if (Directory.Exists($"{Application.dataPath}/Data") == false)
		if (Directory.Exists(path) == false)
		{
			Debug.Log("������ ��� ������ �����ص�");
			Directory.CreateDirectory(path);
		}
		string filePath = Path.Combine(path, "Test.txt");
		string json = JsonUtility.ToJson(gameData, true); // Json ����ȭ
		Debug.Log(json);
		File.WriteAllText(filePath, json); // json text ����� �����ϱ�
										   // File.WriteAllText(filePath, level.ToString());
										   // File.WriteAllText(filePath, $"level:{level} gold:{gold} exp:{2000}"); // "level:1 gold:100 exp:2000"
										   // Debug.Log("SaveData");
	}


	[ContextMenu("Load")]
	public void Load()
	{
		string filePath = Path.Combine(path, "Test.txt");
		if (File.Exists(filePath))
		{
			// string text = File.ReadAllText(filePath);
			// level = int.Parse(text); // int������ �ٽ� �ٲ㼭 ���ǹ��� �ڷ� �����
			// Debug.Log(text);
			// Debug.Log("Load");

			string json = File.ReadAllText(filePath);
			JsonUtility.FromJson<GameData>(json);
		}
		else
		{
			Debug.Log("������ ������ ����");
			// level = 1; // ����� ���� ���ٸ� 1�� ����
			// ���� ����
		}
	}
}
