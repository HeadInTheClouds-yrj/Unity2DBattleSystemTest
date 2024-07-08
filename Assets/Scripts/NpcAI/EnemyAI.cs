using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
<<<<<<< HEAD
using UnityEngine.VFX;
=======
>>>>>>> f83997c3f2b5dcefe8e0b252c5d175509bfb9d2f

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 1.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;
    private Rigidbody2D rb2;
    [SerializeField]
<<<<<<< HEAD
    private float speed = 15f;
    [SerializeField]
    private Transform slash;
    [SerializeField]
    private Transform weapon;
=======
    private float speed = 2.0f;
    [SerializeField]
    private Transform weapon;   
>>>>>>> f83997c3f2b5dcefe8e0b252c5d175509bfb9d2f
    bool following = false;

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
        rb2= GetComponent<Rigidbody2D>();
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        //Enemy AI movement based on Target availability
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            //OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        //Moving the Agent
        //OnMovementInput?.Invoke(movementInput);
        rb2.velocity = movementInput * speed * Time.deltaTime;
    }

    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackDistance)
            {
                //Attack logic
                movementInput = Vector2.zero;
                //OnAttackPressed?.Invoke();
                float anglez = Vector2.Angle(aiData.currentTarget.position, -Vector2.up);
                VisualEffect ve = slash.GetComponent<VisualEffect>();
                if (aiData.currentTarget.position.x < transform.position.x)
                {
                    ve.SetVector3("InitializeAngle", new Vector3(0, 0, -anglez));
                    weapon.eulerAngles = new Vector3(0, 0, -anglez);
                }
                else
                {
                    ve.SetVector3("InitializeAngle", new Vector3(0, 0, anglez));
                    weapon.eulerAngles = new Vector3(0, 0, anglez);
                }
                ve.Play();
                weapon.up = movementInput;
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                weapon.up = movementInput;
                
                
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }
        }

    }
}
