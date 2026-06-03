# Fika Headless Player-Scav Startup Fix

This fork is based on `ozen-m/SPT-BatteriesNotIncluded` v1.0.4.

It carries a local compatibility patch for a Fika/headless player-scav startup issue where a player scav could load into raid with broken hands/firearm behavior:

- Primary weapon would not fire.
- `R` reload would not work, while inventory mag install still worked.
- Grenades could not be thrown.
- Picked-up weapons also would not fire.

The issue was observed with SPT 4.0.13, Fika.Core 2.2.6, and Fika.Headless 1.4.13. Logs showed repeated Fika inventory conversion errors involving `TogglableComponent ... already in state True`, followed in some cases by `hands controller can't perform this operation`.

## Patch Summary

- Keeps Batteries Not Included functionality intact.
- Avoids sending initial scav/bot device toggle state through generic Fika inventory operations during raid startup.
- Keeps the existing Batteries Not Included FikaSync bot-battery packet wire format.
- After bot-battery sync, recalculates device operability/activity and runs device enforcement on receiving clients.
- Fixes the prepatched headphones constructor path so powered headphones start in the same default-on state as the non-prepatched path.

## Install Notes

Install the patched build on every Fika participant that uses Batteries Not Included:

- Player client
- Headless/server client
- Any additional clients in the same Fika raid

Install the patched build everywhere you use Batteries Not Included with Fika so behavior is consistent. The bot-battery sync packet payload is intentionally unchanged by this patch, so accidental mixed deployment should fail less severely than the first local prototype, but it is still not a supported test configuration.

## Validation

Initial local validation: first 5 headless-hosted scav raids succeeded. Firing, `R` reload, grenade throws, and picked-up weapon firing all worked.

Recommended broader validation:

- Player scav with headset.
- Player scav without headset.
- Player scav with sights/tactical devices.
- PMC raid.
- AI scavs and bot PMCs using powered devices.
- Multiple Fika clients connected to the same headless host, all using the same patched build.

## Upstream

Original project: https://github.com/ozen-m/SPT-BatteriesNotIncluded

Original license: MIT
