using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Snap snap;
    public Block lastBlock;
    public GameObject BlockPrefab;
    public Transform BlockGroup;

    public int maxLevel;

    void Awake()
    {
    }
    void Start()
    {
        snap = new Snap();
        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
    Block GetBlock(int i)        //������
    {     
        GameObject instant = Instantiate(BlockPrefab, snap.gridPos[i], Quaternion.identity); //������Ʈ ����
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

        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        yield return new WaitForSeconds(10f);

        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
}
