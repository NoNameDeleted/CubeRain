using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    

    private IEnumerator ExploidWithDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
