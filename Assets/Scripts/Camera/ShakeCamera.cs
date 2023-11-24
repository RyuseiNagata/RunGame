using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // �R���[�`���ŃJ�������V�F�C�N����
    public void BeginShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 pos = transform.localPosition;

        float elapsed = 0f; // �o�ߎ���

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}
