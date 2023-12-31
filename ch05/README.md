# 5 Applying a Microservice Architecture to Your Enterprise Application
- This chapter focuses on highly scalable architectures using microservices, which are small modules that enable fine-grained scaling and independent evolution. The chapter covers the concept of microservices, their benefits, how .NET handles them, and the necessary tools for managing microservices. By the end of the chapter, you will have gained knowledge on implementing a microservice in .NET.

## What are microservices?
- Microservice architectures provide independent scalability of modules, reducing resource waste and overall cost. They enable the development, maintenance, and deployment of software modules independently. This improves the CI/CD cycle by allowing scaling on different hardware, eliminating compatibility constraints, simplifying job organization, utilizing appropriate technologies, matching developers' competencies, and integrating legacy subsystems with newer ones. The section also discusses the origins of microservices and their design principles, including the use of Docker containers.

### Microservices and the evolution of the concept of modules
- Microservices represent an evolution in the concept of deployment modularity, building upon the foundations of code modularity. They emerged as a response to the limitations of DLL-based modularity and the challenges of version compatibility. The progression from monolithic executables to static libraries, dynamic link libraries (DLLs), and then to platforms like .NET and Java facilitated deployment on different hardware and operating systems. However, the inability to handle different versions of shared dependencies led to the adoption of package management systems and Service-Oriented Architecture (SOA), where deployment units were implemented as web services. Microservices, as an evolution of SOA, enhance scalability and modularity, improving the CI/CD cycle. They are considered a refined implementation of SOA with additional features and constraints.

### Microservices design principles
- The microservice architecture is an extension of SOA that emphasizes independence and fine-grained scaling. Understanding the benefits of microservice independence and scaling, we can now explore the design principles that stem from these constraints. Each principle will be discussed in its own subsection.

#### The independence of design choices
- The principle of independence of design choices in microservices ensures that each microservice can have its own independent design and implementation decisions. This allows for flexibility in choosing the most suitable technologies for each microservice. Additionally, microservices should not share the same storage, as this would entail sharing design choices and structure. Instead, microservices can have dedicated data storage, either by having exclusive access to a separate database or by implementing a logical microservice split into multiple physical microservices for better load balancing.

#### Independence from the deployment environment
- Microservices should be independent from the deployment environment. They should not rely heavily on services provided by the operating system or other installed software. By being less dependent on the environment, microservices can be deployed on a wider range of hardware nodes and allow for better optimization. This is why microservices are often containerized using technologies like Docker. Containerization enables each microservice to package its dependencies, making it portable and capable of running anywhere.

#### Loose coupling
- The principle of loose coupling in microservices involves two aspects. Firstly, the interface exposed by each microservice should be general rather than specific, following object-oriented programming principles. Secondly, the communication among microservices should be minimized to reduce costs. Since microservices run on different hardware nodes and do not share the same address space, minimizing communication helps maintain loose coupling and improves overall system efficiency.

#### No chained requests/responses
- The principle of no chained requests/responses in microservices states that a microservice should not trigger a recursive chain of nested requests/responses to other microservices, as it would result in unacceptable response times. This can be avoided by synchronizing the private data models of microservices through push events, ensuring that each microservice has all the necessary data to serve incoming requests without needing to rely on other microservices for data retrieval. Communication of data changes should be done through asynchronous messages to prevent performance issues caused by synchronous nested messages. Additionally, best practices for building a reusable service-oriented architecture (SOA) are automatically enforced by tools and frameworks used to implement web services. Fine-grained scaling of microservices requires complex infrastructure and resilience measures to handle communication and failures, such as exponential retries and circuit break strategies. Congestion propagation is prevented through bulkhead isolation techniques, and designers should strive to make messages idempotent to avoid duplicate processing. Techniques like message identification and storage can help ensure idempotency. Message brokers like Azure Service Bus offer facilities to implement these techniques.

