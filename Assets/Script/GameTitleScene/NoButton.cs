using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    private GameObject startPopUp; //���ӿ�����Ʈ ���� ����
    public void NoButtonDown() //On Click() �Լ�
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("Start_PopUp").gameObject; //��Ȱ��ȭ�� ������Ʈ����
        startPopUp.SetActive(false); //startPopUp������Ʈ ��Ȱ��ȭ
    }
}
