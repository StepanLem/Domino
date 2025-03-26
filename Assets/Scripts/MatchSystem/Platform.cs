using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform LeftTopPoint;
    public Transform RightTopPoint;
    public Renderer Renderer;

    public void SetParams(Vector3 leftTopCornerPosition, float lengthMultiplier, float heightMultiplier, Color color)
    {
        //������
        this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(lengthMultiplier, heightMultiplier, 1));

        //�������
        var offset = leftTopCornerPosition - LeftTopPoint.position;
        this.transform.position += offset;

        Renderer.material.color = color;
    }
}
