using UnityEngine;

public class startYokulacakSegment : MonoBehaviour
{
    void Start()
    {
        // Sahnedeki "StartSegment" adlý GameObject'i bul ve 7 saniye sonra yok et
        GameObject startSegment = GameObject.Find("StartSegment");
        if (startSegment != null)
        {
            Destroy(startSegment, 9f);
        }
        else
        {
            Debug.LogWarning("StartSegment adlý GameObject bulunamadý.");
        }
    }

    void Update()
    {
        
    }
}
