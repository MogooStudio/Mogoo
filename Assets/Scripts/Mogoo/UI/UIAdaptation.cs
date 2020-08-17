using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAdaptation : MonoBehaviour
{
    private static float SCREEN_WIDTH = 1920f;
    private static float SCREEN_HEIGHT = 1080f;
    
    private CanvasScaler _canvasScaler;
    private RectTransform _rectTransform;
    
    void Start()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        _rectTransform = transform.GetComponent<RectTransform>();
        
        InitAdaptation();
    }

    private void InitAdaptation()
    {
        var defaultScale = SCREEN_WIDTH / SCREEN_HEIGHT;
        var scale = Screen.width / (float)Screen.height;
        print(scale);
        if (scale > defaultScale)
        {
            _canvasScaler.matchWidthOrHeight = 1;
        }
        else if (scale < defaultScale)
        {
            _canvasScaler.matchWidthOrHeight = 0;
        }
        
        var root = Instantiate(Resources.Load<GameObject>("Test/LobbyWindow2"), transform);
        var rect = root.GetComponent<RectTransform>().rect;
        rect.width = (float) (rect.width * 1.5);
        rect.height = (float) (rect.height * 1.5);
        root.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
    }

    /// <summary>
    /// 按照宽高比缩放子控件
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private IEnumerator PosAdaptation(GameObject root)
    {
        yield return null;
        for (var i = 0; i < root.transform.childCount; i++)
        {
            var child = root.transform.GetChild(i);
            Vector2 localPosition = child.localPosition;
            var rect = _rectTransform.rect;
            child.localPosition = new Vector2(localPosition.x * rect.width/SCREEN_WIDTH, localPosition.y * rect.height/SCREEN_HEIGHT);
        }
    }
    
}
