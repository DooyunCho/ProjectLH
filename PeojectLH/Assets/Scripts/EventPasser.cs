using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPasser : MonoBehaviour {
    public void attack()
    {
        GetComponentInParent<Player>().attack();
    }
}
