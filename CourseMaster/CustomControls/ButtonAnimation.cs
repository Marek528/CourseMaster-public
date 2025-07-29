using Timer = System.Windows.Forms.Timer;

namespace CourseMaster.CustomControls
{
    public class ButtonAnimation : Button
    {
        private Timer animationTimer = new Timer();
        private bool isHovered = false;
        private int fontSize, fontSizeMax, fontSizeMin, underlineHeight = 0;
        private bool isUnderline, biggerTextOnHover;

        public ButtonAnimation(int fontSizeMax, bool isUnderline = false, bool biggerTextOnHover = true)
        {
            fontSize = fontSizeMax - 2;
            this.fontSizeMax = fontSizeMax;
            fontSizeMin = fontSize;
            this.isUnderline = isUnderline;
            this.biggerTextOnHover = biggerTextOnHover;
            DoubleBuffered = true;
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimationTimer_Tick;
            MouseEnter += ButtonAnimation_MouseEnter;
            MouseLeave += ButtonAnimation_MouseLeave;
            Paint += ButtonAnimation_Paint;
        }

        private void ButtonAnimation_MouseEnter(object? sender, EventArgs e)
        {
            isHovered = true;
            animationTimer.Start();
        }

        private void ButtonAnimation_MouseLeave(object? sender, EventArgs e)
        {
            isHovered = false;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            if (biggerTextOnHover)
            {
                if (isHovered)
                {
                    if (fontSize < fontSizeMax) fontSize++;
                    if (underlineHeight < 3) underlineHeight++;
                }
                else
                {
                    if (fontSize > fontSizeMin) fontSize--;
                    if (underlineHeight > 0) underlineHeight--;
                }
                Invalidate();
            }
            else
            {
                if (isHovered)
                {
                    if (underlineHeight < 3) underlineHeight++;
                }
                else
                {
                    if (underlineHeight > 0) underlineHeight--;
                }
                Invalidate();
            }
        }

        private void ButtonAnimation_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            using (Font font = new Font("Segoe UI Black", fontSize))
            {
                SizeF textSize = e.Graphics.MeasureString(Text, font);
                float textX = (Width - textSize.Width) / 2;
                float textY = (Height - textSize.Height) / 2;

                e.Graphics.DrawString(Text, font, new SolidBrush(Color.FromArgb(0, 82, 165)), new PointF(textX, textY));
            }

            if (isUnderline)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 82, 165)), 10, Height - underlineHeight, Width - 20, underlineHeight);
            }
        }
    }
}
