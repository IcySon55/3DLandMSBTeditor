# MSBT Editor Reloaded
I created this editor a while ago to support a translation project I was planning; Which is now cancelled since Picross 3D 2 is slated for a North American release. It has since grown beyond anything I could have imagined and is now used by many in the modding community on all sorts of projects. I hope you find it useful in your own projects!

Notice: [For a more advanced and complete MSBT solution, check out Kuriimu.](https://github.com/IcySon55/Kuriimu)

### Supported MSBT Sections
* LBL1 - Full support
* NLI1 - Preliminary support. Most content is unknown.
* ATO1 - Preliminary support. Content is all 0xFF without padding.
* ATR1 - Preliminary support. Content is usually blank.
* TSY1 - Preliminary support. Content is unknown.
* TXT2 - Full support.

### Supported Games
* Super Mario 3D Land - Not tested.
* Mario Kart 7 - Supported.
* Super Mario Galaxy 2 - Partially tested.
* Super Smash Bros. 3DS - Supported.
* Super Smash Bros. WiiU - Supported.
* Mario & Luigi Paper Jam - Supported.
* Art Academy - Partially tested.
* Detective Pikachu - Supported.
* Miitomo - Partially tested.
* Animal Crossing: New Leaf - Supported.
* Tokyo Mirage Sessions #FE - Supported.
* Others may be supported, need feedback.

### XMSBT Translation Tools
* This is the MSBT translator's dream feature! This format allows translators to work together and collaborate on the same file(s) without losing any work due to files being overwritten or files being impossible to merge easily.
* Export an open MSBT file to the human readable XMSBT (XML) format.
* Import XMSBT files into an open MSBT file. Text is imported by matching the label names.
* Batch Export and Batch Import support all of the same features as the single file tools.
* To use the batch tools, keep the MSBT and XMSBT files in the same directory. (Export does this automatically, and Import expects it.)
* Export only the differences between two MSBT files to an XMSBT delta file using the new Export Mod feature!

### Other Features
* Find - I've added the ability to search for strings in the file making translation that much easier.
* Search Directory - Works like Find except that it searches all MSBT files in a directory. (Subdirectories supported)
* Export CSV - Exports the currently open file and any changes made to a CSV file.
* BG4 Extraction - Extract files from within BG4 archives found in games like Mario & Luigi Paper Jam.
* LZ11 De/Compression - Compress and Decompress files using LZ11. Held onto this feature from Exelix11's original version.
* UMSBT Extraction & Packing - Extract MSBT files from UMSBT archives and repack them into UMSBT archives.
* v0.9.7 will correct broken files that were previously broken by v0.9.6 and v0.9.5b. Just open the broken file and then click save to fix it. Broken files created by v0.9.5 and below can not currently be fixed with the program. Contact me directly if you need your heavily modified file fixed so you don't lose any work.

### Known Issues
* v0.8.6 - LZ11 Compression windows do not appear. Update to v0.9.0+.
* v0.9.6 and below - Label renaming is broken. Update to v0.9.7+.
