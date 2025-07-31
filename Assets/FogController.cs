using UnityEngine;

public class FogController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Material fogMaterial;
    public float radius1 = 2f;
    public float radius2 = 1f;

    void Update()
    {
        if (!fogMaterial || !player1 || !player2) return;

        fogMaterial.SetVector("_Player1Pos", new Vector4(player1.position.x, player1.position.y, 0, 0));
        fogMaterial.SetVector("_Player2Pos", new Vector4(player2.position.x, player2.position.y, 0, 0));

        fogMaterial.SetFloat("_Radius1", radius1);
        fogMaterial.SetFloat("_Radius2", radius2);
    }
}
