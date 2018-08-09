using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float explosionRadius = 1f;
    float fixedExplosionRadius;
    public int explosionDamage = 10;
    public LayerMask boxMask;
    public GameObject explosion;
    public bool fineExplosion = true;
    BoxCollider2D boxCollider;

    Vector2 explosionCenter;

    public void OnDestroy()
    {
        Explode();
    }
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine("Coroutine");
        Destroy(gameObject, 3f);
    }


    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }

    public void Explode()
    {
        explosionCenter = transform.position;
        fixedExplosionRadius = explosionRadius;
        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity);
        HitDirection(Vector2.left);
        HitDirection(Vector2.right);
        HitDirection(Vector2.up);
        HitDirection(Vector2.down);
    }

    private void HitDirection(Vector2 direction)
    {
        fixedExplosionRadius = explosionRadius;
        RaycastHit2D[] hitDirection = HitTheBox(direction);
        DestroyFoundObjects(hitDirection);
        if (hitDirection.Length == 0 || fineExplosion)
            CreateExplosionPart(direction);
    }

    private void DestroyFoundObjects(RaycastHit2D[] hitDirection)
    {
        fineExplosion = true;
        for (int i = 0; i < hitDirection.Length; i++)
        {
            if (hitDirection[i].collider.gameObject.tag == "Wall")
            {
                fineExplosion = false;
            }
            if (hitDirection[i].collider.gameObject.tag == "OuterWall")
            {
                GameObject wall = hitDirection[i].collider.gameObject;

                if ( Mathf.Abs(transform.position.x - wall.transform.position.x) < Mathf.Abs(transform.position.y - wall.transform.position.y) )
                    //x1 == x2
                    fixedExplosionRadius = Mathf.Abs(wall.transform.position.y - transform.position.y) -0.8f;
                else
                    fixedExplosionRadius = Mathf.Abs(wall.transform.position.x - transform.position.x) -0.8f;
            }

        }
        if (fineExplosion)
        {
            for (int i = 0; i < hitDirection.Length; i++)
            {
                if (hitDirection[i].collider.gameObject.tag == "Player")
                {
                    hitDirection[i].collider.GetComponent<PlayerMovement>().TakeDamage(explosionDamage);
                }
                if (hitDirection[i].collider.gameObject.tag == "Bomb")
                {
                    Destroy(hitDirection[i].collider.gameObject);

                }

                Reward rewardBox = hitDirection[i].collider.GetComponent<Reward>();

                if (!rewardBox)
                    continue;

                rewardBox.gameObject.GetComponent<Reward>().OnMakingItDisable();
                rewardBox.gameObject.SetActive(false);
            }
        }
    }

    private void CreateExplosionPart(Vector2 normalizedVector)
    {
            for (int i = 1; i <= fixedExplosionRadius; i++)
            {
            Vector2 partPosition = explosionCenter + normalizedVector * i;
            GameObject instance = Instantiate(explosion, partPosition, Quaternion.identity);
            }
    }

    private RaycastHit2D[] HitTheBox(Vector2 direction)
    {
        
        return Physics2D.BoxCastAll(transform.position, new Vector2(0.6f, 0.6f), 0f, direction, fixedExplosionRadius); 
    }
}
