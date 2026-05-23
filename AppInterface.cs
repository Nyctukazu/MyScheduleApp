using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyScheduleApp;

class AppInterface
{
    private static string _time;
    private static string _day;
    private int _interfaceHeight;
    private int _interfaceWidth;

    public void appInterface(string time, string day){
        _time = time;
        _day = day;
    }
    public void Update(){
        _interfaceWidth = Globals.WindowWidth;
        _interfaceHeight = Globals.WindowHeight;
    }
    public void Draw(SpriteBatch sb, SpriteFont font, Texture2D pixel){
        sb.Draw(pixel, new Rectangle(0, 0, _interfaceWidth, 60), null, Color.Black * 0.6f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        sb.DrawString(font, "DIT22 Class Schedule", new Vector2(20, 10), Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
        sb.DrawString(font, _time, new Vector2(_interfaceWidth - 250, 10), Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
        sb.DrawString(font, _day, new Vector2(_interfaceWidth - 150, 10), Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
    }
}