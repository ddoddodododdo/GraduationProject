using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDownButton : MonoBehaviour
{
    public GameObject PopUp; //���ӿ�����Ʈ ���� ����
    public void NoButtonDown() //On Click() �Լ�
    {
        PopUp.SetActive(false); //startPopUp������Ʈ ��Ȱ��ȭ
    }
}
