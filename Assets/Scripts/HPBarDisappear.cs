using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarDisappear : MonoBehaviour
{
    public void DisappearHealthBar()
    {
        gameObject.SetActive(false);
    }
}
