using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Bushido
{
	public class MomentumStrike : SamuraiSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "Momentum Strike", null, SpellCircle.Seventh, -1, 9002 );

		public override double RequiredSkill { get { return 70.0; } }
		public override int RequiredMana { get { return 10; } }

		public override int SpellNumber { get { return 0x96; } }

		public static Hashtable m_Table = new Hashtable();

		public static Hashtable m_Table2 = new Hashtable();

		public static Spell GetSpell( Mobile m )
		{
			return (Spell) m_Table2[ m ];
		}

		public static bool UnderEffect( Mobile m )
		{
			return m_Table.Contains( m );
		}

		public MomentumStrike( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
			{
				return false;
			}

			if ( BaseWeapon.UnderFail( Caster ) )
			{
				Caster.SendLocalizedMessage( 1063024 ); // You cannot perform this special move right now.

				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( UnderEffect( Caster ) )
			{
				Caster.Send( new SetNewSpell( SpellNumber, 0 ) );

				m_Table.Remove( Caster );

				FinishSequence();

				return;
			}

			Caster.Send( new SetNewSpell( SpellNumber, 1 ) );

			Caster.SendLocalizedMessage( 1070757 ); // You prepare to strike two enemies with one blow.

			m_Table[ Caster ] = true;

			m_Table2[ Caster ] = this;
		}
	}
}