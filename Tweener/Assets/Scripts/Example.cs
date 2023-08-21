using System.Collections;
using System.Collections.Generic;
using Tweener;
using UnityEngine;

public class Example : MonoBehaviour
{
    public GameObject go;

    private Tweener.Tweener _scale;
    // Start is called before the first frame update
    void Start()
    {
        go.transform.DoMove(new Vector3(10, 0, 0), 2f).SetEase(EaseMode.EaseInOutBack);
        _scale = go.transform.DoScale(new Vector3(2, 2, 2), 2f);

        StartCoroutine(_WaitToKill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator _WaitToKill()
    {
        yield return new WaitForSeconds(1);
        _scale.Kill();
    }
}
