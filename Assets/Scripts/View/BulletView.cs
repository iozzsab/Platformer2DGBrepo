namespace PlatformerMVC
{
    public class BulletView : LevelObjectView
    {
        private int _damagePoint = 50;

        public int DamagePoint
        {
            get => _damagePoint;
            set => _damagePoint = value;
        }
    }
}