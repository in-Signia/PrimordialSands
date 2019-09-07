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
    public class PoisonGas2Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poisonous Chemicals");
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.penetrate = 1;
            projectile.aiStyle = 92;
            projectile.timeLeft = 240;
            projectile.ranged = true;
            projectile.hostile = true;
            projectile.tileCollide = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.position);
            Dust.NewDust(projectile.Center, 0, 0, 89, 0f, 0f, 100, default(Color), 1f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 240);
        }
    }
}