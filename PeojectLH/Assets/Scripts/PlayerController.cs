using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private bool isRun = false;

    // Use this for initialization
    void Start ()
    {
        animator = transform.Find("Model").GetComponent<Animator>();//transform.GetChild(1).GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            isRun = !isRun;
            Debug.Log("Run = " + isRun);
            AnimationController.Instance.setBool("Run", isRun);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {

        }
        else
        {
            Movement();
        }
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            animator.SetBool("Jump", false);

            moveDirection = (transform.forward * vertical) + (transform.right * horizontal);

            // 대각선 이동 보정
            if (vertical != 0.0f && horizontal != 0.0f)
            {
                moveDirection = moveDirection.normalized;
            }

            moveDirection *= (speed * (isRun ? 2 : 1));

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetBool("Jump", true);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        runAnimation(horizontal, vertical);
    }

    private void runAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    private bool IsCheckGrounded()
    {
        // CharacterController.IsGrounded가 true라면 Raycast를 사용하지 않고 판정 종료
        if (characterController.isGrounded) return true;
        // 발사하는 광선의 초기 위치와 방향
        // 약간 신체에 박혀 있는 위치로부터 발사하지 않으면 제대로 판정할 수 없을 때가 있다.
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        // 탐색 거리
        var maxDistance = 1.5f;
        // 광선 디버그 용도
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        // Raycast의 hit 여부로 판정
        // 지상에만 충돌로 레이어를 지정
        //Debug.Log(Physics.Raycast(ray, maxDistance, 9));
        return Physics.Raycast(ray, maxDistance, 9);
    }
}
