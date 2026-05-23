using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MyScheduleApp;

class calendarTable{

    private Dictionary<string, List<CourseSession>> _schedule;
    private List<string> _days = new List<string> {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
    private int _columnWidth;
    private int _rowHeight;
    private int _startX = 100;
    private int _startY = 90;
    private int _startHour = 7;
    private int _endHour = 22;
    private int _clockMin; 
    private string _currentDay;
    private int _weekIndex;
    private int _padding = 5;

    public calendarTable(Dictionary<string, List<CourseSession>> _schedule)
    {
        this._schedule = _schedule;

    }
    public void clock(int min, string day)
    {
        _clockMin = min;
        _currentDay = day;
        _weekIndex = _days.IndexOf(_currentDay);
    }
    public void Update(){
        _columnWidth = (Globals.WindowWidth - _startX) / 7;
        _rowHeight = (Globals.WindowHeight - _startY) / 15;
        int sidebarWidth = (int)(Globals.WindowWidth * 0.15f);
        int tableWidth = Globals.WindowWidth - sidebarWidth;
    }

    public void Draw(SpriteBatch sb, SpriteFont font, Texture2D pixel)
    {   
        sb.Draw(pixel, new Rectangle((_startX - 10)+ _weekIndex * _columnWidth, 0, _columnWidth + 10, Globals.WindowHeight), null, Color.Yellow*0.4f);

        for (int h = _startHour; h <= _endHour; h++)
        {
            float yPos = _startY + ((h - _startHour) * _rowHeight);
            string timeLabel = $"{h:D2}:00"; 
            sb.DrawString(font, timeLabel, new Vector2(_startX - 70, yPos - 10), Color.LightGray, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            sb.Draw(pixel, new Rectangle(_startX - 10, (int)yPos, _days.Count * _columnWidth, 1), Color.White * 0.2f);
        }
        for (int i = 0; i < _days.Count; i++)
        {
            sb.DrawString(font, _days[i], new Vector2(_startX + (i * _columnWidth), _startY - 30), Color.White);
            if(this._schedule.ContainsKey(_days[i]))
            {
                foreach(var session in this._schedule[_days[i]])
                {
                    if(session == null || string.IsNullOrEmpty(session.startTime)) continue;

                    float yStart = GetYPosFromTime(session.startTime, _rowHeight);
                    float yEnd = GetYPosFromTime(session.endTime, _rowHeight);
                    float durationHeight = yEnd - yStart;
                    float yOffset = GetYPosFromTime(session.startTime, _rowHeight);

                    Rectangle box = new Rectangle(_startX + (i * _columnWidth), (int)(_startY + yOffset), _columnWidth - 5, (int)durationHeight);

                    sb.Draw(pixel, box, Utility.HexToColor("#1A237E"));

                    string displayAbbrev = Utility.WrapText(font, session.Abbrev, _columnWidth, durationHeight);
                    Vector2 stringSize = font.MeasureString(displayAbbrev);
                    string displaySubject = Utility.WrapText(font, session.Subject, _columnWidth, durationHeight - stringSize.Y + _padding * 2);

                    sb.DrawString(font, displaySubject, new Vector2(box.X + _padding, box.Y + stringSize.Y + _padding * 2), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
                    sb.DrawString(font, displayAbbrev, new Vector2(box.X + _padding, box.Y + _padding), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);    
                }
            }
        }
        float hourRate = _clockMin / 60.0f;
        if(hourRate >= _startHour && hourRate <= _endHour){
            int clockHand = _startY + (int)((hourRate - _startHour) * _rowHeight);

            sb.Draw(pixel, new Rectangle(_startX - 10, clockHand, _days.Count * _columnWidth, 2), Color.Red);
        }
    }

    private float GetYPosFromTime(string timeStr, int _rowHeight)
    {
        if (string.IsNullOrEmpty(timeStr)) return 0;
        if (TimeSpan.TryParse(timeStr, out TimeSpan time))
        {
            return (float)(time.TotalHours - _startHour) * _rowHeight;
        }
        return 0;
    }
}

