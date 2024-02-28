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

        animator.SetFloat("MoveSpeed",moveDir.magnitude); // MoveSpeed, 누른만큼 이동
    }

    private void Move()
    {
        // 1. 움직이기
        // controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 2. 바라보는 방향으로 움직이기 - 잘못된 방법(땅을 기준으로 움직이지 않아서 공중에 뜨는 문제가 있다)
        // Vector3 forwardDir = Camera.main.transform.forwardDir; // 앞 을 카메라 기준으로 설정
        // Vector3 rightDir = Camera.main.transform.right; // 옆(오른쪽) 을 카메라 기준으로 설정
		// controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
		// controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);

		// 3. 바라보는 방향으로 움직이기
        // 카메라의 기준 방향으로 움직이지만 y = 0, 땅과 평행하게 움직여야하고,
        // 카메라의 각도에 따라 급하게 움직이거나 느리게 움직일 수 있으니 크기를 1로 만들어주어야 한다
		Vector3 forwardDir = Camera.main.transform.forward; // 카메라 기준으로 땅과 평행한 방향으로 앞으로 설정 (y = 0, 크기 1)
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;
		Vector3 rightDir = Camera.main.transform.right; // 카메라 기준으로 땅과 평행한 방향으로 옆(오른쪽)으로 설정 (y = 0, 크기 1)
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;

		controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
		controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);


        // 1. 누른 방향으로 바라보도록 하기 - 잘못된 방법(입력이 없을 때는 zero 벡터로 lookRotation으로 만들고 있다)
        // Quaternion lookRotation = Quaternion.LookRotation(forwardDir * moveDir.z + rightDir * moveDir.x);
        // transform.rotation = lookRotation;

        // 2. 누른 방향으로 바라보도록 하기
        // 입력이 없을 때는 lookRotation을 설정해주지 않도록 해야한다
        Vector3 lookDir = forwardDir * moveDir.z + rightDir * moveDir.x;
        if(lookDir.sqrMagnitude > 0) // if(lookDir != Vector3.zero) <= 이게 더 최적화 Good
        {
			Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            // 1. 너무 급격히 회전한다
            // transform.rotation = lookRotation;

            // 2. 급격히 회전하는 문제 해결
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
		}

        // Vector3.Project / ProjectOnPlane


	}
}
