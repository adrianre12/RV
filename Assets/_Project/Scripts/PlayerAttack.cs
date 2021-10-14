using RVHonorAI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Camera _camera;
    private PlayerCharacter _myCharacter = null;
    public float maxDistance;
    public float maxAngle;
    public float damage;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        if (go == null)
        {
            Debug.Log("No MainCamera tag found");
            return;
        }
        _camera = go.GetComponent<Camera>();
        if (!TryGetComponent<PlayerCharacter>(out _myCharacter))
        {
            Debug.Log("No ICharacter component found");
            return;
        }
    }

     /// <summary>
     /// This is called from an event on the weapon attack animation. For ranged it could be called from a collider trigger on the projectile.
     /// </summary>
    public void DoAttack()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance); // should filter this on layers

        //for (int i = 0; i < colliders.Length; i++)
        foreach (Collider collider in colliders)
        {
            if (collider.transform == transform)
                continue;
  
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (Mathf.Abs(angle) < maxAngle) // limit cone of attack
            {
                SendDamage(collider.gameObject, damage);
            }
            
        }
    }

    public void SendDamage(GameObject reciver, float damage)
    {
        ICharacter target = null; ;
        if (!reciver.TryGetComponent<ICharacter>(out target)) // check collider is an ICharacter
            return;
        if (target.IsAlly(_myCharacter) )//|| !target.IsEnemy(_myCharacter)) // is it an enemy?
            return;

        float damageDone = target.ReceiveDamage(damage, _myCharacter as PlayerCharacter, DamageType.Physical);
        Debug.LogFormat("Damage done {0}", damageDone);
    }
}
