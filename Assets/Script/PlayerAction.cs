using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float moveSpeed;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec; // 현재 바라보고 있는 방향 값을 가진 변수가 필요
    GameObject scanObject; // 상호작용 중인 오브젝트

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Check Button Down or Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown) // 만약 수평버튼을 눌렀다면
            isHorizonMove = true;
        else if (vDown) // 만약 수직버튼을 눌렀다면
            isHorizonMove = false;
        else if (hUp || vUp) // 수평버튼이나 수직버튼을 떼어도 여전히 수평 방향으로 입력이 들어오고 있다면 수평 이동 중이라고 판단하는 로직
            isHorizonMove = h != 0;
        /*if (h != 0)
            isHorizonMove = true;
        else
            isHorizonMove = false;*/

        // Animation
        if (anim.GetInteger("hAxisRaw") != h) // 현재 애니메이터에 저장된 수평 값(hAxisRaw)과 실제 입력값 h가 다르면
        {
            anim.SetBool("isChange", true); // 애니메이션 상태 변경
            anim.SetInteger("hAxisRaw", (int)h); // 애니메이터에 수평 값 업데이트
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false); // 바뀌지 않았음

        //Direction
        if (vDown && v==1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        // Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null) 
            Debug.Log("this is : " + scanObject.name);

    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); // 대각선 이동 방지
        rigid.linearVelocity = moveVec * moveSpeed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec*0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BorderLine")
        {
            Debug.Log("범위 밖으로 나갈 수 없습니다.");
        }

    }
}
