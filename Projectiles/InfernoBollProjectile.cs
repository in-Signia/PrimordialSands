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
    public class InfernoBollProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Boll");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.penetrate = 1;
            projectile.aiStyle = 8;
            projectile.timeLeft = 300;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.wet)
            {
                projectile.Kill();
            }
            int num3;
            if (Main.myPlayer == projectile.owner)
            {
                for (int num350 = 0; num350 < 2; num350 = num3 + 1)
                {
                    float num351 = -projectile.velocity.X * (float)Main.rand.Next(20, 50) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
                    float num352 = -Math.Abs(projectile.velocity.Y) * (float)Main.rand.Next(30, 50) * 0.01f + (float)Main.rand.Next(-20, 5) * 0.4f;
                    Projectile.NewProjectile(projectile.Center.X + num351, projectile.Center.Y + num352, num351, num352, mod.ProjectileType("EmberFlameProjectile"), (int)((double)projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
                    num3 = num350;
                }
            }
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {              
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                Main.PlaySound(SoundID.Item66, projectile.position);
            }
            return false;
        }
    

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item66, projectile.position);
            Dust.NewDust(projectile.Center, 0, 0, 127, 0f, 0f, 100, default(Color), 1f);
        }
    }
}