using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;
    GameBoard gameBoard;

    public int maxLevel;

    void Awake()
    {
        gameBoard = new GameBoard();
    }
    void Start()
    {
        Spawn();
    }
    Block GetBlock(int i)  //������
    {     
        //������Ʈ ����
        GameObject instant = Instantiate(BlockPrefab, gameBoard.blockGridPos[0,i], Quaternion.identity);
        Block instantBlock = instant.GetComponent<Block>(); //��ȯ���� Block�ϱ����� ��ȯ
        return instantBlock;
    }

    void NextBlock(int i) //���� ��
    {
        Block newBlock = GetBlock(i); //���Լ� ��Ʈ���ϱ����� ��������
        lastBlock = newBlock;
        lastBlock.manager = this;
        lastBlock.level = Random.Range(0, maxLevel); //maxLevel���� ���� �� ��
        lastBlock.gameObject.SetActive(true);

        StartCoroutine(WaitNext(i));
    }

    /*void test()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        for (int j = 0; j < blocks.Length; j++)
        {
            Vector3 newPos = blocks[j].transform.position;
            newPos.y += 1.12f;
            blocks[j].transform.position = newPos;
        }
    }*/

    IEnumerator WaitNext(int i)
    {
        yield return new WaitForSeconds(4f);

        NextBlock(i);
    }
    void StartSpawn()
    {

    }

    void Spawn()
    {
        for (int i = 0; i < 6; i++)
            {
                NextBlock(i);
            }
    }
}
