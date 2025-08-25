# Edit Personal Perk Stats

A comprehensive Phoenix Point mod for customizing personal perk statistics and effects with real-time configuration.

## Features

### Core Functionality
- **Real-Time Perk Modification**: Configure perk stats without restarting the game
- **Comprehensive Stat Control**: Modify accuracy, damage, AP cost, range, and other perk attributes
- **VE Description Updates**: Automatic Visual Elements description updates to reflect changes
- **Live Preview**: Changes are immediately reflected in soldier screens and tooltips

### Supported Perks
- **Assault Class**: Rapid Clearance, Onslaught, Berserker, Assault Training
- **Heavy Class**: Boom Blast, Heavy Training, Strongman, Rage Burst
- **Sniper Class**: Extreme Focus, Master Marksman, Sniper Training, Quarterback
- **Technician Class**: Tech Training, Hacking Protocol, Turret Deployment

### Advanced Configuration
- **Percentage-Based Adjustments**: Use percentage modifiers for balanced scaling
- **Absolute Value Overrides**: Set exact values for precise control
- **Multi-Stat Perks**: Configure complex perks with multiple effect components
- **Conditional Effects**: Per-situation stat modifications

## Installation

1. Extract to your Phoenix Point Mods directory
2. Enable "Edit Personal Perk Stats" in the Mods menu
3. Configure desired perk modifications in mod settings
4. Changes apply immediately - no restart required

## Configuration

Access the full configuration through the in-game mod settings:

### Basic Settings
- **Enable/Disable Individual Perks**: Toggle modifications per perk
- **Stat Multipliers**: Apply percentage-based changes
- **Absolute Overrides**: Set specific values

### Advanced Settings  
- **Description Updates**: Enable/disable automatic tooltip updates
- **Real-Time Application**: Configure when changes take effect
- **Compatibility Mode**: Adjust for other perk-modifying mods

## Technical Features

### Refactored Architecture
- Simplified main class removing dual bootstrap systems
- Direct DefRepository access for better performance
- Maintained compatibility with existing patch systems
- Reduced complexity while preserving full functionality

### Patch System
- **Harmony Integration**: Non-destructive runtime patching
- **Geoscape Updates**: Real-time stat display in soldier management
- **Tactical Integration**: Changes apply during missions
- **UI Synchronization**: Tooltips and descriptions stay current

## Compatibility

Designed for maximum compatibility with other Phoenix Point mods:
- Uses non-destructive Harmony patches
- Preserves original perk functionality
- Compatible with other stat modification mods
- Tested with major gameplay overhauls

## Build Instructions

1. Ensure ModSDK is available in the parent directory
2. Build using Visual Studio or MSBuild
3. Compiled mod will be output to the Dist folder
4. Copy entire Dist contents to Phoenix Point Mods directory

## Version History

- **Current**: Refactored codebase with improved stability and performance
- **Previous**: Complex reflection-based system replaced with direct access
- **Legacy**: Multiple bootstrap systems consolidated into single entry point

## Credits

Developed for the Phoenix Point modding community. Refactored for improved maintainability and reliability.