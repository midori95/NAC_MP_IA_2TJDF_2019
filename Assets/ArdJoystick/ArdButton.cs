namespace ArdJoystick
{
    public enum ArdKeyCode
    {
        BUTTON_LEFT,
        BUTTON_DOWN,
        BUTTON_UP,
        BUTTON_RIGHT,

        BUTTON_START,
        BUTTON_SELECT,

        BUTTON_Y, //A
        BUTTON_B, //B
        BUTTON_X, //X
        BUTTON_A //Y
    }

    public class ArdButton
    {
        private ArdKeyCode keyCode;

        public bool keyDown = false;
        public bool keyPress = false;
        public bool keyUp = false;

        private int oldData = 0;

        public ArdButton(ArdKeyCode keyCode)
        {
            this.keyCode = keyCode;
        }

        public void ProcessData(int data)
        {
            if (data == 1 && oldData == 0)
            {
                keyDown = true;
            }
            else if (data == 0 && oldData == 1)
            {
                keyUp = true;
            }

            if (data == 1)
            {
                keyPress = true;
            }
            else
            {
                keyPress = false;
            }

            oldData = data;
        }
    }
}