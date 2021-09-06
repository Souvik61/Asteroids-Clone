using UnityEngine;
using UnityEngine.Tilemaps;


// Class to contain all event functions
public class AllEventsScript : MonoBehaviour
{
    public delegate void BlankFunc();
    //function with a float as parameter
    public delegate void Func1(float a);
    //On ship destroyed
    public static Func1 OnShipDestroyed;

}
