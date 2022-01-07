using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nishida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickButton()
    {
        GameManager.Clear();
        Debug.Log("Nishida");
    }
}
