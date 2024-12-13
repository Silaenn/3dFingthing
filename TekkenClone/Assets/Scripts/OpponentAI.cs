using System;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [Header("Opponent Movement")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    public CharacterController characterController;
    public Animator animator;

    [Header("Opponent Fight")]
    public float attackCooldown = 0.5f;
    public int attackDamages = 5;
    public string[] attackAnimations = {"Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation"};
    public float dodgeDistance = 2f;
    public int attackCount = 0;
    public int randomNumber;
    public float attackRadius = 2f;
    public FightingController[] fightingControllers;
    public Transform[] players;
    public bool isTakingDamage;
    private float lastAttackTime;

    [Header("Effects and Sound")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;


    private void Awake() {
        createRandomNumber();
    }

    private void Update() {
        for(int i = 0; i < fightingControllers.Length; i++){
            if(players[i].gameObject.activeSelf){
                Vector3 direction = (players[i].position - transform.position).normalized;
                characterController.Move(direction * movementSpeed * Time.deltaTime);

                Quaternion targetRotattion = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotattion, rotationSpeed * Time.deltaTime);

                animator.SetBool("Walking", true);
            }
        }
    }

     void PerformAttack(int attackIndex){
         animator.Play(attackAnimations[attackIndex]);

         int damage = attackDamages;
         Debug.Log("Performed attack: " + attackIndex + 1 + " dealing " + damage + "damage");

         lastAttackTime = Time.time;

    }
    void PerformDodgeFront(){
        animator.Play("DodgeFrontAnimation");

        Vector3 dodgeDIrection = -transform.forward * dodgeDistance;

        characterController.SimpleMove(dodgeDIrection);
    }
    void createRandomNumber(){
        randomNumber = UnityEngine.Random.Range(1, 5);
    }
    public void Attack1Effect(){
        attack1Effect.Play();
    }
    public void Attack2Effect(){
        attack2Effect.Play();
    }
    public void Attack3Effect(){
        attack3Effect.Play();
    }
    public void Attack4Effect(){
        attack4Effect.Play();
    }
}
