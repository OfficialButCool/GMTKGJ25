// PlayerGroupManager.cs
using UnityEngine;

public class PlayerGroupManager : MonoBehaviour
{
    public PlayerRespawn player1;
    public PlayerRespawn player2;

    public void KillBothPlayers()
    {
        if (player1 != null)
            player1.Respawn();

        if (player2 != null)
            player2.Respawn();
    }
}
