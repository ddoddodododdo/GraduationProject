using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    [Header("scale speed")]  //�ν�����â ����
    [Range(1f, 10f)] public float size = 1f;   //������Ʈ ũ��
    [Range(1f, 10f)] public float scaleSpeed = 1f; //ũ�⺯�� �ӵ�
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = new Vector3(Mathf.Cos(timer* scaleSpeed) +size,
                                            Mathf.Cos(timer* scaleSpeed) +size, 0); //ũ�� ���� �ﰢ�Լ��� ���� �ݺ�
    }
}
