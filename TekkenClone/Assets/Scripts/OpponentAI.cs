using System;
using System.Collections;
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

    public AudioClip[] hitSounds;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;


    private void Awake() {
        createRandomNumber();
        currentHealth = maxHealth;
    }

    private void Update() {
        if (attackCount == randomNumber){
            attackCount = 0;
        }
        for(int i = 0; i < fightingControllers.Length; i++){
            if(players[i].gameObject.activeSelf && Vector3.Distance(transform.position, players[i].position) <= attackRadius){
                animator.SetBool("Walking", false);

                if(Time.time - lastAttackTime > attackCooldown){
                    int randomAttackIndex = UnityEngine.Random.Range(0, attackAnimations.Length);

                    if(!isTakingDamage){
                        PerformAttack(randomAttackIndex);
                    }

                    fightingControllers[i].StartCoroutine(fightingControllers[i].PlayHitDamageAnimation(attackDamages));

                }
            } else {
            if(players[i].gameObject.activeSelf){
                    Vector3 direction = (players[i].position - transform.position).normalized;
                    characterController.Move(direction * movementSpeed * Time.deltaTime);

                    Quaternion targetRotattion = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotattion, rotationSpeed * Time.deltaTime);

                    animator.SetBool("Walking", true);
                }
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

     public IEnumerator PlayHitDamageAnimation(int takeDamage){
        yield return new WaitForSeconds(0.2f);
        
        if(hitSounds != null && hitSounds.Length > 0){
            int randomIndex = UnityEngine.Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[randomIndex], transform.position);
        }

        currentHealth -= takeDamage;

        if(currentHealth <= 0){
            Die();
        }
        animator.Play("HitDamageAnimation");
    }

    void Die(){
        Debug.Log("Oppenet died.");
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
