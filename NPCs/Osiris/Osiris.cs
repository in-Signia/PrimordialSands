using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs.Osiris
{
    public class Osiris : ModNPC
    {
        public int dashTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Osiris, Ethereal Scourge");
            Main.npcFrameCount[npc.type] = 1;
        }
        
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.damage = 62;
            npc.defense = 30;
            npc.lifeMax = 24000;
            npc.width = 120;
            npc.height = 120;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit34;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = Terraria.Item.buyPrice(0, 15, 0, 0);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OneThousandDeadlyStares");
            bossBag = mod.ItemType("Osiris Bag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.38f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.45f);
        }
        public int A = 0;
        public int B = 0;
        public override void AI() //All of this is broken.
        {
            /* 
            Phase 1: Summons Lost souls that orbit Osirs, they occasionally fire projectiles
            that go directly in the player's 'oldPosition', his movement is thrusting towards 
            the player, where the player was 1.5 seconds ago to be exact.
            Phase 2: (75% Heatlh) Phase two allows the Souls in orbit to fly free, these heads
            having basic player homing, and explode within a close radius of the player.
            Osiris switches to basic movement and follows the player.
            Phase 3: (35% Health) Osiris now quickly chases the player, if the player tries to
            fly diagonally above him, he will fly to the direction they're moving (i somewhat have
            code working like this). Projectiles start to move from the screen left to right slowly,
            and projectiles come from the floor slowly moving diagonally. basically final phase is 
            a bullet hell.
            */
            Player player = Main.player[npc.target]; //Despawn Method taken from The Twins
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                {
                    npc.TargetClosest(true);
                }
                bool dead2 = Main.player[npc.target].dead;
                float num368 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
                float num369 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
                if (Main.netMode != 1 && !Main.dayTime && !dead2 && npc.timeLeft < 10)
                {
                    int num;
                    for (int num373 = 0; num373 < 200; num373 = num + 1)
                    {
                        if (num373 != npc.whoAmI && Main.npc[num373].active && Main.npc[num373].timeLeft - 1 > npc.timeLeft)
                        {
                            npc.timeLeft = Main.npc[num373].timeLeft - 1;
                        }
                        num = num373;
                    }
                }
                if (Main.dayTime | dead2)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                        return;
                    }
                }
            }
            bool phase1 = ((double)npc.life < (double)npc.lifeMax * 1);
            bool phase2 = ((double)npc.life < (double)npc.lifeMax * 0.7);
            bool phase3 = ((double)npc.life < (double)npc.lifeMax * 0.35);

            float npcVel = 5f;
            float num2 = 10f;
            Vector2 vector3 = new Vector2(npc.Center.X, npc.Center.Y);
            float playerX = Main.player[npc.target].Center.X - vector3.X;
            float playerY = Main.player[npc.target].Center.Y - vector3.Y - 250f;
            float playerSqrt = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
            playerSqrt = npcVel / playerSqrt;
            playerX *= playerSqrt;
            playerY *= playerSqrt;
            if (npc.velocity.X < playerX) //NPCs velocity, npc attempts to reach player center
            {
                npc.velocity.X = npc.velocity.X + num2;
                if (npc.velocity.X < 0f && playerX > 0f && Main.netMode != 1)
                {
                    npc.velocity.X = npc.velocity.X + num2;
                    npc.netUpdate = true;
                }
            }

            else if (npc.velocity.X > playerX)
            {
                npc.velocity.X = npc.velocity.X - num2;
                if (npc.velocity.X > 0f && playerX < 0f && Main.netMode != 1)
                {
                    npc.velocity.X = npc.velocity.X - num2;
                    npc.netUpdate = true;
                }
            }

            if (npc.velocity.Y < playerY)
            {
                npc.velocity.Y = npc.velocity.Y + num2;
                if (npc.velocity.Y < 0f && playerY > 0f && Main.netMode != 1)
                {
                    npc.velocity.Y = npc.velocity.Y + num2;
                    npc.netUpdate = true;
                }
            }

            else if (npc.velocity.Y > playerY)
            {
                npc.velocity.Y = npc.velocity.Y - num2;
                if (npc.velocity.Y > 0f && playerY < 0f && Main.netMode != 1)
                {
                    npc.velocity.Y = npc.velocity.Y - num2;
                    npc.netUpdate = true;
                }
            }

            npc.ai[0] = 1; //start ai
            if (npc.ai[0] == 1) //Summoning Lost Souls, Orbiting Mode
            {
                if (phase1)
                {        
                    int num32 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num32].ai[1] = 0;
                    Main.npc[num32].ai[0] = npc.whoAmI;
                    int num33 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num33].ai[1] = 45;
                    Main.npc[num33].ai[0] = npc.whoAmI;
                    int num34 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num34].ai[1] = 90;
                    Main.npc[num34].ai[0] = npc.whoAmI;
                    int num35 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num35].ai[1] = 135;
                    Main.npc[num35].ai[0] = npc.whoAmI;
                    int num36 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num36].ai[1] = 180;
                    Main.npc[num36].ai[0] = npc.whoAmI;
                    int num37 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num37].ai[1] = 225;
                    Main.npc[num37].ai[0] = npc.whoAmI;
                    int num38 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num38].ai[1] = 270;
                    Main.npc[num38].ai[0] = npc.whoAmI;
                    int num39 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Soul"));
                    Main.npc[num39].ai[1] = 315;
                    Main.npc[num39].ai[0] = npc.whoAmI;
                    npc.ai[1] = 1;  //I saw in kitty's code how he made AI run manually instead of a tick runner, it's effiecent for Large AI
                    npc.localAI[0] += 1;
                    dashTimer += 1;
                }

                if (npc.ai[1] == 1)
                {
                   if (dashTimer >= 120)
                   {              
                        float dash = 25f; //Thrusting Movement
                        if (npc.direction == 1)
                        {
                            npc.velocity = new Vector2(npc.velocity.X + dash, npc.velocity.Y);
                            if (npc.velocity.X > 50f) //Don't want him to increase dash speed
                            {
                                npc.velocity.X = 25f;
                            }
                        }
                        if (npc.direction == -1)
                        {
                            npc.velocity = new Vector2(npc.velocity.X - dash, npc.velocity.Y);
                            if (npc.velocity.X > 50f)
                            {
                                npc.velocity.X = 25f;
                            }
                        }
                        dashTimer = 0;  
                    }
                    if (npc.localAI[0] >= 250 && phase1)
                    {
                        npc.netUpdate = true;
                        float projectileSpeed = 10.5f; //Moderate Firing Speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 36;
                        if (Main.expertMode)
                        {
                            damage = 48;
                        }
                        int type = mod.ProjectileType("SoulProjectile");
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * projectileSpeed) * -1), (float)((Math.Sin(rotation) * projectileSpeed) * -1), type, damage, 0f, Main.myPlayer);
                    }

                    else if (npc.localAI[0] >= 250 && !phase1)
                    {
                        npc.netUpdate = true;
                        float projectileSpeed = 15.5f; //Increased Firing Speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 42;
                        if (Main.expertMode)
                        {
                            damage = 56;
                        }
                        int type = mod.ProjectileType("SoulProjectile");
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * projectileSpeed) * -1), (float)((Math.Sin(rotation) * projectileSpeed) * -1), type, damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * projectileSpeed) * 1.5), (float)((Math.Sin(rotation) * projectileSpeed) * 1.5), type, damage, 0f, Main.myPlayer);
                    }
                    npc.ai[0] = 1;
                    npc.localAI[0] = 0;
                } 
            }
        }
    }
}