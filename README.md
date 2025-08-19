

# SharpBIM.RevolutQRConverter

A helper tool to convert Slovenian UPN QR codes to a Revolut-compatible format.

## Overview

This project provides a utility to transform Slovenian UPN (Universal Payment Notification) QR codes into a format compatible with the Revolut mobile banking application. It is particularly useful for users in Slovenia who wish to streamline their payment processes by scanning UPN QR codes directly into Revolut.

The converter can be accessed through multiple platforms:

* **Web**: [https://slov-qr-revolout.tryasp.net/](https://slov-qr-revolout.tryasp.net/)
* **Android App**: Coming soon
* **Windows App**: Coming soon

## Features

* **UPN QR Code Parsing**: Extracts payment details from Slovenian UPN QR codes.
* **Revolut-Compatible Output**: Generates a QR code that can be scanned directly into the Revolut app.
* **Web Interface**: User-friendly interface for quick conversions.
* **Cross-Platform Plans**: Upcoming Android and Windows apps to expand accessibility.
* **Shared Library**: Provides a library (`SharpBIM.RevolutQRConverter.Shared`) for integration into other projects.

## Installation

### For Local Development

1. Clone the repository:

   ```bash
   git clone https://github.com/mostafa901/SharpBIM.RevolutQRConverter.git
   ```

2. Navigate to the project directory:

   ```bash
   cd SharpBIM.RevolutQRConverter
   ```

3. Open the solution file (`SharpBIM.RevolutQRConverter.sln`) in Visual Studio.

4. Restore NuGet packages.

5. Build the solution.

## Usage

### Web Interface

1. Open [https://slov-qr-revolout.tryasp.net/](https://slov-qr-revolout.tryasp.net/) in your browser.
2. Upload a UPN QR code image.
3. The tool will process the image and display a Revolut-compatible QR code.
4. Scan the generated QR code with the Revolut app to initiate the payment.

### Shared Library

To use the shared library in your own project:

1. Add a reference to the `SharpBIM.RevolutQRConverter.Shared` project.
2. Use the provided classes and methods to parse UPN QR codes and generate Revolut-compatible QR codes.

## Upcoming Platforms

* **Android App**: Will allow QR conversion on mobile devices.
* **Windows App**: Will provide a desktop version for offline usage.

## Contributing

Contributions are welcome! Please fork the repository, make your changes, and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

If you want, I can also create a **more visually appealing version with screenshots, badges, and platform icons**, which makes the README look more professional and modern. Do you want me to do that next?
