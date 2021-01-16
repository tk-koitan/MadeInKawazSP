using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FickInput
{
    public class FrickInputManager : MonoBehaviour
    {
        private int index;
        private int subIndex;
        private int pIndex;
        string[,] mozi =
            {
                { "あ", "い", "う", "え", "お" },
                { "か", "き", "く", "け", "こ" },
                { "さ", "し", "す", "せ", "そ" },
                { "た", "ち", "つ", "て", "と" },
                { "な", "に", "ぬ", "ね", "の" },
                { "は", "ひ", "ふ", "へ", "ほ" },
                { "ま", "み", "む", "め", "も" },
                { "や", "「", "ゆ", "」", "よ" },
                { "ら", "り", "る", "れ", "ろ" },
                { "わ", "を", "ん", "ー", "お" },
            };
        string[,] mozi2 =
            {
                { "ぁ", "ぃ", "ぅ", "ぇ", "ぉ" },
                { "が", "ぎ", "ぐ", "げ", "ご" },
                { "ざ", "じ", "ず", "ぜ", "ぞ" },
                { "だ", "ぢ", "っ", "で", "ど" },
                { "な", "に", "ぬ", "ね", "の" },
                { "ば", "び", "ぶ", "べ", "ぼ" },
                { "ま", "み", "む", "め", "も" },
                { "ゃ", "「", "ゅ", "」", "ょ" },
                { "ら", "り", "る", "れ", "ろ" },
                { "わ", "を", "ん", "ー", "お" },
            };
        string[] mozi3 = { "ぱ", "ぴ", "ぷ", "ぺ", "ぽ" };
        [SerializeField]
        RectTransform subRect;
        [SerializeField]
        RectTransform[] buttonRects;
        [SerializeField]
        TextMeshProUGUI inputText;
        [SerializeField]
        string[] odai;
        [SerializeField]
        TextMeshProUGUI odaiText;

        // Start is called before the first frame update
        void Start()
        {
            //DebugTextManager.Display(() => Input.mousePosition.ToString());
            subRect.gameObject.SetActive(false);
            inputText.text = "";
            odaiText.text = odai[Random.Range(0, odai.Length)];
        }

        // Update is called once per frame
        void Update()
        {
            if (inputText.text == odaiText.text)
            {
                GameManager.Clear();
            }
        }

        public void ButtonDown(int n)
        {
            index = n;
            subRect.gameObject.SetActive(true);
            subRect.position = buttonRects[n].position;
        }

        public void SubButtonEnter(int n)
        {
            subIndex = n;
        }

        public void ButtonUp()
        {
            subRect.gameObject.SetActive(false);
            //Debug.Log(mozi[index, subIndex]);
            inputText.text += mozi[index, subIndex];
            pIndex = 0;
        }

        public void pDown()
        {
            if (inputText.text.Length == 0)
                return;
            if (pIndex == 0)
            {
                pIndex = 1;
                string s = inputText.text;
                s = s.Substring(0, s.Length - 1);
                s += mozi2[index, subIndex];
                inputText.text = s;
            }
            else if (pIndex == 1 && index == 5)
            {
                pIndex = 2;
                string s = inputText.text;
                s = s.Substring(0, s.Length - 1);
                s += mozi3[subIndex];
                inputText.text = s;
            }
            else if (pIndex == 1 && index == 3 && subIndex == 2)
            {
                pIndex = 2;
                string s = inputText.text;
                s = s.Substring(0, s.Length - 1);
                s += "づ";
                inputText.text = s;
            }
            else
            {
                pIndex = 0;
                string s = inputText.text;
                s = s.Substring(0, s.Length - 1);
                s += mozi[index, subIndex];
                inputText.text = s;
            }
        }

        public void Del()
        {
            if (inputText.text.Length == 0)
                return;
            string s = inputText.text;
            s = s.Substring(0, s.Length - 1);
            inputText.text = s;
        }

        public void Log(string s)
        {
            Debug.Log(s);
        }
    }
}
