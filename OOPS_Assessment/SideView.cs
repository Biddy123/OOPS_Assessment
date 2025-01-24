using SFML.Graphics;
using SFML.System;

namespace OOPS_Assessment
{
    internal class SideView : RectangleShape
    {

        private Font basic_font;
        private Text title_text;

         public SideView() 
        {
            this.Size = new Vector2f(200, 600);
            this.Position = new Vector2f(600, 0);
            this.FillColor = new Color(128, 128, 128);

            basic_font = new Font("resources/basic_font.ttf");
            title_text = new Text("Welcome to my Game", basic_font);
            title_text.Position = this.Position + new Vector2f(20f, 20f);
        }

        public void DrawSideView(RenderWindow window)
        {
            window.Draw(this);

            window.Draw(title_text);
        }
    }
}