### Containers and Docker
- Containers and Docker provide a solution for deploying microservices that are independent of the hosting environment. Containers are a lightweight form of virtual machines that virtualize the OS filesystem level without the overhead of starting a whole OS for each instance. Docker is a popular container image format and runtime that allows the creation of isolated environments for containers. Images are built by layering new files and configuration information on top of existing images, and they can be grouped into public or private registries. Docker Compose is a tool used to specify the deployment of multiple images and how their internal resources are mapped to the host machine's resources. Containerized microservices are often deployed and load-balanced on clusters managed by orchestrators. Understanding microservices, their advantages, and design principles allows us to determine when and how to use them in our system architecture.

## When do microservices help?
1. Layered architectures and microservices
2. When is it worth considering microservice architectures?

### Layered architectures and microservices
- Layered architectures are commonly used in enterprise systems, with different layers responsible for specific functionalities and data processing. Each layer interacts with the layer above and below it, ensuring independence and modularity. Microservices can be integrated into a layered architecture, spanning across different layers depending on their functionalities. Some microservices may exist in the business layer, some in the data layer, and there can also be microservices that span both layers. However, the relationship between microservices and the presentation layer is not explicitly discussed in the given text.

#### The presentation layer
- The presentation layer in a microservice architecture can be implemented on the server side. Single-page applications and mobile applications typically connect directly to the business microservices or through an API gateway. Implementing the presentation layer as microservices is suitable for websites, but heavy web servers and frameworks may not be ideal for containerization. ASP.NET Core is a lightweight framework that can be efficiently containerized and used in worker microservices. However, frontend and high-traffic websites often require specialized components, such as ingresses, to handle security and load balancing. Microservice architectures can also be applied to monolithic websites to break them into load-balanced smaller subsites or to construct a single HTML page using micro-frontends. The HTML chunks can be combined on the server side or directly in the browser. The adoption of microservices should be based on specific rules and considerations.

### When is it worth considering microservice architectures?
- The decision to adopt a microservice architecture should consider the costs associated with allocating instances, scaling, increased communication costs, and the complexity of designing and testing the software. Microservices are worth considering when the application is large in terms of traffic and complexity, and the benefits of scaling optimization and development outweigh the costs. Microservice adoption can be justified by improved overall application throughput, increased market value, and the need to address coordination challenges in large development teams. Additionally, the integration of newer subparts with legacy systems and the presence of developers with experience in different development stacks may require a microservice architecture.

## How does .NET deal with microservices?
- The .NET framework, particularly ASP.NET Core, is well-suited for implementing microservices due to its multi-platform compatibility, lightweight nature, and efficient support for text-REST and binary gRPC APIs. The .NET stack has evolved with microservices in mind, providing facilities and packages for building resilient services, handling long-running tasks, and facilitating efficient communication using HTTP and gRPC. Various tools and solutions are available within the .NET ecosystem to implement a microservice architecture.

### .NET communication facilities
- Microservices require two types of communication channels. The first type is for receiving external requests, typically using HTTP as the protocol, and ASP.NET Core is a lightweight framework suitable for implementing web APIs in microservices. The second type of communication channel is used for pushing updates to other microservices. Asynchronous communication is ideal for performance reasons, and a publisher/subscriber pattern is preferred to achieve independence and scalability among microservices. The publisher/subscriber pattern allows microservices to publish events, which are then distributed to interested subscribers by a service responsible for queuing and dispatching events.
- while .NET itself doesn't provide tools for asynchronous communication or publisher/subscriber communication, Azure offers Azure Service Bus as a service that can handle these types of communication. Azure Service Bus supports both queued asynchronous communication and publisher/subscriber communication through queues and topics. It can be accessed using the Microsoft.Azure.ServiceBus NuGet package. Queue-based communication involves sending messages to a queue and having receivers pull messages from the same queue, while topic-based communication allows multiple subscribers to pull messages from their private subscriptions associated with a topic. Other alternatives to Azure Service Bus include NServiceBus, MassTransit, and Brighter. Additionally, RabbitMQ is a free and open-source option that can be used for both on-premises and cloud platforms, offering similar functionalities but requiring more implementation details. Tools like EasyNetQ provide a higher-level abstraction on top of RabbitMQ.

