using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Image farImage;
    public Image closeImage;
    public Image veryCloseImage;

    public Transform playerTransform;

    public float farDistance = 10f;
    public float closeDistance = 5f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > farDistance)
        {
            farImage.gameObject.SetActive(true);
            closeImage.gameObject.SetActive(false);
            veryCloseImage.gameObject.SetActive(false);
        }
        else if (distanceToPlayer > closeDistance)
        {
            farImage.gameObject.SetActive(false);
            closeImage.gameObject.SetActive(true);
            veryCloseImage.gameObject.SetActive(false);
        }
        else
        {
            farImage.gameObject.SetActive(false);
            closeImage.gameObject.SetActive(false);
            veryCloseImage.gameObject.SetActive(true);
        }
    }
}

