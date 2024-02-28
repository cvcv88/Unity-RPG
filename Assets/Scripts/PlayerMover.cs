using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed;

    private Vector3 moveDir;

	private void Update()
	{
        Move();
	}

	private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir.x = input.x;
        moveDir.z = input.y;

        animator.SetFloat("MoveSpeed",moveDir.magnitude); // MoveSpeed, ������ŭ �̵�
    }

    private void Move()
    {
        // 1. �����̱�
        // controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 2. �ٶ󺸴� �������� �����̱� - �߸��� ���(���� �������� �������� �ʾƼ� ���߿� �ߴ� ������ �ִ�)
        // Vector3 forwardDir = Camera.main.transform.forwardDir; // �� �� ī�޶� �������� ����
        // Vector3 rightDir = Camera.main.transform.right; // ��(������) �� ī�޶� �������� ����
		// controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
		// controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);

		// 3. �ٶ󺸴� �������� �����̱�
        // ī�޶��� ���� �������� ���������� y = 0, ���� �����ϰ� ���������ϰ�,
        // ī�޶��� ������ ���� ���ϰ� �����̰ų� ������ ������ �� ������ ũ�⸦ 1�� ������־�� �Ѵ�
		Vector3 forwardDir = Camera.main.transform.forward; // ī�޶� �������� ���� ������ �������� ������ ���� (y = 0, ũ�� 1)
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;
		Vector3 rightDir = Camera.main.transform.right; // ī�޶� �������� ���� ������ �������� ��(������)���� ���� (y = 0, ũ�� 1)
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;

		controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
		controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);


        // 1. ���� �������� �ٶ󺸵��� �ϱ� - �߸��� ���(�Է��� ���� ���� zero ���ͷ� lookRotation���� ����� �ִ�)
        // Quaternion lookRotation = Quaternion.LookRotation(forwardDir * moveDir.z + rightDir * moveDir.x);
        // transform.rotation = lookRotation;

        // 2. ���� �������� �ٶ󺸵��� �ϱ�
        // �Է��� ���� ���� lookRotation�� ���������� �ʵ��� �ؾ��Ѵ�
        Vector3 lookDir = forwardDir * moveDir.z + rightDir * moveDir.x;
        if(lookDir.sqrMagnitude > 0) // if(lookDir != Vector3.zero) <= �̰� �� ����ȭ Good
        {
			Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            // 1. �ʹ� �ް��� ȸ���Ѵ�
            // transform.rotation = lookRotation;

            // 2. �ް��� ȸ���ϴ� ���� �ذ�
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
		}

        // Vector3.Project / ProjectOnPlane


	}
}
