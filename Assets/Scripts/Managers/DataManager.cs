using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR // true면 else 밑의 내용 없는 것 취급, false면 else 내용 실행
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

	// 어트리뷰트 attribute 함수 앞에 붙여주고, 이름 말해준다.
	// 컴포넌트에서 우클릭하면 메뉴에 생겨있다.
	[ContextMenu("SaveData")]
	public void SaveData()
	{
		// string path = Application.dataPath;
		// "C:\\Users\\메타플밍5기\\Unity-RPG\\Test.txt";
		// Debug.Log(path);

		Debug.Log(path);
		// if (Directory.Exists($"{Application.dataPath}/Data") == false)
		if (Directory.Exists(path) == false)
		{
			Debug.Log("폴더가 없어서 폴더를 생성해따");
			Directory.CreateDirectory(path);
		}
		string filePath = Path.Combine(path, "Test.txt");
		string json = JsonUtility.ToJson(gameData, true); // Json 직렬화
		Debug.Log(json);
		File.WriteAllText(filePath, json); // json text 결과를 저장하기
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
			// level = int.Parse(text); // int형으로 다시 바꿔서 유의미한 자료 만들기
			// Debug.Log(text);
			// Debug.Log("Load");

			string json = File.ReadAllText(filePath);
			JsonUtility.FromJson<GameData>(json);
		}
		else
		{
			Debug.Log("저장한 데이터 없음");
			// level = 1; // 저장된 파일 없다면 1로 지정
			// 새로 시작
		}
	}
}
