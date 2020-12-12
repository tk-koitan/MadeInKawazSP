using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoshokuManager : MonoBehaviour
{
    private int ballCount;
    [SerializeField]
    private GameObject original;
    [SerializeField]
    float width;
    [SerializeField]
    float height;
    [SerializeField]
    float offsetY;
    [SerializeField]
    int ballMaxCount;
    [SerializeField]
    TextMeshProUGUI textMesh;
    private int targetCount;
    private float finishTime = 7.8f;
    private float t = 0f;
    private float t2 = 0f;
    // Start is called before the first frame update
    void Start()
    {
        ballCount = 1;
        targetCount = Random.Range(5, ballMaxCount);
        textMesh.text = "<size=200%><color=red>" + targetCount.ToString() + "</color></size>個にして!";
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (ballCount == targetCount)
        {
            t2 += Time.deltaTime;
            if (t2 >= 1f || t >= finishTime)
            {
                GameManager.Clear();
            }
        }
    }

    public void PlusOne()
    {
        GenerateBall(1);
    }

    public void MulTwo()
    {
        GenerateBall(ballCount);
    }

    private void GenerateBall(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            //限界
            if (ballCount >= ballMaxCount)
            {
                break;
            }
            Vector3 pos = new Vector3(Random.Range(-width, width), Random.Range(-height, height) + offsetY);
            Instantiate(original, pos, Quaternion.identity);
            ballCount++;
        }
    }
}
