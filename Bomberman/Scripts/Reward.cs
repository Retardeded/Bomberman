using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {

    public GameObject speedBoost;
    public GameObject damageBoost;
    public GameObject bombBoost;
    public GameObject pushBoost;
    public int chanceforSpeed = 10;
    public int chanceforDamage = 10;
    public int chanceforBomb = 10;
    void Start () {
		
	}

    public void OnMakingItDisable()
    {

        int randomIndex = Random.Range(0, 100);
        if (randomIndex < chanceforSpeed)
        {
            GameObject boost = Instantiate(speedBoost, transform.position, Quaternion.identity);
            boost.GetComponent<Boost>().SetBoostType(0);
        }
        else if (randomIndex < chanceforSpeed + chanceforDamage)
        {
            GameObject boost = Instantiate(damageBoost, transform.position, Quaternion.identity);
            boost.GetComponent<Boost>().SetBoostType(1);
        }
        else if (randomIndex < chanceforSpeed + chanceforDamage + chanceforBomb)
        {
            GameObject boost = Instantiate(bombBoost, transform.position, Quaternion.identity);
            boost.GetComponent<Boost>().SetBoostType(2);
        }
    }
}
