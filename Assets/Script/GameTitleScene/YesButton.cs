using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesButton : MonoBehaviour
{
    public void YesButtonDown() //On Click()�Լ�
    {
        SceneManager.LoadScene("Select Stage"); //Select Stage������ ��ȯ
    }
}
