using UnityEngine;

public abstract class RangeBaseState : State{

    protected RangeStateMachine stateMachine;
    protected RangeBaseState(RangeStateMachine stateMachine){
        this.stateMachine = stateMachine;
    }

    protected void MoveToPlayer(float deltaTime) {

        Vector3 direction = stateMachine.Player.transform.position - stateMachine.transform.position; 

        stateMachine.Rigidbody.MovePosition(stateMachine.transform.position + direction.normalized * stateMachine.MovementSpeed * deltaTime);
    }

    protected void Move(Transform targetPos, float deltaTime) {

        stateMachine.transform.position = Vector2.MoveTowards(stateMachine.transform.position, targetPos.position, stateMachine.PatrolSpeed * deltaTime);
    }

    protected bool IsInSpotRange() {

        if (stateMachine.Player.isDie) { return false; }
        float distanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceSqr <= stateMachine.SpotRange * stateMachine.SpotRange;
    }

    //checking if player distance to enemy is less than the enemy chasing range so return true 
    protected void CheckFacing() {

        if ((stateMachine.transform.position.x < stateMachine.Player.transform.position.x) && !stateMachine.IsFacingRight) {
            Turn();
        }
        else if ((stateMachine.transform.position.x > stateMachine.Player.transform.position.x) && stateMachine.IsFacingRight) {
            Turn();
        }
    }

    protected bool IsInShootRange() {

        if (stateMachine.Player.isDie) { return false; }
        float distanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceSqr <= stateMachine.ShootRange * stateMachine.ShootRange;
    }

    protected bool IsInAttackRange() {

        if (stateMachine.Player.isDie) { return false; }
        float distanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

    protected void ShootAttack(GameObject enemyBullet){
        
        Vector2 lookDir = stateMachine.Player.transform.position - stateMachine.ShotPoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        stateMachine.ShotPoint.rotation = rotate;

        Object.Instantiate(enemyBullet, stateMachine.ShotPoint.position, stateMachine.ShotPoint.rotation);
    }

    protected void CheckPlayerDashing(){

        if(stateMachine.playerStateMachine != null){
            if(stateMachine.playerStateMachine.IsDashing){

                stateMachine.Collider.enabled = false;
            }
            else{
                stateMachine.Collider.enabled = true;
            }
        }
    }

    protected void CheckAstralDashing(){

        if(stateMachine.astralStateMachine != null){
            if(stateMachine.astralStateMachine.IsDashing){

                stateMachine.Collider.enabled = false;
            }
            else{
                stateMachine.Collider.enabled = true;
            }
        }
    }

    protected void CheckColliding(){

        if((stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude < 0.5f){

            stateMachine.Collider.enabled = false;
        }
        else{
            stateMachine.Collider.enabled = true;
        }
    }

    protected void Turn() {

        stateMachine.IsFacingRight = !stateMachine.IsFacingRight;

        Vector3 scale = stateMachine.transform.localScale;
        scale.x *= -1;
        stateMachine.transform.localScale = scale;
    }

    protected void Destroy(GameObject gameObject, float timer){

        Destroy(gameObject, timer);
    }
}