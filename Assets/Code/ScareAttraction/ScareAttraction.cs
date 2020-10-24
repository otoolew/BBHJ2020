using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareAttraction : MonoBehaviour
{
    public abstract Timer Timer { get; set; }
    public abstract SphereCollider ScareCollider { get; set; }
    public abstract bool IsActive { get; set; }
    public abstract void FireScare();

}
