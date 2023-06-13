# 2 Non-Functional Requirements
Non-functional requirements play a crucial role in the architectural design of a software system. Factors such as scalability, availability, performance, security, and interoperability need to be carefully analyzed to ensure that user needs are met. While they may not be directly related to the functionality of the software, non-functional requirements significantly impact the quality and effectiveness of the final product. Software architects must pay close attention to these aspects to distinguish between good and bad software.

## Scalability, availability, and resiliency
- Scalability is not solely dependent on hardware solutions; it also relies on well-architected software that can save costs. The running costs of a system should be considered when making architectural decisions. In cloud computing, resiliency is crucial due to potential system failures and the need to recover without affecting the end user. Working with multiple systems and dealing with transient errors necessitates a focus on resiliency Achieving high availability in a system is possible through scalable and resilient solutions. 

## Performance issues that need to be considered when programming in C#
[Performance Example Project](performance-issues)
1. String concatenation
- The performance issues that can arise from naive string concatenation using the + operator. Each concatenation operation involves copying the contents of the strings into a new string, resulting in increased cost. The overall cost grows exponentially with the number of strings and their average length. While this may not be a concern for small numbers of strings, it becomes problematic for larger quantities. To address this, the StringBuilder class is introduced as an alternative. By queuing the pointers of the strings and copying them only once when calling sb.ToString(), the StringBuilder-based concatenation offers improved performance, growing linearly with the number of strings and their average length. It is important to be mindful of such concatenation scenarios in code, especially in systems handling multiple simultaneous requests, to avoid performance bottlenecks that can impact non-functional requirements.
2. Exceptions
- It is important to use try-catch blocks concisely and only when necessary, as exceptions are significantly slower than normal code flow. A comparison between using try-catch and Int32.TryParse to convert a string into an integer demonstrates the performance difference. Exceptions should be reserved for exceptional cases that disrupt the normal flow of control, such as when operations need to be aborted due to unexpected reasons and control needs to be returned to a higher level in the call stack.
3. Multithreading
- When utilizing multithreading in software development, it is essential to consider the hardware benefits it offers. By allowing threads to relinquish the CPU to other threads while waiting for operations to complete, the system can make efficient use of available resources. However, implementing parallel code is not without its challenges. It is crucial for software architects to determine if their system truly requires multithreading, considering both non-functional and functional requirements.
- Once the decision to employ multithreading has been made, several options are available for implementation. These include creating threads using System.Threading.Thread, utilizing System.Threading.ThreadPool, leveraging System.Threading.Tasks.Parallel classes, or developing through asynchronous programming. Each approach offers different levels of control and simplicity.
- Regardless of the chosen implementation, there are important considerations for software architects to keep in mind. It is recommended to use concurrent collections (System.Collections.Concurrent) to ensure thread-safe operations. Static variables should be used cautiously, with attention given to potential issues arising from multiple threads accessing the same variable. Testing system performance after multithreading implementations is crucial to identify and address CPU usage and system slowdowns. Multithreading is not a simple task, and careful consideration must be given to aspects such as user interface synchronization, thread termination, and coordination. It is important to plan the number of threads needed for the system, particularly in 32-bit programs. Finally, ensuring proper termination procedures for each thread is vital to prevent memory leaks and handling issues.

## Usability - why inserting data takes too much time
- As a software architect, improving the performance of man-machine interaction is crucial for overall system effectiveness. Designing an effective user interface (UI) plays a significant role in achieving this goal. The UI should be easy to learn, ensuring fast interaction and minimizing data insertion slowdowns. Consideration should also be given to accessibility aspects to include a broader range of users.
- While there are UX experts available, as a software architect, you must determine when their expertise is essential for project success. Here are some tips for designing user-friendly interfaces:
- Clearly state the purpose of each input screen.
Use language that is familiar to the user, not developer jargon.
Avoid unnecessary complications and design for the average case, handling more complex scenarios with additional inputs when needed.
Leverage past inputs to understand user intentions and guide them with messages and automatic UI changes.
Error messages should not simply point out mistakes but also provide guidance on how to input the correct data.
Fast UIs can be achieved by addressing three key requirements:
- Order input fields based on the usual filling sequence, allowing users to navigate using the Tab or Enter key. Place fields that are often left empty at the bottom to minimize mouse usage and reduce user gestures.
System reactions to user inputs should be immediate. Error or information messages should appear as soon as the user leaves an input field. Move help and input validation logic to the client side to minimize delays caused by server communication.
Implement efficient selection logic to make it easy for users to select from a large number of options. Complex selection tasks should be simplified, allowing users to make selections with minimal effort and without requiring precise knowledge of product names or barcodes.

## Designing fast selection logic
- When designing selection logic for user interfaces, the approach can vary based on the number and complexity of options. Here are some considerations:

- For a small number of choices (1-50), a simple drop-down menu is sufficient.
- When the number of options is higher but still within a few thousand, an autocomplete feature that displays items starting with the typed characters is recommended.
- Complex search patterns are required for descriptions composed of multiple words. Full-text search support in the database can efficiently search for words typed by the user within descriptions.
- When dealing with descriptions consisting of names or complex strings, algorithms like the Levenshtein algorithm can be used to find the best match for the user's input. These algorithms compare the typed string with the descriptions and calculate a penalty based on missing characters and the distance between character occurrences.
- The descriptions are ranked based on the number of occurrences of typed characters and the penalty calculated. Sorting is done first by the number of occurrences and then by the lowest penalties.
- Implementing these selection logic techniques can enhance the user experience and facilitate efficient and accurate selection in user interfaces.
[Levenshtein Smart Search Example Project](smart-search)

