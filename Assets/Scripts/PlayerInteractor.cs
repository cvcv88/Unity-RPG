using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
	[SerializeField] bool debug;
	[SerializeField] float range; // 상호작용 범위

	Collider[] colliders = new Collider[20];
	private void Interact()
	{
		int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
		// 단발성 아닌 여러번이니까 NonAlloc 붙이기
		// 플레이어 위치에서 범위만큼 충돌체

		for (int i = 0; i < size; i++)
		{
			IInteractable interactable = colliders[i].GetComponent<IInteractable>();
			if(interactable != null)
			{
				interactable.Interact(this); // 하나라도 상호작용하면 끝내기
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
