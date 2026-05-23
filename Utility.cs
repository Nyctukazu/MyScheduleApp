using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public static class Utility{
    public static Color HexToColor(string hex)
    {

        hex = hex.Replace("#", "");

        int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color(r, g, b);
    }   
    public static string WrapText(SpriteFont font, string text, float maxLineWidth, float maxHeight)
    {
        string[] words = text.Split(' ');
        string result = "";
        string lineAttempt = "";
        float currentTotalHeight = 0;
        float spacing = font.LineSpacing; 
        int lineNumber = 0;
        string firstLetter = text[0].ToString();

        foreach (string word in words)
        {
            Vector2 size = font.MeasureString(lineAttempt + word);

            if (size.X < maxLineWidth)
            {
                lineAttempt += word + " ";
            }
            else
            {
                if (currentTotalHeight + (spacing * 2) > maxHeight) 
                {
                    return result + "..."; 
                }
                result += lineAttempt + "\n";
                lineAttempt = word + " ";
                currentTotalHeight += spacing;
            }
        }

        return result + lineAttempt; 
    }
}
