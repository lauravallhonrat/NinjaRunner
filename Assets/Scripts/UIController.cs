using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text m_coinsValue;

    public void CoinsUpdate(int pcountCoins)
    {
        m_coinsValue.text = pcountCoins.ToString();
    }
}


