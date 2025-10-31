using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;



    private void Update()
    {
        transform.Rotate(new Vector3(0f, _speed * Time.deltaTime, 0f), Space.World);
    }
}
