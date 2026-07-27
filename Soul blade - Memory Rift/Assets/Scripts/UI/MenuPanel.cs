using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public bool pausedGame;

    public virtual void Open() { }
    public virtual void Close() { }
}