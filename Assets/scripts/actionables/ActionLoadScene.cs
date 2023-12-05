using PixelRPG.Framework;
using PixelRPG.Input;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class ActionLoadScene : MonoBehaviour, IActionable
    {
        public async void Activate()
        {
            // This is now specifically for loading the menu after beating the game
            Core.DataSaver.SaveGame();
            Core.InputHandler.AddInputBlock(new InputBlock(new InputType[] { InputType.Pause }));
            await Task.Delay(4000);
            Core.LevelChanger.ChangeLevel(_level, false);
        }

        [SerializeField] string _level;
    }
}
