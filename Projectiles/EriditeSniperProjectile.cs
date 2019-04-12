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
    public class EriditeSniperProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eridite Sniper");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.ranged = true;
            projectile.extraUpdates = 100;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            int num3;
            for (int num450 = 0; num450 < 3; num450 = num3 + 1)
            {
                Vector2 vector35 = projectile.position;
                vector35 -= projectile.velocity * ((float)num450 * 0.25f);
                int num451 = Dust.NewDust(vector35, 1, 1, 134, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num451].position = vector35;
                Main.dust[num451].noGravity = true;
                Main.dust[num451].scale = (float)Main.rand.Next(80, 81) * 0.014f;
                Dust dust3 = Main.dust[num451];
                dust3.velocity *= 2f;
                num3 = num450;
            }
            return;
        }

        public override void Kill(int timeLeft)
        {
            int num3;
            int num570 = 12;
            for (int num571 = 0; num571 < num570; num571 = num3 + 1)
            {
                int num572 = Dust.NewDust(projectile.Center, 0, 0, 134, 0f, 0f, 100, default(Color), 1f);
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