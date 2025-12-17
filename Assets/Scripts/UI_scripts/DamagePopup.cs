using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatSpeed = 1.5f;
    public float lifetime = 1f;

    private float timer;

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifetime)
            Destroy(gameObject);
    }

    public void SetDamage(float dmg)
    {
        text.text = dmg.ToString();
    }
}
