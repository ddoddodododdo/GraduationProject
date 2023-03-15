using System;
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
        
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
        //    RigidbodyConstraints2D.FreezePositionY|
        RigidbodyConstraints2D.FreezeRotation; //������Ʈ Rotation��, x�� ����
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
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //��������
        if (transform.position.x >= 3.0)
        {
            transform.position = new Vector3(3.0f, transform.position.y, 0);
        }
        if(transform.position.x <= -3.0)
        {
            transform.position = new Vector3(-3.0f, transform.position.y, 0);
        }
        if (transform.position.y >= 3.45)
        {
            transform.position = new Vector3(transform.position.x, 3.45f, 0);
        }
    }
    public void OnMouseUp()
    {
        //�������
        Snap();

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

    void Snap()     //�����Լ�
    {
        float[] distanceArray = new float[6];  //������Ʈ �Ÿ��� ���κ� �Ÿ����̸� ������ �迭����
        float min;

        for (int i = 0; i < snap.BlockGroupPos.Length; i++)  //������Ʈ �Ÿ���� �� �迭�� �� ����
        {
            distanceArray[i] = Vector3.Distance(transform.position, snap.BlockGroupPos[i]);
        }

        min = distanceArray[0];     //min �⺻������

        for (int i = 1; i < snap.BlockGroupPos.Length; i++)     //�迭���� ���� ���� �� ã�� = ���� ����� ����ã��
        {
            if (min > distanceArray[i])
            {
                min = distanceArray[i];
            }
        }
        transform.position = new Vector3(snap.BlockGroupPos[Array.IndexOf(distanceArray, min)].x,
                                                transform.position.y, 0); //������Ʈ ��ġ�� ���� ����� ������ x���� ����
    }
}
