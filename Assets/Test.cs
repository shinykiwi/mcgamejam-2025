using System;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    public RectTransform uiRect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //uiRect.DOPunchScale(new Vector3(2, 2, 2), 20, 5, 1);

            uiRect.DOPunchAnchorPos(Vector2.down * 4, 0.8f, 5);
        }
    }
}
