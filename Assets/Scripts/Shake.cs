using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;

    }

    private IEnumerator ShakeSequence(float offset, float duration)
    {
        //yield return null;                            -wait for 1 frame
        //yield return new WaitForSeconds(offset);      -wait for offset frame

        float durationPassed = 0f;
        while (durationPassed < duration)
        {
            DoShake(offset);
            durationPassed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition;


    }

    public void StartShake(float offset, float duration)
    {
        StopShake();
        StartCoroutine(ShakeSequence(offset, duration));
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = initialPosition;
    }

    private void DoShake(float maxOffset)
    {
        float xOffset = Random.Range(-maxOffset, maxOffset);
        float yOffset = Random.Range(-maxOffset, maxOffset);
        transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0f);
    }
}
