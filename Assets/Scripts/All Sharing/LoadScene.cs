using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSelectStageScene() //On Click()�Լ�
    {
        SceneManager.LoadScene("Select Stage"); //Select Stage������ ��ȯ
    }
    public void LoadPuzzleStage01() //On Click()�Լ�
    {
        SceneManager.LoadScene("PuzzleStage01"); //Select Stage������ ��ȯ
    }
}
