using UnityEngine;
using UnityEngine.Tilemaps;


// Class to contain all event functions
public class AllEventsScript : MonoBehaviour
{
    public delegate void BlankFunc();

    //On ship destroyed
    public static BlankFunc OnShipDestroyed;

}
