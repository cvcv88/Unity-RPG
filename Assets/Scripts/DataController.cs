using System;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
#if UNITY_EDITOR
	private string path = $"{Application.dataPath}/Data";
#else
	private string path = $"{Application.persistentDataPath}/Data";
#endif

	public GameData gameData ;

	[ContextMenu("Save")]
	public void Save()
	{
		if (Directory.Exists(path) == false)
		{
			Directory.CreateDirectory(path);
		}

		string filePath = Path.Combine(path, "Data/Test.txt");
		string json = JsonUtility.ToJson(gameData); // json ��Ģ�� �°� �ٲٱ�
		// File.WriteAllText(path, "abcdefg");
		Debug.Log(json);
	}

	[ContextMenu("Load")]
	public void Load()
	{
		string filePath = Path.Combine(path, "Data/Test.txt");
		if (File.Exists(filePath))
		{
			string text = File.ReadAllText(filePath);
			Debug.Log(text);
		}
		else
		{
			Debug.Log("������ ������ ����");
			// ���ν���
		}
	}
}

[Serializable] // ������ �ʿ��ؼ� ����ȭ�ؾ� �� ��
public class GameDate
{
	public int level;
	public int gold;
	public int exp;
}

