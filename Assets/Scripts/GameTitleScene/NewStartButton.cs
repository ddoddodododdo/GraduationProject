using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStartButton : MonoBehaviour
{
    private GameObject startPopUp;  //���ӿ�����Ʈ ���� ����
    public void NewStartButtonDown()  //On Click() �Լ�
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("Start_PopUp").gameObject;  //��Ȱ��ȭ�� ������Ʈ����
        startPopUp.SetActive(true);  //startPopUp������Ʈ ��Ȱ��ȭ
    }
}
