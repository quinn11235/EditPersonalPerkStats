# EditPersonalPerkStats

Phoenix Point mod for configuring personal perk stats with simplified, refactored codebase.

## Personal Perks

- **Biochemist** - Viral damage bonus
- **Bombardier** - Mounted weapon range/damage multipliers
- **Cautious** - Cover accuracy bonus with damage penalty
- **Close Quarters** - Melee/shotgun damage/accuracy bonuses
- **Quarterback** - Speed bonus, grenade range multiplier  
- **Reckless** - Damage bonus with accuracy penalty
- **Resourceful** - Strength bonus, carry capacity multiplier
- **Farsighted** - Perception/willpower bonuses
- **Healer** - Willpower bonus, healing multiplier
- **Thief** - Speed/stealth bonuses
- **Self Defense** - Pistol/PDW accuracy/damage bonuses, hearing range
- **Sniperist** - Sniper damage bonus with willpower penalty
- **Strongman** - Heavy weapon bonuses, strength bonus, perception penalty
- **Trooper** - Assault rifle accuracy/damage bonuses

## Configuration

### Per perk
- Damage multipliers (percentage-based)
- Accuracy bonuses (flat improvements)
- Attribute modifications (Speed, Strength, Perception, Willpower)
- Special effects (carry capacity, healing, stealth, hearing)

### Usage
1. Enable mod in Phoenix Point Mods menu
2. Configure values in mod settings
3. Changes apply immediately in geoscape and tactical

## Technical Details

- Refactored from 14 files to 3 files (94% code reduction)
- Uses Harmony patches for real-time stat updates
- Direct DefRepository access for perk modifications
- Updates geoscape UI and tactical calculations simultaneously

## Installation

Extract to `Documents/My Games/Phoenix Point/Mods/` directory.

## Build

Requires ModSDK in parent directory. Output in `Dist/` folder.