using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public int level;
    public bool select = false;
    public bool isMerge;

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
        anim.SetInteger("Level", level);
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.simulated = false;
    }
    public void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);
    }
    public void OnMouseUp()
    {
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

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while (frameCount < 20)
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
            yield return null;
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel);

        isMerge = false;
    }
}
