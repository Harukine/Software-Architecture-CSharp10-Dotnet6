# Azure Kubernetes Service
- This chapter provides an in-depth exploration of Kubernetes, the container orchestrator, with a focus on Azure Kubernetes Service (AKS). The chapter starts by explaining the fundamental concepts of Kubernetes and then delves into interacting with a Kubernetes cluster and deploying applications on Azure Kubernetes. The concepts are reinforced with practical examples. It is recommended to have prior knowledge of microservice architecture from Chapter 5 before reading this chapter, as the concepts from previous chapters will be utilized. The chapter covers Kubernetes basics, interacting with Azure Kubernetes clusters, and advanced Kubernetes concepts. By the end of the chapter, readers will have the knowledge to implement and deploy a comprehensive solution using Azure Kubernetes Service.

## Kubernetes basics
- Kubernetes is an open-source orchestrator widely used for cluster orchestration. It consists of a cluster of virtual machines called nodes, running the Kubernetes orchestrator. The basic unit in Kubernetes is a pod, which is a group of containerized applications. Pods running on the same node can easily communicate through localhost ports, while communication between different pods is more complex due to their ephemeral nature and the possibility of replication. Master nodes manage the cluster and communicate with administrators through an API server. The scheduler allocates pods to nodes based on constraints, and the controller manager monitors the cluster's state and works towards the desired state. Services handle communication between pods, providing constant virtual addresses to sets of pod replicas. Kubernetes entities can be labeled and selected using selectors. Stateless pods are organized in ReplicaSets, while stateful pod replicas are organized in StatefulSets, which use sharding to split traffic among the pods. Kubernetes does not provide predefined storage facilities, so long-term storage is typically provided through cloud databases or other cloud storage options. YAML files can be used to define Kubernetes entities, and once deployed, the entities specified in the file are created in the cluster.

### .yaml files
- .yaml files are used to describe nested objects and collections in a human-readable format. They have a different syntax than JSON files, using indentation for nested objects and hyphens for list items. Multiple sections can be included in a .yaml file, each defining a different entity. Sections are separated by "---" and comments are indicated by "#" symbols. Each section starts with the declaration of the Kubernetes API group and version, specifying the API name for objects belonging to different API groups.

### ReplicaSets and Deployments
- The ReplicaSet is a fundamental component in Kubernetes applications, representing a replicated set of pods. However, Deployments are commonly used to create and monitor ReplicaSets, ensuring a constant number of replicas despite hardware faults or other events. Deployments provide a declarative way to define ReplicaSets and pods.
- A Deployment includes a name, the desired number of replicas, a selector to choose the pods to monitor, and a template for creating pod replicas. The template specifies the metadata and specifications for the pod, including labels, container details (name, Docker image, resource requirements, ports, environment variables), and additional fields for virtual files and container readiness/health.
- Namespaces can be used to separate objects within a Kubernetes cluster, allowing independent applications to coexist. The optional namespace field can be included in a Deployment to specify the namespace for the objects.
- Overall, Deployments offer a higher-level abstraction for managing ReplicaSets and pods, providing robustness and scalability to Kubernetes applications.

### StatefulSets
- StatefulSets in Kubernetes are similar to ReplicaSets but with some key differences. While ReplicaSets consist of indistinguishable pods that contribute in parallel to a workload, StatefulSets are designed for storing information and require unique identities for each pod instance.
- StatefulSets maintain a unique identity for each pod and are responsible for sharding information rather than parallel processing. Each pod instance is associated with a specific virtual disk space, and ordinal numbers are assigned to the pod instances. The pod instances start and stop in sequence based on these ordinal numbers, with instance names formed by combining the pod name and instance ordinal.
- A typical StatefulSet definition includes the name, selector, replicas, and template sections, similar to Deployments. The significant difference is the addition of the serviceName field, which specifies the name of a service that provides unique network addresses for all pod instances. Storage is commonly used with StatefulSets.
- By default, StatefulSets follow an ordered creation and stop strategy, but this can be changed by specifying an explicit value for the podManagementPolicy property.
- Stable network addresses for both ReplicaSets and StatefulSets are covered in the subsequent subsection.

### Services
- Services in Kubernetes are responsible for assigning unique and stable virtual addresses to ReplicaSets and StatefulSets, as well as load balancing the traffic among their instances. Services work at level 4 of the protocol stack, understanding protocols like TCP but not performing higher-level actions like HTTPS security.
- ClusterIP services provide a unique internal IP address within the cluster and route traffic to the connected pod instances. They can't be accessed from outside the cluster. NodePort services expose pods to the outside world by opening a specific port on all cluster nodes and forwarding traffic to the associated ClusterIP service.
- LoadBalancer services expose pods by utilizing a level 4 load balancer provided by the cloud provider. They can assign a dynamic public IP or a specific public IP specified by the developer. In AKS (Azure Kubernetes Service), the resource group and static domain name can also be specified for the load balancer.
- For StatefulSets, headless services are used to provide a unique URL address for each pod instance without load balancing. The headless service has the clusterIP property set to "none".
- Services primarily support low-level protocols, so Kubernetes introduces higher-level entities called Ingresses to handle more sophisticated protocols like HTTP.

### Ingresses
- Ingresses in Kubernetes are designed to work with HTTP(S) and provide several services such as HTTPS termination, name-based virtual hosting, and load balancing. Ingresses rely on Ingress Controllers, which are custom Kubernetes objects installed in the cluster to handle the interface between Kubernetes and a web server.
- HTTPS termination and name-based virtual hosting can be configured in the Ingress definition independently of the chosen Ingress Controller. The configuration for load balancing depends on the specific Ingress Controller and its settings, which can be passed through annotations in the Ingress metadata.
- Name-based virtual hosting is defined in the Ingress definition's rules section, where each rule specifies an optional hostname with an optional wildcard. Multiple paths can be specified for each rule, redirecting to different service/port pairs.
- HTTPS termination for specific hostnames is achieved by associating the hostname with a certificate encoded in a Kubernetes secret. Certificates can be obtained from certificate authorities like Let's Encrypt, and they can be automatically installed and renewed using a certificate manager.
- The Ingress definition includes TLS configuration, rules for name-based virtual hosting, and other optional metadata.
- Ingresses provide a way to manage HTTP(S) traffic and routing within a Kubernetes cluster by leveraging Ingress Controllers and web server integration.

## Interacting with Azure Kubernetes clusters
- To create an AKS (Azure Kubernetes Service) cluster in Azure, you can follow these steps:
1. Search for "AKS" in the Azure search box and select "Kubernetes services."
2. Click on the "Create" button to start creating a new AKS cluster.
3. Fill in the required information, including subscription, resource group, and region.
4. Choose a unique name for your Kubernetes cluster and select the desired version of Kubernetes.
5. Specify the number of nodes and select a machine template (node size) for each node. For cost-saving purposes, you can choose a lower-cost virtual machine size.
6. Set the scale method to "Manual" to prevent automatic scaling of nodes and avoid excessive Azure credit consumption.
7. Optionally, you can configure availability zones for better fault tolerance by spreading nodes across multiple geographic zones. In this case, select two zones since you have two nodes.
8. Review the chosen settings and click on the "Review + create" button.
9. On the review page, confirm the settings and create the AKS cluster.
10. Wait for the deployment process to complete, which may take around 10-20 minutes.
- Once the cluster is created, you will have your first Kubernetes cluster in Azure.
- These steps guide you through the process of creating an AKS cluster in Azure, allowing you to leverage the power of Kubernetes for your applications.