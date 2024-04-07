using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
       public float Health {get; set;}
       public float Damage {get; set;}
       public abstract bool TakeDamage();

}
