using UnityEngine;

public class CursorPresenter : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;

    private void Awake()
    {
        // ���� ������ �����, �������� ��������.
        if (_cursorTexture != null)
        {
            Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void HideCursor()
    {
        // �������� ������
        Cursor.visible = false;
        // ��������� ������ � ������ ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

