# MDM Assassin
### Fuck outta here, your shit water whip.

## ⚡ MDM Assassin: The Ultimate MDM Bypass Tool

### 🚀 About
MDM Assassin is a powerful tool designed to obliterate MDM restrictions, bypass limitations, and reclaim full control over your device. Built for those who refuse to be locked down.

**⚠ Disclaimer:** This tool is for **educational and security research purposes only**. Do not use it for unauthorized access or illegal activities.

### 🔥 Features
✅ MDM Removal – Wipe out corporate control like it never existed
✅ Factory Reset Bypass – Reset even with the latest security patches
✅ Bootloop Trigger – Soft brick stubborn devices to force a fresh start
✅ ADB Mastery – Push, pull, and execute commands like a boss
✅ MTP Mode QR Generator – Generate QR codes for seamless configurations

### 📌 Requirements
- **ADB & Fastboot installed** ([Download](https://developer.android.com/studio/releases/platform-tools))
- USB Debugging enabled on the device
- Compatible drivers for device communication
- Python 3.x (For advanced scripts)

### 🔧 Installation
1. Clone the repo:
   ```sh
   git clone https://github.com/Melsun-Enterprises/MDM-Assassin.git
   cd MDM-Assassin
   ```
2. Run the setup:
   ```sh
   chmod +x setup.sh && ./setup.sh   # For Linux/Mac
   setup.bat  # For Windows
   ```
3. Build the project:
   dotnet build
   
5. Run the tool:
   dotnet run


### 🛠 Usage
Remove MDM:
adb shell pm uninstall -k --user 0 com.android.mdm

## Trigger Bootloop (Soft Brick Method):
adb shell rm -rf /data/system/gesture.key
adb reboot

### Factory Reset Patch Override:

bash
Copy
Edit

### 🤝 Contributors
Want to join the chaos? Submit your pull requests and let's make MDM vendors cry.

### 🌍 Community & Support
- **Telegram**: [Join Here](#)
- **Discord**: [Coming Soon]
- **Twitter/X**: [@MDM_Assassin](#)

🔥 Ready to wipe out restrictions? Let’s build MDM Masterstroke! 🚀😈

