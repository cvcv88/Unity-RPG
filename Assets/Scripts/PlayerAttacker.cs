using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
	[SerializeField] bool debug;
	[SerializeField] Animator animator;
	// [SerializeField] Weapon weapon;

	[SerializeField] LayerMask layerMask;
	[SerializeField] float range;
	[SerializeField] float angle;
	[SerializeField] int damage;

	private float cosRange;

	private void Awake()
	{
		cosRange = Mathf.Cos(angle * Mathf.Deg2Rad); // ȣ���� : ���̷� ǥ��
	}

	private void Attack()
	{
		// animator.SetTrigger("Attack1");
		int rand = Random.Range(0, 2); // 0, 1
		if (rand == 0)
		{
			animator.SetTrigger("Attack1");
		}
		else
		{
			animator.SetTrigger("Attack2");
		}
	}

	/*public void EnableWeapon() // 10�� event
	{
		weapon.EnableWeapon();
	}
	public void DisableWeapon() // 20�� event
	{
		weapon.DisableWeapon();
	}
*/

	Collider[] colliders = new Collider[20];

	private void AttackTiming()
	{
		int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
		for (int i = 0; i < size; i++)
		{
			Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
			// (�浹ü�� ��ġ - ���� �� ��ġ)�� ���� = Ÿ�ٱ����� ����
			// if (Vector3.Angle(transform.forward, dirToTarget) < angle) // ���� �� ������ ���� ���(�ﰢ�Լ��� ���� �ӵ� ����)
			//	continue;

			if (Vector3.Dot(dirToTarget, transform.forward) < cosRange) // ���� : Dot
				continue;
			IDamagable damagable = colliders[i].GetComponent<IDamagable>();
			damagable?.TakeDamage(damage);
		}
	}
	private void OnAttack(InputValue value)
	{
		if (EventSystem.current.IsPointerOverGameObject()) // UI ���� ���� Į �ֵθ��� �ʰ�
			return;
		Attack();
	}

	private void OnDrawGizmosSelected()
	{
		if (debug == false)
			return;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
