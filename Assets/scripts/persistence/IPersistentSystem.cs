
namespace PixelRPG.Persistence
{
    public interface IPersistentSystem
    {
        public SaveData SaveData();

        public void LoadData(SaveData data);

        public void ResetData();
    }
}
