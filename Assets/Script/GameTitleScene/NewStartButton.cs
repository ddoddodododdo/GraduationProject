using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStartButton : MonoBehaviour
{
    private GameObject startPopUp;  //���ӿ�����Ʈ ���� ����
    void Start()
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("StartButton_PopUp").gameObject;  //��Ȱ��ȭ�� ������Ʈ����
    }
    public void NewStartButtonDown()  //On Click() �Լ�
    {
        startPopUp.gameObject.SetActive(true);  //startPopUp������Ʈ ��Ȱ��ȭ
    }
}
