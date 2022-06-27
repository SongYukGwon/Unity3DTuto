using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //정적메소드와 동적메서드의 차이 공부필
    Vector3 m_Movement;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    //쿼터리언으로 회전을 저장
    //제로 쿼터리언을 선언할떄 선언한는 방법. 
    Quaternion m_Rotation = Quaternion.identity;

    public float turnSpeed= 20f;
    // Start is called before the first frame update
    void Start()
    {
        //정적메서드 할당방법
        //GetComponet = MonoBehavior를 상속받아 그냥 사용가능
        // 제네릭 메서드 일반 파라미터와 타입 파라미터로 구성된 메서드,
        // 홑 화살 괄호 사이에 나열된 파라미터는 타입 파라미터 <>()
        m_Animator = GetComponent<Animator>();

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);

        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        //RotateTowards(Vector3 현재회전값, 목표회전값, 각도의 변화 속도?, 크기의 변화량)
        //turnSpeed에 deltatime을 곱하는 이유는 프레임당 으로 실행되기때문에 프레임 단위 차이를 벗어나기 위해서
        //
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        //LookRotatation 해당 파라미터 방향으로 바라보는 회전을 생성
        m_Rotation = Quaternion.LookRotation(desiredForward);


    }

   // 애니메이터에서 루트 모션이 적용되는 방식을변경하는 특수메서드
    void OnAnimatorMove()
    {
        //기존 position에 새로운 movement와 이동 벡터에 애니머이터의 deltaposition크기를 곱한값을 추가함
        //deltaPosition은 루트모션으로 인한 프레임당 포지션의 이동량을 말함. 
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        m_Rigidbody.MoveRotation(m_Rotation);
    }


}
