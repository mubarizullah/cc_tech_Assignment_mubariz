using UnityEngine;

public class CameraRotMouse : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    [Header("Rotation Clamping")]
    public float minY = -60f;
    public float maxY = 60f;

    [Header("UI Area to Detect")]
    public RectTransform uiArea;

    [Header("Target for Y Rotation")]
    public Transform playerTransform;

    private float rotationX = 0f;
    private bool isDragging;

    void Update()
    {
        if (IsPointerOverUIArea())
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minY, maxY);

            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            if (playerTransform != null)
            {
                playerTransform.rotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y + mouseX, 0f);
            }
        }
    }

    private bool IsPointerOverUIArea()
    {
        if (uiArea == null)
            return false;

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiArea, Input.mousePosition, null, out localMousePosition);
        return uiArea.rect.Contains(localMousePosition);
    }
}
