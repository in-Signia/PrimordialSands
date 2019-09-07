using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class FloodBlastProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flood Blast");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == (0))
            {
                target.AddBuff(mod.BuffType("Flood"), 380);
            }
        }

        public override void AI()
        {
            float num992 = 120f;
            float num993 = 60f;
            if (projectile.ai[1] < num992)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 60f)
                {
                    float num994 = (projectile.ai[1] - num993) / (num992 - num993);
                    projectile.alpha = (int)((1f - num994) * 255f);
                }
                float num995 = projectile.ai[1] / num993;
                Vector2 spinningpoint = new Vector2(0f, -30f);
                spinningpoint = spinningpoint.RotatedBy((double)(num995 * 1.5f * 6.28318548f), default(Vector2)) * new Vector2(1f, 0.4f);
                int num;
                for (int num996 = 0; num996 < 4; num996 = num + 1)
                {
                    Vector2 value3 = Vector2.Zero;
                    float scaleFactor2 = 1f;
                    if (num996 == 0)
                    {
                        value3 = Vector2.UnitY * -15f;
                        scaleFactor2 = 0.15f;
                    }
                    if (num996 == 1)
                    {
                        value3 = Vector2.UnitY * -5f;
                        scaleFactor2 = 0.3f;
                    }
                    if (num996 == 2)
                    {
                        value3 = Vector2.UnitY * 5f;
                        scaleFactor2 = 0.6f;
                    }
                    if (num996 == 3)
                    {
                        value3 = Vector2.UnitY * 20f;
                        scaleFactor2 = 0.45f;
                    }
                    int num997 = Dust.NewDust(projectile.Center, 0, 0, 89, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num997].noGravity = true;
                    Main.dust[num997].position = projectile.Center + spinningpoint * scaleFactor2 + value3;
                    Main.dust[num997].velocity = Vector2.Zero;
                    Main.dust[num997].scale = 1f;
                    spinningpoint *= -1f;
                    num997 = Dust.NewDust(projectile.Center, 0, 0, mod.DustType("IndenwoodDust"), 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num997].noGravity = true;
                    Main.dust[num997].position = projectile.Center + spinningpoint * scaleFactor2 + value3;
                    Main.dust[num997].velocity = Vector2.Zero;
                    Main.dust[num997].scale = 0.75f;
                    num = num996;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item66, projectile.position);
            int num3;
            int num570 = 6;
            for (int num571 = 0; num571 < num570; num571 = num3 + 1)
            {
                int num572 = Dust.NewDust(projectile.Center, 0, 0, 89, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num572];
                dust.velocity *= 1.6f;
                Dust dust56 = Main.dust[num572];
                dust56.velocity.Y = dust56.velocity.Y - 1f;
                dust = Main.dust[num572];
                dust.velocity += -projectile.velocity * (Main.rand.NextFloat() * 2f - 1f) * 0.5f;
                Main.dust[num572].scale = 1.5f;
                Main.dust[num572].fadeIn = 0.5f;
                Main.dust[num572].noGravity = true;
                num3 = num571;
            }
        }
    }
}