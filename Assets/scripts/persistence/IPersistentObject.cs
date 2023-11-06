
namespace PixelRPG.Persistence
{
    public interface IPersistentObject
    {
        public bool CurrentStatus { get; set; }

        public int SceneIndex { get; }
    }
}