### Resilient task execution
- Polly is a .NET library that helps implement resilient communication and task execution. It is available as a NuGet package. With Polly, you define policies that handle specific exceptions and specify actions to take when those exceptions occur. For example, you can define retry policies that retry a failed task a certain number of times or with a specific delay between retries. You can also implement circuit breaker policies that prevent task execution for a defined period after a certain number of failures, and bulkhead isolation policies that limit the number of parallel executions and queue excess tasks. Policies can be combined using the Wrap method. Polly offers additional features such as timeout policies, task result caching, and the ability to define custom policies. It can be configured as part of an HttpClient definition in ASP.NET Core and .NET applications.

### Using generic hosts
- the concept of hosts and hosted services in .NET provides an environment for running multiple tasks within a microservice. The Microsoft.Extensions.Hosting NuGet package contains the necessary features for creating hosts. The host is automatically created in ASP.NET Core, Blazor, and Worker Service projects. The host configuration involves defining resources, setting default file folders, loading configuration parameters from different sources, and declaring hosted services. Hosted services implement the IHostedService interface and have StartAsync and StopAsync methods for starting and stopping the service. They can be declared in the ConfigureServices method along with other host options. BackgroundService is an abstract class that simplifies the implementation of IHostedService by providing the ExecuteAsync method. Hosted services can use dependency injection to receive resources through constructor parameters. The IHostApplicationLifetime interface allows a hosted service to shut down the host. HostBuilder methods are available for defining the default folder, configuring logging, and reading configuration parameters from various sources.

### Visual Studio support for Docker
- Visual Studio provides support for creating, debugging, and deploying Docker images. To utilize this support, Docker Desktop for Windows needs to be installed on the development machine. Once Docker Desktop is installed and running, Visual Studio allows for the creation of Docker images.
- To demonstrate Docker support, an example is given using an ASP.NET Core MVC project. Docker support can be added to an existing project by right-clicking on the project icon in Solution Explorer, selecting "Add," then "Container Orchestrator Support," and finally "Docker Compose." The operating system, either Windows or WSL (Windows Subsystem for Linux), can be chosen depending on the installation.
- Enabling Docker Compose in Visual Studio allows for the configuration of Docker Compose files, which can run and deploy multiple Docker images simultaneously. By adding another MVC project with container orchestrator support, the new Docker image will be included in the same Docker Compose file.
- Once Docker Compose is enabled and the Docker runtime is properly installed and running, the Docker image can be executed directly from Visual Studio.

[Example MVC Docker](MvcDockerTest)

#### Analyzing the Docker file
- The Dockerfile generated by Visual Studio consists of a sequence of image creation steps. Each step builds upon an existing image using the "FROM" instruction. The first step uses the ASP.NET (Core) runtime image provided by Microsoft. It sets the working directory to "/app" and exposes ports 80 and 443.
- The second step starts from the ASP.NET SDK image and creates a separate image for building application-specific files. It sets the working directory, copies the project file, restores NuGet packages, copies the entire project file tree, builds the project in release mode, and publishes the binaries.
- The final step starts from the base image created in the first step (containing the ASP.NET runtime) and adds the published files from the previous step. The working directory is set, and the entry point command is specified as "dotnet MvcDockerTest.dll".
- Overall, the Dockerfile creates an image that includes the necessary runtime and application-specific files, allowing the application to be executed within a Docker container.

[Example Dockerfile](MvcDockerTest/MvcDockerTest/Dockerfile)

#### Publishing the project
- When publishing the project, there are options to publish the image to an existing or new web app created by Visual Studio, or to publish to Docker registries, including a private Azure Container Registry that can be created within Visual Studio. Docker Compose support allows running and publishing a multi-container application with additional images, such as a containerized database.
- The Docker Compose file provided in the example includes two ASP.NET applications within the same Docker image. It references existing Docker files and includes environment-dependent information in the docker-compose.override.yml file, which is merged with the docker-compose.yml file when launching the application from Visual Studio.
- The docker-compose.override.yml file specifies environment variables, port mappings, and host file mappings. The volumes are used to map the self-signed HTTPS certificate used by Visual Studio.
- To add a containerized SQL Server instance, additional instructions split between docker-compose.yml and docker-compose.override.yml are needed. These instructions specify the properties of the SQL Server container, including configuration, installation parameters, environment variables, and port mappings.
- It is possible to add further docker-compose-xxx.override.yml files for different environments (e.g., staging, production) and launch them manually in the target environment using the docker-compose command.

