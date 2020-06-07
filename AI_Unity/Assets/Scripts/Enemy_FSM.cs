using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_FSM : MonoBehaviour
{
    //enums are nice to keep states 
    public enum STATE { EthanStanding, Start_Move, Move_Holland, Move_Friesland, Move_Zeeland, Move_Gelderland, LevelUp };

    //We need a property to access the current state

    public STATE CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;

            //Stop all coroutines
            StopAllCoroutines();

            switch (currentState)
            {
                case STATE.EthanStanding:
                    StartCoroutine(Standing());
                    break;
                case STATE.Start_Move:
                    StartCoroutine(Move());
                    break;

                case STATE.Move_Holland:
                    StartCoroutine(Holland());
                    break;

                case STATE.Move_Friesland:
                    StartCoroutine(Friesland());
                    break;

                case STATE.Move_Zeeland:
                    StartCoroutine(Zeeland());
                    break;

                case STATE.Move_Gelderland:
                    StartCoroutine(Gelderland());
                    break;
                case STATE.LevelUp:
                    StartCoroutine(Level_Up());
                    break;



            }
        }
    }

    [SerializeField]
    private STATE currentState;

    //What about some references?
    private CheckMyVision checkMyVision = null; //This is our previous file

    private NavMeshAgent agent = null;

    private Health playerHealth = null; //TODO

    private Transform playerTransform = null;

    //Reference to patrol destination
    private Transform patrolDestination = null;



    public float maxDamage = 10f;
    private void Awake()
    {
        checkMyVision = GetComponent<CheckMyVision>();
        agent = GetComponent<NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").
            GetComponent<Health>();
        //Do something about player transform too
        playerTransform = playerHealth.GetComponent<Transform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Find a random destination
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Dest");
        patrolDestination = destinations[Random.Range(0, destinations.Length)]
            .GetComponent<Transform>();

        CurrentState = STATE.EthanStanding;
    }

    public IEnumerator Standing()
    {
        while (currentState == STATE.EthanStanding)
        {
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.HIGH;

            agent.isStopped = false;
            agent.SetDestination(patrolDestination.position);

            while (agent.pathPending)
                yield return null; //This is to ensure we wait for path completion

            if (checkMyVision.targetInSight)
            {
                Debug.Log("Found you, changing to Move state");
                agent.isStopped = true;
                CurrentState = STATE.Start_Move;
                yield break;
            }
            yield return null;
        }

    }

    public IEnumerator Move()
    {
        while (currentState == STATE.Start_Move)
        {
            //In this case, let us keep sensitivity LOW
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.LOW;

            //The idea of the chase is to go to the last known position
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastKnownSighting);

            //Again we need to yield if path is yet incomplete
            while (agent.pathPending)
            {
                yield return null;
            }

            //While chasing we need to keep checking if we reached
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;

                //What if we reached destination but cannot see the player?

                if (!checkMyVision.targetInSight)
                {
                    CurrentState = STATE.EthanStanding;
                }
                yield break;
            }

            //Till next frame
            yield return null;
        }

    }





    public IEnumerator Holland()
    {
        while (currentState == STATE.Move_Holland)
        {
            //In this case, let us keep sensitivity LOW
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.LOW;

            //The idea of the chase is to go to the last known position
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastKnownSighting);

            //Again we need to yield if path is yet incomplete
            while (agent.pathPending)
            {
                yield return null;
            }

            //While chasing we need to keep checking if we reached
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;

                //What if we reached destination but cannot see the player?

                if (!checkMyVision.targetInSight)
                {
                    CurrentState = STATE.EthanStanding;
                }
                yield break;
            }

            //Till next frame
            yield return null;
        }

    }


    public IEnumerator Friesland()
    {
        while (currentState == STATE.Move_Holland)
        {
            //In this case, let us keep sensitivity LOW
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.LOW;

            //The idea of the chase is to go to the last known position
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastKnownSighting);

            //Again we need to yield if path is yet incomplete
            while (agent.pathPending)
            {
                yield return null;
            }

            //While chasing we need to keep checking if we reached
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;

                //What if we reached destination but cannot see the player?

                if (!checkMyVision.targetInSight)
                {
                    CurrentState = STATE.EthanStanding;
                }
                yield break;
            }

            //Till next frame
            yield return null;
        }

    }
    public IEnumerator Zeeland()
    {
        while (currentState == STATE.Move_Holland)
        {
            //In this case, let us keep sensitivity LOW
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.LOW;

            //The idea of the chase is to go to the last known position
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastKnownSighting);

            //Again we need to yield if path is yet incomplete
            while (agent.pathPending)
            {
                yield return null;
            }

            //While chasing we need to keep checking if we reached
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;

                //What if we reached destination but cannot see the player?

                if (!checkMyVision.targetInSight)
                {

                    CurrentState = STATE.EthanStanding;
                }
                yield break;
            }

            //Till next frame
            yield return null;
        }

    }
    public IEnumerator Gelderland()
    {
        while (currentState == STATE.Move_Holland)
        {
            //In this case, let us keep sensitivity LOW
            checkMyVision.sensitivity = CheckMyVision.enmSensitivity.LOW;

            //The idea of the chase is to go to the last known position
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastKnownSighting);

            //Again we need to yield if path is yet incomplete
            while (agent.pathPending)
            {
                yield return null;
            }

            //While chasing we need to keep checking if we reached
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;

                //What if we reached destination but cannot see the player?

                if (!checkMyVision.targetInSight)
                {

                    CurrentState = STATE.EthanStanding;
                }
                yield break;
            }

            //Till next frame
            yield return null;
        }

    }
    public IEnumerator Level_Up()
    {
        yield break;
    }
}

