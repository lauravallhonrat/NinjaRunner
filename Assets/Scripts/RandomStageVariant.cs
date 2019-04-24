using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStageVariant : MonoBehaviour
{
    [SerializeField]
    int percentageToBeActivated = 50;

    [SerializeField]
    bool ReScaleActive = true;

    [SerializeField]
    bool Rotation = true;


    void Start()
    {
        if (ActivateObjRandom())
        {
            if (Rotation)
                RotateObjRandom();

            if (ReScaleActive)
               ScaleObjRandom();
        }
    }

    private bool ActivateObjRandom()
    {
        int _result = Random.Range(1, 100);

        if (percentageToBeActivated <= _result)
        {
            gameObject.SetActive(false);
            return false;
        }

        return true;
    }

    private void RotateObjRandom()
    {
        int _result = Random.Range(1,360);

        gameObject.transform.Rotate(0,_result,0);
    }

    private void ScaleObjRandom()
    {
        float _resultX = Random.Range(1f, 2f);
        float _resultY = Random.Range(1f, 3.5f);
        float _resultZ = Random.Range(0.5f, 1.5f);

        gameObject.transform.localScale = new Vector3(_resultX, _resultY, _resultZ);
    }
}
