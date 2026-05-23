using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace MyScheduleApp;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    string fileName = "Schedule.json";
    private Dictionary<string, List<CourseSession>> _schedule;
    private SpriteFont _instructionFont;
    private AppInterface _myInterface;
    private calendarTable _myCalendar;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnResize;

        _graphics.IsFullScreen = false; 
        _graphics.PreferredBackBufferWidth = Globals.WindowWidth;
        _graphics.PreferredBackBufferHeight = Globals.WindowHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        Window.Title = "My College Schedule - 2nd Semester";
   
        loadFileJson();
        
        _myInterface = new AppInterface();
        _myCalendar = new calendarTable(_schedule);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[]{Color.White});

        _instructionFont = Content.Load<SpriteFont>("File");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        DateTime now = DateTime.Now;
        string currentDay = now.DayOfWeek.ToString();
        TimeSpan currentTime = now.TimeOfDay;

        _myInterface.appInterface(currentTime.ToString(@"hh\:mm"), currentDay);
        _myCalendar.clock((int)currentTime.TotalMinutes, currentDay);
        _myInterface.Update();
        _myCalendar.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _myCalendar.Draw(_spriteBatch, _instructionFont, _pixel);
        _myInterface.Draw(_spriteBatch, _instructionFont, _pixel);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
    void OnResize(object sender, EventArgs e)
    {
        Globals.WindowWidth = Window.ClientBounds.Width;
        Globals.WindowHeight = Window.ClientBounds.Height;
    }
    public void loadFileJson()
    {
        string json = "";
        try
        {
            json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
        }
        catch(FileNotFoundException)
        {
            json = "{ 'Monday': [], 'Tuesday': [], 'Wednesday': [], 'Thursday': [], 'Friday': [], 'Saturday': [], 'Sunday': [] }";
        }
        catch(Exception ex)
        {
            Console.WriteLine("Critical Error: " + ex.Message);
        }
    _schedule = JsonConvert.DeserializeObject<Dictionary<string, List<CourseSession>>>(json);
    }
    public void saveFileJson()
    {
        
    }
}
