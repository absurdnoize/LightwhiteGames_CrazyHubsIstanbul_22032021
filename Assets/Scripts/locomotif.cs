using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locomotif : MonoBehaviour
{
    [SerializeField] private bool bot = true;
    [SerializeField] private Player player;
    public bot bott;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("barrel"))
        {
            this.transform.localScale = new Vector3(transform.localScale.x / 1.1f, transform.localScale.y / 1.1f, transform.localScale.z / 1.1f);
            if (bot)
            {
                bott.t1Devidedto += 10;
            }
            else
            {
                player.t1Devidedto += 10;
                if (player.sizeOfLoc > 50)
                {
                    player.Loc();
                }
            }
        }
    }
}