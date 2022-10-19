using Models.Player.Weapon.Bullets;

namespace Models.Player.Interfaces
{
    public interface IDamageVisitor
    {
        float Health();
        void Visit(BulletModel bullet);
        void Visit(PlayerModel playerModel);
        void Visit(Sun sun);
    }
}