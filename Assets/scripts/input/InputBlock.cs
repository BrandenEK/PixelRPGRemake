using System.Collections.Generic;

namespace PixelRPG.Input
{
    public class InputBlock
    {
        private readonly IEnumerable<InputType> _blockedInputs;

        public IEnumerable<InputType> BlockedInputs => _blockedInputs;

        public InputBlock(IEnumerable<InputType> blockedInputs)
        {
            _blockedInputs = blockedInputs ?? System.Array.Empty<InputType>();
        }
    }
}
