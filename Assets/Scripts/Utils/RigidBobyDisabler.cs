using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBobyDisabler : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float decayTime = 1f;

    void Start()
    {
        StartCoroutine(Dis());
    }

    IEnumerator Dis()
    {
        yield return new WaitForSeconds(decayTime);
        _rigidbody.isKinematic = true;
    }
}
