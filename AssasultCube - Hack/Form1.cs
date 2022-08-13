using Memory;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;




namespace AssasultCube___Hack
{
    public partial class Form2 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

       

       

        Mem meme = new Mem();

        //Player
        public static string PlayerBase = "ac_client.exe+18AC00";
        public static string EntityList = "ac_client.exe+18AC04";

        //Ammunition
        public static string RifleAmmo = "0x140";
        public static string PistolAmmo = "0x12C";
        public static string Granades = "0x144";

        //Health
        public static string Health = "0xEC";
        public static string Armor = "0xF0";


        //Pos
        public static string walkX = "0x28";
        public static string walkY = "0x2C";
        public static string flyY  = "0x30";
        public static string ViewX = "0x34";
        public static string ViewY = "0x38";

        public string X { get; private set; }
        public string Y { get; private set; }
        public string Z { get; private set; }

        public Form2()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            int PID = meme.GetProcIdFromName("ac_client");
            if (PID > 0)
            {
                meme.OpenProcess(PID);
                Thread WA = new Thread(write) { IsBackground = true };
                WA.Start();
            }
            else
            {
                Form3 f3 = new Form3();
                //Form2 f2 = new Form2();
                Program.OpenDetailFormOnClose = true;
                this.Close();
                f3.Show();
                
            }
        }

        private void write()


             

        {
            while (true)
            {
                if (checkBox1.Checked)
                {
                    string RA = $"{PlayerBase},{RifleAmmo}";
                    string PA = $"{PlayerBase},{PistolAmmo}";

                    meme.WriteMemory(RA, "int", "2137");
                    meme.WriteMemory(PA, "int", "2137");
                    Thread.Sleep(2);
                }


                if (checkBox2.Checked)
                {
                    string HP = $"{PlayerBase},{Health}";
                    meme.WriteMemory(HP, "int", "99999");
                    Thread.Sleep(2);
                }

                if (checkBox3.Checked)
                {
                    if (GetAsyncKeyState(Keys.W) < 0)
                    {
                        float wX = meme.ReadFloat(walkX);


                        float AddX = wX + 1;


                        string wXs = AddX.ToString();

                        meme.WriteMemory(walkX, "float", wXs);

                        Thread.Sleep(2);
                    }
                    if (GetAsyncKeyState(Keys.A) < 0)
                    {

                        float wY = meme.ReadFloat(walkY);
                        float AddY = wY + 1;


                        string wYs = AddY.ToString();



                        meme.WriteMemory(walkY, "float", wYs);
                        Thread.Sleep(2);
                    }
                }

                if (checkBox4.Checked)
                {
                    if (GetAsyncKeyState(Keys.Space) < 0)
                    {
                        float fY = meme.ReadFloat(flyY);


                        float AddfY = fY + 1;


                        string fYs = AddfY.ToString();

                        meme.WriteMemory(flyY, "float", fYs);

                        Thread.Sleep(2);
                    }
                    if (GetAsyncKeyState(Keys.Z) < 0)
                    {
                        float fY = meme.ReadFloat(flyY);


                        float AddfY = fY - 1;


                        string fYs = AddfY.ToString();

                        meme.WriteMemory(flyY, "float", fYs);

                        Thread.Sleep(2);
                    }
                }
                if (checkBox5.Checked)
                {
                    string GR = $"{PlayerBase},{Granades}";

                    meme.WriteMemory(GR, "int", "2137");
                    Thread.Sleep(2);
                }
                if (checkBox6.Checked)
                {
                    string AR = $"{PlayerBase},{Armor}";

                    meme.WriteMemory(AR, "int", "2137");
                    Thread.Sleep(2);
                }
           
                if (checkBox7.Checked)
                {




                    if (GetAsyncKeyState(Keys.XButton2) < 0)
                    {
                        var LocalPlayer = GetLocal();
                        var Players = GetPlayers(LocalPlayer);

                        Players = Players.OrderBy(o => o.Magnitude).ToList();
                        if (Players.Count != 0)
                        {

                            Aim(LocalPlayer, Players[0]);
                        }
                    }
                    Thread.Sleep(2);
                    

                    Player GetLocal()
                    {
                        string hX = $"{PlayerBase},{walkX}";
                        string hY = $"{PlayerBase},{walkY}";
                        string yY = $"{PlayerBase},{flyY}";
                      

                        var Player = new Player
                        {
                            
                            X = meme.ReadFloat(hX),
                            Y = meme.ReadFloat(hY),
                            Z = meme.ReadFloat(yY)


                        };
                        return Player;
                    }

                    float GetMag(Player player, Player entity)
                    {

                        float mag;

                        mag = (float)Math.Sqrt(Math.Pow(entity.X - player.X, 2) + Math.Pow(entity.Y - player.Y, 2) + Math.Pow(entity.Z - player.Z, 2));


                        return mag;
                    }

                    void Aim(Player Player, Player Enemy)
                    {
                        float deltaX = Enemy.X - Player.X;
                        float deltaY = Enemy.Y - Player.Y;

                        float deltaZ = Enemy.Z - Player.Z;

                        float viewX = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;
                        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                        float viewY = (float)(Math.Atan2(deltaZ, distance) * 180 / Math.PI);

                        string vX = $"{PlayerBase},{ViewX}";
                        string vY = $"{PlayerBase},{ViewY}";

                        meme.WriteMemory(vX, "float", viewX.ToString());
                        meme.WriteMemory(vY, "float", viewY.ToString());

                        //System.Windows.Forms.MessageBox.Show("My message here");
                    }






                    List<Player> GetPlayers(Player local)
                    {
                        var players = new List<Player>();


                        for (int i = 0; i < 32; i++)
                        {
                            var CurrentStr = EntityList + ",0x" + (i * 0x28).ToString("X");


                            string pX = $"{CurrentStr},{walkX}";
                            string pY = $"{CurrentStr},{walkY}";
                            string pZ = $"{CurrentStr},{flyY}";
                            string hp = $"{CurrentStr},{Health}";

                            var Player = new Player
                            {

                                X = meme.ReadFloat(pX),
                                Y = meme.ReadFloat(pY),
                                Z = meme.ReadFloat(pZ),
                                Health = meme.ReadInt(hp)
                            };
                            Player.Magnitude = GetMag(local, Player);
                            if (Player.Health > 0 && Player.Health < 102)
                            {
                                players.Add(Player);
                            }
                        }





                        return players;


                    }

                    Thread.Sleep(2);
                }
                Thread.Sleep(2);
            }
        }

       
    }
}