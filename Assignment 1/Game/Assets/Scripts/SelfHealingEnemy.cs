using UnityEngine;
public class SelfHealingEnemy : Enemy
{
    private float _healingFactor;
    SelfHealingEnemy()
    {
        _health = 75f;
        _range = 25f;
        _cooldown = 1;
        _healingFactor = 1.005f;
    }
    public override void Attack()
    {
        // attack logic
        _health = Mathf.Clamp(_health + _healingFactor * Time.deltaTime, 0, 75);
        return;
    }
}
