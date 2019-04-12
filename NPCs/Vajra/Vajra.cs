using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs.Vajra
{
    public class Vajra : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vajra, Goddess of the Flood");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.damage = 15;
            npc.defense = 10;
            npc.lifeMax = 1400;
            npc.width = 92;
            npc.height = 150;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.buffImmune[mod.BuffType("Flood")] = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/WoodHit");
            npc.DeathSound = SoundID.NPCDeath10;
            npc.value = Terraria.Item.buyPrice(0, 1, 30, 0);
            music = MusicID.Boss1;
            bossBag = mod.ItemType("VajraBag");
        }

        /*
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        */

        public override void AI()
        {
            #region Basic Properties
            Player player = Main.player[npc.target];
            if (npc.Center.X < Main.player[npc.target].Center.X - 2f)
            {
                npc.direction = 1;
            }
            if (npc.Center.X > Main.player[npc.target].Center.X + 2f)
            {
                npc.direction = -1;
            }
            float dash = 10f;
            bool halfLife = ((double)npc.life < (double)npc.lifeMax * 0.5);
            npc.spriteDirection = npc.direction;
            #endregion
            #region Multiplayer
            if (Main.netMode != 1)
            {
                npc.TargetClosest(true);
                int distance = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)distance)
                {
                    npc.active = false;
                    npc.life = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            #endregion           
            #region Movement
            npc.TargetClosest(true);
            Vector2 Vector5 = new Vector2(npc.Center.X, npc.Center.Y);
            float playerCenterX = Main.player[npc.target].Center.X - Vector5.X;
            float playerCenterY = Main.player[npc.target].Center.Y - Vector5.Y;
            float playerCenterSqrt = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
            float speed = 6.5f;
            playerCenterSqrt = speed / playerCenterSqrt;
            playerCenterX *= playerCenterSqrt;
            playerCenterY *= playerCenterSqrt;
            npc.velocity.X = (npc.velocity.X * 30f + playerCenterX) / 31f;
            npc.velocity.Y = (npc.velocity.Y * 30f + playerCenterY) / 31f;
            #endregion
            #region Dust
            int num2 = 25;
            for (int i = 0; i < num2; i++)
            {
                Vector2 Vector3 = Vector2.Normalize(npc.velocity) * new Vector2((float)npc.width / 2.5f, (float)npc.height) * 0.75f * 0.5f;
                Vector3 = Vector3.RotatedBy((double)((float)(i - (num2 / 2 - 1)) * 6.28318548f / (float)num2), default(Vector2)) + npc.Center;
                Vector2 Vector4 = Vector3 - npc.Center;
                int num3 = Dust.NewDust(Vector3 + Vector4, 0, 0, 89, Vector4.X * 2f, Vector4.Y * 2f, 100, default(Color), 1f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].velocity = Vector2.Normalize(Vector4) * 3f;
            }
            #endregion
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int num5;
                for (int i = 0; i < 100; i = num5 + 1)
                {
                    int num4 = Dust.NewDust(npc.Center, npc.width, npc.height, mod.DustType("IndenwoodDust"), 0f, 0f, 0, default(Color), 1f);
                    Dust dust = Main.dust[num4];
                    dust.velocity *= 2f;
                    dust = Main.dust[num4];
                    dust.scale = 1.5f;
                    num5 = i;
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode && Main.rand.Next (3) == (0))
            {
                player.AddBuff(mod.BuffType("Flood"), 220, true);
            }
            else if (!Main.expertMode && Main.rand.Next(4) == (0))
            {
                player.AddBuff(mod.BuffType("Flood"), 180, true);
            }
        }
    }
}