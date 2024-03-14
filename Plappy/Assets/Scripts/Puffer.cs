using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerIdle
{
    none, move,jump
}

public class Puffer : MonoBehaviour
{
    [SerializeField] private float jumpPawer;
    
    public Rigidbody2D PlayerRD;

    public Vector2 StartPosition;

    public Animator Animator;

    public CapsuleCollider2D capsuleCollider2D;

    public playerIdle playerstateaIdle = playerIdle.none;
    
    public void JumpCollOn()
    {
        //플레이어 상태
        playerstateaIdle = playerIdle.jump;
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
    }
    public void MoveCollOn()
    {
        //플레이어 상태
        playerstateaIdle = playerIdle.move;
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
    }

    private void Awake() {
        //처음 위치 저장
        StartPosition = transform.position;
    }

    private void Update() 
    {
        var euler = PlayerRD.transform.rotation.eulerAngles; 
        euler = PlayerRD.velocity;
        
        PlayerRD.transform.rotation = Quaternion.Euler( 0,0,
        Mathf.Clamp(euler.y,-50f,2f)*20);
    }

    public void Jump()
    {
        //게임진행 상태일때
        if(ComponentManager.instance.gameManager.GameState == State.Game)
        {
            //jump 애니메이션 강제실행
            //Animator.Play("BogeaJump");
            Animator.SetTrigger("JUMP");
            //jump사운드 
            ComponentManager.instance.soundManager.SoundOneShot("playerjump");
            //심플 addforce로 점프 로직 (오직 위로만 가능)
            PlayerRD.velocity = Vector2.zero;
            PlayerRD.AddForce(Vector2.up * jumpPawer, ForceMode2D.Impulse );
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallBotton") ||
           collision.CompareTag("Pipe"))
        {
            PlayerRD.velocity = Vector2.zero;
            ComponentManager.instance.machine.ChangeState(State.GameEnd);
            return;
        }
    }

   
}
