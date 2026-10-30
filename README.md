# Batteries Not Included: Fika scav fix

> [!WARNING]
> **AI-generated fix.** The Fika patch in this fork was written by AI coding agents. Not reviewed upstream. Use at
> your own risk. The original mod is by [ozen-m](https://github.com/ozen-m/SPT-BatteriesNotIncluded).

Fork of [ozen-m/SPT-BatteriesNotIncluded](https://github.com/ozen-m/SPT-BatteriesNotIncluded) v1.0.4 (MIT). Adds a
fix for Fika headless player-scav startup.

## Fix

Seen with SPT 4.0.13, Fika.Core 2.2.6, Fika.Headless 1.4.13. A player scav spawns unable to fire, reload with `R`,
throw grenades or fire picked-up weapons. Log: `TogglableComponent ... already in state True`, then
`hands controller can't perform this operation`.

- Initial scav/bot device toggle state is no longer sent through Fika inventory operations at raid start.
- After bot-battery sync, receiving clients recalculate device state and run enforcement.
- Prepatched headphones start powered on, same as the non-prepatched path.
- Bot-battery packet format unchanged.

Tested: 5 headless-hosted scav raids, all OK. Details: [PATCH_NOTES.md](PATCH_NOTES.md).

## Install

Install on every Fika participant: player clients and the headless client.

1. Download the ZIP from [Releases](https://github.com/Mjolniar/spt-batteries-not-included-fika-fix/releases/latest).
2. Extract into the SPT folder.
3. SPT Launcher: **Settings > Clean Temp Files**.

## Mod features (upstream)

- Battery slots on headsets, holographic/reflex sights, NVDs and tactical devices.
- Batteries: CR123A (replaces Rechargeable Battery), CR2032 (replaces D Size Battery), AA.
- Drain only while a device is on. Drain rate from the manufacturer's battery life, mapped to game time.
- Toggle component for headsets and sights.
- Item description shows battery type, count and full-charge runtime.
- Bots spawn with batteries. Charge depends on PMC, Scav or Boss.

## Config

Server, `config.jsonc`:

| Key | Default | Meaning |
|---|---|---|
| `globalDrainMultiplier` | `1.0` | Drain speed multiplier |
| `minGameRuntime` | `900` s | Game runtime of a device rated 1 real hour |
| `maxGameRuntime` | `9000` s | Game runtime of a device rated 100,000 real hours |
| `botBatteries` | PMC 50-100, Scav 20-60 | Bot battery charge range |
| `siccContainerBatteries` | `true` | Allow batteries in the SICC case |
| `debugLogs` | `false` | Debug logging |

Server, `customDevices.jsonc`: battery type, count and life (hours) for modded devices. Examples in the file.
Unlisted modded devices use CR2032.

Client, BepInEx config (F12): `Remaining Battery Tooltip`, default `true`.

## Compatibility

- Modded devices: supported. See `customDevices.jsonc`.
- [Project Fika](https://forge.sp-tarkov.com/mod/2326/project-fika): supported with the sync addon.

## Support

- Fika fix: [issues here](https://github.com/Mjolniar/spt-batteries-not-included-fika-fix/issues).
- Everything else: [upstream](https://github.com/ozen-m/SPT-BatteriesNotIncluded/issues) or ozen on the SPT Discord.

## Credits

- [ozen-m](https://github.com/ozen-m/SPT-BatteriesNotIncluded): Batteries Not Included.
- [Jiro](https://forge.sp-tarkov.com/user/32217/jiro): original [BatterySystem](https://forge.sp-tarkov.com/mod/948/batterysystem) and bundles.
- [Birgere](https://forge.sp-tarkov.com/user/59606/birgere): [BatterySystem for SPT 3.9.0](https://forge.sp-tarkov.com/mod/1708/batterysystem).
- [DrakiaXYZ](https://forge.sp-tarkov.com/user/27605/drakiaxyz): install GIF.

License: MIT ([LICENSE](LICENSE)).
