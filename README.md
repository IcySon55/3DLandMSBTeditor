# MSBT Editor Reloaded
This is a heavily modified version of Exelix11's MSBT file editor.

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
* Import XMSBT files into an open MSBT file. Text is imported by matching the label names. Names that do not match are not imported.
* I plan to add batch functionality for XMSBT in a later version.

### Other Features
* Find - I've added the ability to search for strings in the file making translation that much easier.
* Search Directory - Works like Find except that it searches all MSBT files in a directory. (Subdirectories supported)
* Export CSV - Exports the currently open file and any changes made to a CSV file.
* BG4 Extraction - Extract files from within BG4 archives found in games like Mario & Luigi Paper Jam.
* LZ11 De/Compression - Compress and Decompress files using LZ11. Held onto this feature from Exelix11's original version.
* UMSBT Extraction & Packing - Extract MSBT files from UMSBT archives and repack them into UMSBT archives.

### Known Issues
* v0.8.6 - LZ11 Compression windows do not appear. Update to v0.9.0+.
