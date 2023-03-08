using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public Snap snap = new Snap();

    public int level;
    public bool select = false;
    public bool isMerge;

    Vector3 mousePos;

    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D box;


    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable() //��ũ��Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �̺�Ʈ�Լ�
    {
        anim.SetInteger("Level", level); //�ִϸ����� int�� �Ķ����
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.simulated = false;
    }
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);
    }
    public void OnMouseUp()
    {
        //BlockGroup�߿� �� ��°�� �������
        Debug.Log(snap.BlockGroupPos[0].x);

        select = false;
        rigid.simulated = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            Block other = collision.gameObject.GetComponent<Block>();

            if(level == other.level && !isMerge && !other.isMerge && level < 7)
            {
                //���� ��� ��ġ�� ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;
                //1. ���� �Ʒ��� ������
                //2. ������ ������ ��, ���� �����ʿ� ���� ��
                if(meY < otherY || (meY == otherY && meX > otherX))
                {
                    //������ �����
                    other.Hide(transform.position);
                    //���� ������
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos) //����� �Լ�
    {
        isMerge = true;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(HideRoutine(targetPos)); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator HideRoutine(Vector3 targetPos) //���� �ִϸ��̼�
    {
        int frameCount = 0;

        while (frameCount < 20) //�̵�
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f); //target���� �̵�
            yield return null; //�̰� ������ �� ������ �ȿ��� �ݺ����� ���Ƽ� �ǹ�X
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void LevelUp() //�������� ���� �Լ�
    {
        isMerge = true;

        rigid.velocity = Vector2.zero; //������ �� ���ص� �� �ִ� �����ӵ� ���� �̵��ӵ�=velocity, 2d�� vector2
        rigid.angularVelocity = 0; //ȸ���ӵ� �ʱ�ȭ, +�ð�, -�ݽð�

        StartCoroutine(LevelUpRoutine()); //�ִϸ��̼��ֱ� ���� �ڷ�ƾ
    }

    IEnumerator LevelUpRoutine() //������ �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //maxLevel ����

        isMerge = false;
    }
}
