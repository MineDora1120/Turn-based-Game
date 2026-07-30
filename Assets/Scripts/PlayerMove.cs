using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb; //물리 엔진인 Rigidbody2D를 사용하기 위해 선언
    private Animator anim; //애니메이션을 관리하기 위해 선언
    private SpriteRenderer spr; //스프라이트를 관리하기 위해 선언

    public float speed;
    private float moveInput;

    private void Start()
    {
        // Rigidbody2D 컴포넌트를 가져옵니다.
        rb = GetComponent<Rigidbody2D>();

        // Animator 컴포넌트를 가져옵니다.
        anim = GetComponent<Animator>();

        // SpriteRenderer 컴포넌트를 가져옵니다.
        spr = GetComponent<SpriteRenderer>();
    }

    // 이동 키가 눌렸을 때
    public void OnMove(InputValue value)
    {
        // 매개변수에 들어오는 값: 1, -1 (왼쪽, 오른쪽 이동)

        // InputValue에서 float 값을 가져옵니다.
        moveInput = value.Get<float>();

        // 왼쪽으로 이동할 때 스프라이트를 뒤집습니다.
        spr.flipX = moveInput < 0;
    }

    //FixedUpdate는 물리 연산을 처리하는 함수이므로, 이동 속도를 애니메이션에 반영하기 위해 사용합니다.
    private void FixedUpdate()
    {
        // 이동 속도를 애니메이션에 반영합니다.
        anim.SetFloat("Speed", rb.linearVelocity.magnitude);

        // 이동 키의 입력 값을 가져와서 Rigidbody2D의 속도를 설정합니다.
        rb.linearVelocityX = moveInput * speed;
    }
}