using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Block lastBlock;
    public GameObject BlockPrefab;
    public Transform[] BlockGroup;

    public int maxLevel;

    void Awake()
    {
    }
    void Start()
    {
        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
    Block GetBlock(int i)        //������
    {     
        GameObject instant = Instantiate(BlockPrefab, BlockGroup[i]); //������Ʈ ����
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

    IEnumerator WaitNext(int i)
    {
        yield return new WaitForSeconds(10f);

        NextBlock(i);
    }
}
