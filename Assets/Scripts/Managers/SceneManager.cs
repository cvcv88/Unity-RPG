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
		// UnitySceneManager.LoadScene(sceneName); // �ε��ϴ� �������� �ٸ� �۾� �� ����
		StartCoroutine(LoadingRoutine(sceneName)); 
	}

	IEnumerator LoadingRoutine(string sceneName) // �ܰ躰 �ε� ����
	{
		// Fade Out
		float time = 0f;
		while(time < 0.5f)
		{
			// time += Time.deltaTime;
			time += Time.unscaledDeltaTime;
			fade.color = new Color(0, 0, 0, time * 2);
			yield return null; // �����Ӹ��� �����ֱ�
		}

		Time.timeScale = 0f;
		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName); // ��׶��忡�� �ε�, �ٵǾ����� ��ȯ
		oper.allowSceneActivation = true; // �ε� �ǰ��� ���Ҷ����� ��ȯ�ǰ� �� �� �ִ�.
		while (oper.isDone == false /*oper.progress < 0.9f*/) // oper�� �������� Ȯ�� / 0.9�� �Ϸ�Ǿ��� ���̱� ������ 0.9���� �������� ���ư���
		{
			// Debug.Log(oper.progress); // �ε� �󸶳� �Ǿ����� �� �� ����, 1(0.9) : �Ϸ�, 0 : ����
			loadingBar.value = Mathf.Lerp(0f, 0.5f, oper.progress);
			yield return null;
		}
		//yield return new WaitUntil( () => {return Input.GetKeyDown(KeyCode.Space); } );
		//oper.allowSceneActivation = true;

		BaseScene curScene = GetCurScene();
		yield return curScene.LoadingRoutine();
		Time.timeScale = 1f;
		loadingBar.value = 1f;

		// yield return new WaitForSeconds(0.1f); // ��� ���� ��

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