## Selecting from a huge number of items
- When dealing with a large number of items (more than 10,000-100,000), it becomes challenging for users to remember the specific details of each item. In such cases, selecting the right item requires a hierarchical approach.
Cascading drop-down menus are a common solution for navigating through a hierarchy of categories. Each selection is performed through a series of input fields, where the options in each subsequent field depend on the values selected in the previous fields. For example, selecting a town from around the world can involve selecting the country first and then populating the next drop-down menu with towns from the chosen country.
In cases where multiple hierarchies need to be intersected to make the right selection, cascading drop-down menus may become inefficient. In such situations, a filter form can be used to facilitate the selection process.
Overall, when dealing with a large number of items, hierarchical navigation and filtering techniques can help users find and select the desired item effectively.

# The fantastic world of interoperability with .NET 6
- Applications developed with .NET 6 in Windows are largely compatible with Linux and macOS, meaning there's no need to rebuild the application for these platforms. Even platform-specific behaviors, like the System.IO.Ports.SerialPort class, have multi-platform support.
Additionally, differences between Linux and Windows, such as case-sensitivity and path separators, need to be considered when working with files. The Path class and its members can help ensure code compatibility across platforms.
.NET 6 provides runtime checks that allow code to adapt to the underlying operating system. By utilizing the RuntimeInformation.IsOSPlatform method, developers can conditionally execute code based on the platform, enabling platform-specific functionality or behavior.
In summary, .NET 6 brings interoperability to Windows developers, allowing them to target Linux and macOS. Consideration should be given to platform-specific differences and the use of runtime checks to adapt code accordingly.
## Creating a service in linux
- To create a service in Linux for a command-line .NET 6 app, follow these steps:

- Create a file that will run the command-line app. For example, let's assume the app is named "app.dll" and is located in the "appfolder." The service will check the application every 5,000 milliseconds. Use the following 
```bash
cat >sample.service<<EOF
[Unit]
Description=Your Linux Service
After=network.target
[Service]
ExecStart=/usr/bin/dotnet $(pwd)/appfolder/app.dll 5000
Restart=on-failure
[Install]
WantedBy=multi-user.target
EOF
```
- Once the file is created, copy the service file to a system location, reload the system, and enable the service to restart on reboots. Execute the following commands:
```bash
sudo cp sample.service /lib/systemd/system
sudo systemctl daemon-reload
sudo systemctl enable sample
```
- You're done! Now you can start, stop, and check the service using the following commands:
```bash
# Start the service:
sudo systemctl start sample
# View service status:
sudo systemctl status sample
# Stop the service:
sudo systemctl stop sample
```
- By following these steps, you can create a service that encapsulates your .NET 6 app on Linux, allowing it to run as a background service without the need for a logged-in user.

## Achieving security by design
- The opportunities and techniques for software development are expanding rapidly, especially with the advent of cloud computing. However, these opportunities come with increased complexity and responsibilities for software architects. The changing world, with its focus on apps, social media, Industry 4.0, big data, and artificial intelligence, requires a different approach to security.
Data protection regulations like the General Data Protection Regulation (GDPR) have a global impact and influence software development practices. As a software architect, it is important to prioritize security by design and have a specialist in information security on your team. This ensures the implementation of necessary policies and practices to prevent cyber attacks and maintain the confidentiality, privacy, integrity, authenticity, and availability of your services.
ASP.NET Core offers features to help protect applications, including authentication and authorization patterns. The OWASP (Open Web Application Security Project) Cheat Sheet Series provides valuable information on various .NET security practices. Additionally, ASP.NET Core provides APIs and templates to assist with GDPR compliance, such as policy declaration and cookie usage consent.

## List of practices for achieving a safe architecture
1. Authentication
- When designing a web application, it is important to consider the authentication method. There are various options available, such as ASP.NET Core Identity or external provider authentication methods like Facebook or Google. As a software architect, it is crucial to understand the target audience of the application and consider using Azure Active Directory (Azure AD) as a starting point, especially for internal usage.
- Azure AD can be integrated for managing the company's Active Directory. It offers options for B2B (Business to Business) or B2C (Business to Consumer) scenarios. In some cases, implementing Multi-Factor Authentication (MFA) might be necessary, which requires multiple forms of proof of identity. Azure AD provides support for MFA implementation.
- For the APIs provided by the web app, it is essential to determine an authentication method. JSON Web Token (JWT) is a recommended cross-platform pattern for this purpose.
- Regarding authorization, there are four model options to consider: simple, role-based, claims-based, and policy-based. The choice of model determines the access and permissions for each user. Additionally, the [AllowAnonymous] attribute can be used to allow access to certain controllers or methods without authentication. However, it is crucial to ensure that such implementation does not introduce vulnerabilities to the system.
- The chosen authentication and authorization models will define the level of access and actions that users can perform within the application.
2. Sensitive Data
3. Web Security