using UnityEngine;

public class RollAndMoveAnimation : MonoBehaviour
{
    public float rollSpeed = 1f;         // Vitesse du roulis (rotation)
    public float maxRollAngle = 10f;       // Amplitude maximale du roulis
    public float moveSpeed = 1f;          // Vitesse de la translation
    public float moveRange = 0.2f;          // Amplitude du mouvement de translation (distance maximale sur l'axe X)

    private Vector3 startPosition;        // Position initiale de l'objet

    private void Start()
    {
        // Stocker la position de départ pour le mouvement relatif
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcul du roulis (rotation continue)
        float rollAmount = Mathf.Sin(Time.time * rollSpeed) * maxRollAngle;

        // Appliquer la rotation autour de l'axe Z
        transform.rotation = Quaternion.Euler(0f, 0f, rollAmount);

        // Calcul du mouvement (translation oscillante)
        float moveAmount = Mathf.Sin(Time.time * moveSpeed) * moveRange;

        // Appliquer la translation sur l'axe X (ou Y selon le besoin)
        transform.position = startPosition + new Vector3(moveAmount, 0f, 0f);
    }
}