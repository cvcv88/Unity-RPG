using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
	// public GameObject player;
	[SerializeField] Monster monsterPrefab;
	[SerializeField] Transform spawnPoint;
	[SerializeField] int count;
	public override IEnumerator LoadingRoutine()
	{
		yield return null;
		/*Debug.Log("GameScene Load Start");
		// fake loading
		yield return new WaitForSecondsRealtime(1f);
		Manager.Scene.SetLoadingBarValue(0.6f);
		Debug.Log("Player Spawn");
		yield return new WaitForSecondsRealtime(1f);
		Manager.Scene.SetLoadingBarValue(0.7f);
		Debug.Log("Object pool Prepare");

		for(int i = 0; i < count; i++)
		{
			Vector2 randomOffset = Random.insideUnitCircle * 3;
			Vector3 spawnPos = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);
			Monster monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

			Debug.Log("Monster Spawn");
			yield return null;
		}
		Manager.Scene.SetLoadingBarValue(0.8f);
		yield return new WaitForSecondsRealtime(1f);
		Manager.Scene.SetLoadingBarValue(0.9f);
		Debug.Log("GameScene Loading Finish");*/
		//player = Instantiate(player);

		//Transform target = Manager.Scene.GetCurScene<GameScene>().player.transform;
	}

	public void ToTitleScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}
}
