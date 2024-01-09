using UnityEngine;

public class ScreenSizeInfo : MonoBehaviour
{
    public static Vector2 ScreenSize => ScreenToWorld(new Vector3(Screen.width, Screen.height));

    public static Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        var rayObject = Camera.main.ScreenPointToRay(screenPosition);

        var rayDirection = rayObject.direction;
        var rayOrigin = rayObject.origin;

        Vector3 planeNormal = new Vector3(0, 0, 1);
        Vector3 planePoint = new Vector3(0, 0, 0);

        float dotProduct = Vector3.Dot(rayDirection, planeNormal);

        float distance = Vector3.Dot(planePoint - rayOrigin, planeNormal) / dotProduct;

        Vector3 result = rayOrigin + distance * rayDirection;
        return result;
    }
}
