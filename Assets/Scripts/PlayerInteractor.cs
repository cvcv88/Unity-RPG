using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
	[SerializeField] bool debug;
	[SerializeField] float range; // ��ȣ�ۿ� ����

	Collider[] colliders = new Collider[20];
	private void Interact()
	{
		int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
		// �ܹ߼� �ƴ� �������̴ϱ� NonAlloc ���̱�
		// �÷��̾� ��ġ���� ������ŭ �浹ü

		for (int i = 0; i < size; i++)
		{
			IInteractable interactable = colliders[i].GetComponent<IInteractable>();
			if(interactable != null)
			{
				interactable.Interact(this); // �ϳ��� ��ȣ�ۿ��ϸ� ������
				break;
			}
		}
	}
	private void OnInteract(InputValue value)
	{
		Interact();
	}

	private void OnDrawGizmosSelected()
	{
		if (debug == false)
			return;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
