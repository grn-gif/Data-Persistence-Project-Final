using UnityEngine;

public class DeathZone : MonoBehaviour //Beta Script Folder
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
