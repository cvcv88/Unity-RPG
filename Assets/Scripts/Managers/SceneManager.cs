using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
	[SerializeField] Image fade;
	[SerializeField] Slider loadingBar;

	private BaseScene curScene;

	public BaseScene GetCurScene()
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene;
	}

	public T GetCurScene<T>() where T : BaseScene
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene as T;
	}
	private void Update()
	{
		/*if (Input.GetKeyDown(KeyCode.Space))
        {
			Debug.Log(GetCurScene().gameObject.name);
        }*/
	}

	public void LoadScene(string sceneName)
	{
		// UnitySceneManager.LoadScene(sceneName); // 로딩하는 과정에서 다른 작업 다 멈춤
		StartCoroutine(LoadingRoutine(sceneName)); 
	}

	IEnumerator LoadingRoutine(string sceneName) // 단계별 로딩 과정
	{
		// Fade Out
		float time = 0f;
		while(time < 0.5f)
		{
			// time += Time.deltaTime;
			time += Time.unscaledDeltaTime;
			fade.color = new Color(0, 0, 0, time * 2);
			yield return null; // 프레임마다 돌려주기
		}

		Time.timeScale = 0f;
		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName); // 백그라운드에서 로딩, 다되었을때 전환
		oper.allowSceneActivation = true; // 로딩 되고나서 원할때에만 전환되게 할 수 있다.
		while (oper.isDone == false /*oper.progress < 0.9f*/) // oper가 끝났는지 확인 / 0.9가 완료되었을 때이기 때문에 0.9보다 작을때만 돌아가게
		{
			// Debug.Log(oper.progress); // 로딩 얼마나 되었는지 알 수 있음, 1(0.9) : 완료, 0 : 시작
			loadingBar.value = Mathf.Lerp(0f, 0.5f, oper.progress);
			yield return null;
		}
		//yield return new WaitUntil( () => {return Input.GetKeyDown(KeyCode.Space); } );
		//oper.allowSceneActivation = true;

		BaseScene curScene = GetCurScene();
		yield return curScene.LoadingRoutine();
		Time.timeScale = 1f;
		loadingBar.value = 1f;

		// yield return new WaitForSeconds(0.1f); // 잠깐 멈춘 거

		// Fade In
		time = 0.5f;
		while (time > 0f)
		{
			// time -= Time.deltaTime;
			time -= Time.unscaledDeltaTime;
			fade.color = new Color(0, 0, 0, time * 2);
			yield return null;
		}
	}

	public void SetLoadingBarValue(float value)
	{
		loadingBar.value = value;
	}
}