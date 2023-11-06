
namespace PixelRPG.Damage
{
    [System.Flags]
    public enum DamageType
    {
        Normal = 0x01,
        Fire = 0x02,

        Player = 0x04,
        Enemy = 0x08,
    }
}
