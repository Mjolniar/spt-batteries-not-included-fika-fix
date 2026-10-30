# Fika headless player-scav startup fix

> AI-generated fix. Original mod by ozen-m.

Based on `ozen-m/SPT-BatteriesNotIncluded` v1.0.4.

## Problem

Seen with SPT 4.0.13, Fika.Core 2.2.6, Fika.Headless 1.4.13. A player scav loads into raid with broken hands/firearm:

- Primary weapon does not fire.
- `R` reload fails (inventory mag install works).
- Grenades cannot be thrown.
- Picked-up weapons do not fire.

Logs: repeated Fika inventory conversion errors with `TogglableComponent ... already in state True`, sometimes followed by
`hands controller can't perform this operation`.

## Changes

- Batteries Not Included features unchanged.
- Initial scav/bot device toggle state is no longer sent through generic Fika inventory operations at raid start.
- Existing FikaSync bot-battery packet wire format kept.
- After bot-battery sync, receiving clients recalculate device operability/activity and run device enforcement.
- Prepatched headphones constructor path now starts powered headphones on, matching the non-prepatched path.

## Install

Install the patched build on every Fika participant using Batteries Not Included:

- Player client
- Headless/server client
- Every other client in the same raid

Mixed deployment is unsupported. The packet payload is unchanged, so an accidental mix should fail less badly than the
first prototype.

## Validation

Done: first 5 headless-hosted scav raids OK. Firing, `R` reload, grenades and picked-up weapons worked.

Still to test:

- Player scav with headset.
- Player scav without headset.
- Player scav with sights/tactical devices.
- PMC raid.
- AI scavs and bot PMCs with powered devices.
- Several Fika clients on one headless host, all on the patched build.

## Upstream

https://github.com/ozen-m/SPT-BatteriesNotIncluded (MIT)