### Azure and Visual Studio support for microservice orchestration
- Azure and Visual Studio provide support for microservice orchestration. Visual Studio offers project templates for defining microservices to be deployed in Azure Kubernetes and extensions for debugging a single microservice while it interacts with other microservices deployed in Azure Kubernetes.
- Additionally, there are tools available for testing and debugging multiple communicating microservices on the development machine without the need to install Kubernetes software. These tools also facilitate the automatic deployment of microservices on Azure Kubernetes with minimal configuration requirements.

## Which tools are needed to manage microservices?
- To manage microservices effectively in CI/CD cycles, you need two essential tools: a private Docker image registry and a sophisticated microservice orchestrator. These tools enable the following capabilities:
1. Allocation and Load Balancing: The microservice orchestrator should be capable of allocating and load-balancing microservices across available hardware nodes. This ensures efficient utilization of resources and optimal performance.
2. Health Monitoring and Fault Recovery: The orchestrator should monitor the health state of services and automatically replace faulty services in the event of hardware or software failures. This ensures high availability and resilience of the microservices.
3. Logging and Analytics: The orchestrator should provide logging capabilities to track and analyze the behavior of microservices. It should also offer features for presenting analytics, enabling insights into the performance and behavior of the microservices.
4. Dynamic Configuration: The orchestrator should allow designers to dynamically change requirements, such as adjusting the allocation of hardware nodes to a cluster or scaling the number of service instances. This flexibility enables adaptation to changing demands and optimizes resource utilization.
- By leveraging a private Docker image registry and a robust microservice orchestrator, organizations can effectively manage their microservices throughout the development lifecycle, ensuring scalability, reliability, and operational efficiency.

### Defining your private Docker registry in Azure
- To define your private Docker registry in Azure, follow these steps:
1. In the Azure portal, search for "Container registries" and select it from the results.
2. Click on the "Create" button to start creating a new registry.
3. Fill in the required information, such as the name of the registry, subscription, resource group, and location.
4. Choose the appropriate SKU from the dropdown, considering performance and available memory options.
5. Once the registry is created, the registry URI will be in the format \<name\>.azurecr.io.
6. When referencing image names in Docker commands or Visual Studio, prefix them with the registry URI, such as \<name\>.azurecr.io/\<my imagename\>.

- To publish images to your Azure registry:
1. If using Visual Studio, follow the instructions that appear after publishing the project.
2. If not using Visual Studio, use Docker commands to push the images into your registry.
3. Install the Azure CLI on your computer from https://aka.ms/installazurecliwindows.
4. Log in to your Azure account using the command `az login`.
5. Once logged in, log in to your private registry using the command `az acr login --name {registryname}`.
6. Pull the Docker image from another registry to your local computer using `docker pull other.registry.io/samples/myimage`.
7. Tag the image with the desired path in the Azure registry using `docker tag myimage myregistry.azurecr.io/testpath/myimage`.
8. Push the tagged image to your Azure registry using `docker push myregistry.azurecr.io/testpath/myimage`.
9. Optionally, you can specify a version when tagging and pushing the image.
10. You can remove the image from your local computer using `docker rmi myregistry.azurecr.io/testpath/myimage`.
- By following these steps, you can define and use your private Docker registry in Azure to manage and publish your Docker images.

## Summary
- we discussed the concept of microservices and their evolution from modules. We explored the benefits of using microservices and the criteria for designing them. We also examined the relationship between microservices and Docker containers.
Moving into implementation, we explored the tools available in .NET for building microservice architectures. We discussed the necessary infrastructure for microservices and how Azure provides container registries and orchestrators.
Overall, provided an overview of microservices, their advantages, and practical considerations for implementing them using .NET tools and Azure infrastructure.