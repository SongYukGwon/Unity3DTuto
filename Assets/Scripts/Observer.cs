using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_IsPlayerInRange;
    public Ending gameEnding;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            //방향을 한단위 위로 봐야함.
            Vector3 direction = player.position - transform.position + Vector3.up;

            Ray ray = new Ray(transform.position, direction);

            RaycastHit raycastHit;

            //raycastHit에 반환함.
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {

                }
            }
            gameEnding.CaughtPlayer();
        }


    }
}