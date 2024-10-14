public class RangedEnemy : Enemy
{
    RangedEnemy()
    {
        _health = 100f;
        _range = 55f;
        _cooldown = 3;
    }

    public override void Attack()
    {
        // attack logic
        return;
    }
}