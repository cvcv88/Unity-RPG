using System.Collections;

public class TitleScene : BaseScene
{
	public void GameSceneLoad()
	{
		Manager.Scene.LoadScene("GameScene");
	}

	public void NewGame()
	{

	}

	public void ContinueGame()
	{

	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null; // �ƹ��͵� ���ϱ�, ���� �� ��� ����
	}
}
