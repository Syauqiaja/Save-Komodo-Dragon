using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ketapel : ProjectileBase
{
    public override void Launch(Vector2 direction)
    {
        LeanTween.move(gameObject, direction, 0.4f).setEaseInSine().setOnComplete(()=>{
            gameObject.SetActive(false);
        });
    }
    private void OnDisable() {
        ParticleBase particleBase = ObjectPooler.Instance.GetParticle(ParticleType.StoneExplode);
        particleBase.transform.position = transform.position;
        particleBase.gameObject.SetActive(true);
        particleBase.particle.Play();
    }
}
