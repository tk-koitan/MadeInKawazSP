using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Camera targetCamera; //対象とするカメラ
    private Vector2 aspectVec = new Vector2(1080, 1920); //目的解像度

    private void Awake()
    {
    }

    void Update()
    {
        targetCamera = Camera.main;
        if (targetCamera == null)
        {
            TryGetComponent(out targetCamera);
        }
        if (targetCamera == null) return;
        var screenAspect = Screen.width / (float)Screen.height; //画面のアスペクト比
        var targetAspect = aspectVec.x / aspectVec.y; //目的のアスペクト比

        var magRate = targetAspect / screenAspect; //目的アスペクト比にするための倍率

        var viewportRect = new Rect(0, 0, 1, 1); //Viewport初期値でRectを作成

        if (magRate < 1)
        {
            viewportRect.width = magRate; //使用する横幅を変更
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;//中央寄せ
        }
        else
        {
            viewportRect.height = 1 / magRate; //使用する縦幅を変更
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;//中央余生
        }

        targetCamera.rect = viewportRect; //カメラのViewportに適用
    }
}
