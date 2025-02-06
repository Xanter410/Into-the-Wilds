using UnityEngine;

public class CursorPresenter : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;

    private void Awake()
    {
        // Если спрайт задан, получаем текстуру.
        if (_cursorTexture != null)
        {
            Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void HideCursor()
    {
        // Скрываем курсор
        Cursor.visible = false;
        // Блокируем курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

