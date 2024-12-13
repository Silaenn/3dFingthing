using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FightingController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;
    private Animator animator;
    [Header("Player Fight")]
    public float attackCooldown = 0.5f;
    public int attackDamages = 5;
    public string[] attackAnimations = {"Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation"};
    public float dodgeDistance = 2f;
    public float attackRadius = 2.2f;
    public Transform[] opponents;
    private float lastAttackTime;

    [Header("Effects and Sound")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        PerformMovement();
        PerformDodgeFront();

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            PerformAttack(0);
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){
            PerformAttack(1);
        } else if(Input.GetKeyDown(KeyCode.Alpha3)){
            PerformAttack(2);
        } else if(Input.GetKeyDown(KeyCode.Alpha4)){
            PerformAttack(3);
        } 
    }

    void PerformMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        if(movement != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if(horizontalInput > 0){
                animator.SetBool("Walking", true);
            } else if(horizontalInput < 0){
                animator.SetBool("Walking", true);
            } else if (verticalInput != 0){
                animator.SetBool("Walking", true);
            }
        } else {
            animator.SetBool("Walking", false);
        }

        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }


    void PerformAttack(int attackIndex){
        if(Time.time - lastAttackTime > attackCooldown){
            animator.Play(attackAnimations[attackIndex]);

            int damage = attackDamages;
            Debug.Log("Performed attack: " + attackIndex + 1 + " dealing " + damage + "damage");

            lastAttackTime = Time.time;

            foreach(Transform opponent in opponents){
                if(Vector3.Distance(transform.position, opponent.position) <= attackRadius){
                    opponent.GetComponent<OpponentAI>().StartCoroutine(opponent.GetComponent<OpponentAI>().PlayHitDamageAnimation(attackDamages));
                }
            }

        } else{
            Debug.Log("Cannot perform attack yet. Cooldown time remining");
        }
    }

    void PerformDodgeFront(){
        if(Input.GetKeyDown(KeyCode.E)){
            animator.Play("DodgeFrontAnimation");

            Vector3 dodgeDIrection = transform.forward * dodgeDistance;
            characterController.Move(dodgeDIrection);
        }
    }

    public IEnumerator PlayHitDamageAnimation(int takeDamage){
        yield return new WaitForSeconds(0.2f);

        animator.Play("HitDamageAnimation");
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
