using UnityEngine;

public class MeleeSpecialState : MeleeBaseState{

    public MeleeSpecialState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int SkillHash = Animator.StringToHash("Skill 1");
    private const float TransitionDuration = 0.1f;
    private float duration = 1f;

    public override void Enter() {

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(SkillHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        //After hit duration pass back to previous state
        duration -= deltatime;
        
       if (duration <= 0f) {

            AreaManager.Instance.Special();

            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new MeleeSpotState(stateMachine));
                return;
            }
        }
    }

    public override void Exit() {}
}