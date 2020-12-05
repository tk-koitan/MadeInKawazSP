using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Needle;
    const int needleSum = 4;
    // Start is called before the first frame update
    void Start()
    {
        NeedleGenerator();
    }

    void NeedleGenerator()
    {
        int leftSum = 0, rightSum = 0;
        for (int i = 0; i < needleSum; i++)
        {
            if (IsNeedleRight())
            {
                rightSum++;
                GameObject obj = Instantiate(Needle, new Vector2(1.7f * (1.5f - 1 * i), -5f), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5f / needleSum);
            }
            else
            {
                leftSum++;
                GameObject obj = Instantiate(Needle, new Vector2(1.7f * (1.5f - 1 * i), 5f), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f / needleSum);
            }
        }

        bool IsNeedleRight()
        {
            if (leftSum == needleSum - 1) return true;
            if (rightSum == needleSum - 1) return false;

            if (Random.Range(0, 2) == 1) return true;
            else return false;
        }
    }
}