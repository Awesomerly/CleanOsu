# CleanOsu Fix

CleanOsu is a program most likely written by [dukambe](https://osu.ppy.sh/community/forums/posts/3157632) that replaces song backgrounds and deletes commonly unused map-specific skin elements. Due to an [update](https://osu.ppy.sh/home/changelog/stable40/20250122.1) to the osu! stable client, the original version of this program broke.

I decompiled the original exe with dnSpyEx and fixed a few things:
- Fix broken registry path
- Migrated from an old version of .NET framework to .NET 8.0
- Changed some code/added a package to make user.config work properly
- Changed obsolete form of `MD5.Create()`
- Add more elements to delete instead of replace image for (sliderendcircle, sliderstartcircle)

## DISCLAIMER
[TcNo-osu!cleaner](https://github.com/TCNOco/TcNo-osu-Cleaner) is probably superior to CleanOsu, but I haven't tried it.
