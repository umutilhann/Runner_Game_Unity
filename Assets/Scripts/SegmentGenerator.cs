using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    
    [SerializeField] int zPos = 50;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum;

    // Ýlk 3 oluþturulan nesneyi saklamak için
    private List<GameObject> firstThree = new List<GameObject>();
    private int createdCount = 0;
    private bool firstThreeScheduled = false;

    void Update()
    {
        if(creatingSegment == false)
        {
            creatingSegment = true;
            StartCoroutine(segmentGen());
        }
    }

    IEnumerator segmentGen()
    {
        segmentNum = Random.Range(0, segment.Length);
        GameObject created = Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);

        // Ýlk 3 nesneyi listele
        if (!firstThreeScheduled)
        {
            createdCount++;
            if (createdCount <= 3)
            {
                firstThree.Add(created);
                if (createdCount == 3)
                {
                    firstThreeScheduled = true;
                    // Üçüncü obje oluþtuktan sonra her birini 7s aralýkla yok et
                    StartCoroutine(DestroyFirstThreeSequentially(10f));
                }
            }
        }

        zPos += 50;
        yield return new WaitForSeconds(5);
        creatingSegment = false;
    }

    IEnumerator DestroyFirstThreeSequentially(float interval)
    {
        // Her bir obje için önce bekle, sonra yok et (sýralý, 7s aralýklarla)
        for (int i = 0; i < firstThree.Count; i++)
        {
            yield return new WaitForSeconds(interval);
            var go = firstThree[i];
            if (go != null)
                Destroy(go);
        }

        firstThree.Clear();
    }
}
