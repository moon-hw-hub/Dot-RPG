using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float moveSpeed;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec; // ���� �ٶ󺸰� �ִ� ���� ���� ���� ������ �ʿ�
    GameObject scanObject; // ��ȣ�ۿ� ���� ������Ʈ

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
        h = GameManager.instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = GameManager.instance.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // Check Button Down or Up
        bool hDown = GameManager.instance.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = GameManager.instance.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = GameManager.instance.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = GameManager.instance.isAction ? false : Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown) // ���� �����ư�� �����ٸ�
            isHorizonMove = true;
        else if (vDown) // ���� ������ư�� �����ٸ�
            isHorizonMove = false;
        else if (hUp || vUp) // �����ư�̳� ������ư�� ��� ������ ���� �������� �Է��� ������ �ִٸ� ���� �̵� ���̶�� �Ǵ��ϴ� ����
            isHorizonMove = h != 0;
        /*if (h != 0)
            isHorizonMove = true;
        else
            isHorizonMove = false;*/

        // Animation
        if (anim.GetInteger("hAxisRaw") != h) // ���� �ִϸ����Ϳ� ����� ���� ��(hAxisRaw)�� ���� �Է°� h�� �ٸ���
        {
            anim.SetBool("isChange", true); // �ִϸ��̼� ���� ����
            anim.SetInteger("hAxisRaw", (int)h); // �ִϸ����Ϳ� ���� �� ������Ʈ
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false); // �ٲ��� �ʾ���

        //Direction
        if (vDown && v==1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        // Scan Object & Action
        if (Input.GetButtonDown("Jump") && scanObject != null) 
            //Debug.Log("this is : " + scanObject.name);
            GameManager.instance.Action(scanObject);

    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); // �밢�� �̵� ����
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
            Debug.Log("���� ������ ���� �� �����ϴ�.");
        }

    }
}
