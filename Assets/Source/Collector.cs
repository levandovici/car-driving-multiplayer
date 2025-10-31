using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Money")
        {
            SaveLoadManager.PlayerData.money += 100;

            SaveLoadManager.Save();

            Destroy(other.gameObject);
        }
    }
}
