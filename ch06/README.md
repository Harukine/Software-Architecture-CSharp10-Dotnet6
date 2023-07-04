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

### Using Kubectl
- To interact with your AKS (Azure Kubernetes Service) cluster, you can use Azure Cloud Shell, which provides a Bash shell environment within the Azure portal. Alternatively, you can install Azure CLI on your local machine along with the necessary tools like kubectl and Helm. However, for simplicity, the instructions assume the use of Azure Cloud Shell.
- Here are the steps to use kubectl, the command-line tool for Kubernetes, to interact with your cluster:
1. Access Azure Cloud Shell by clicking on the console icon in the top right corner of the Azure portal page.
2. Choose the Bash Shell when prompted and create a storage account if required.
3. In Azure Cloud Shell, use the file icon at the top to upload your .yaml files containing Kubernetes configurations.
4. To interact with your AKS cluster, you need to activate the cluster credentials. Run the following command in Azure Cloud Shell, replacing <resource group> and <cluster name> with your specific values:
`az aks get-credentials --resource-group <resource group> --name <cluster name>`
- This command retrieves the cluster credentials and stores them in a config file located in the /.kube directory, allowing you to authenticate with the cluster.
5. Now you can use kubectl commands without further authentication. For example, running kubectl get nodes will display a list of all the Kubernetes nodes in your cluster.
- `kubectl get <object type>` lists all objects of a given type, such as nodes, pods, or stateful sets.
- `kubectl get all` shows a list of all objects created in your cluster.
- `kubectl get <object type> <object name>` provides detailed information about a specific object.
- Adding the --watch option to a kubectl get command continuously updates the object list, allowing you to observe the state changes over time. You can exit the watch mode by pressing Ctrl + C.
6. The `kubectl describe <object name>` command provides a detailed report on a specific object.
7. To create objects defined in a .yaml file, use the `kubectl create -f <filename.yaml>` command. For example, `kubectl create -f myClusterConfiguration.yaml`.
8. If you make modifications to the .yaml file, you can apply those changes to the cluster using the `kubectl apply -f <filename.yaml>` command. This command updates the existing resources if they already exist, while create would return an error in such cases.
9. To delete objects created from a .yaml file, use the `kubectl delete -f <filename.yaml>` command. It will remove all the objects defined in the file.
10. Alternatively, you can specify specific objects to delete by passing the object type and a list of object names. For example, `kubectl delete deployment deployment1 deployment2...` will delete multiple deployments.
11. These kubectl commands should cover most of your practical needs. For more detailed information, refer to the official documentation.

- By following these steps, you can effectively use kubectl to interact with your AKS cluster and manage the Kubernetes resources within it.

### Deploying the demo Guestbook application
- To deploy the Guestbook application, which is a demo application used in the official Kubernetes documentation, follow these steps:
1. The Guestbook application consists of a UI tier implemented with a Deployment and a database layer implemented with Redis. There are three .yaml files for the application, available in the associated GitHub repository.
2. Upload the redis-master.yaml file to Azure Cloud Shell and deploy it to the cluster using the following command: `kubectl create -f redis-master.yaml`. This creates a Deployment with a single replica and a ClusterIP Service that exposes the Deployment on port 6379 at the internal network address `redis-master.default.svc.cluster.local`.
3. Inspect the cluster content using `kubectl get all` to verify the successful deployment of the Redis master.
4. Upload the redis-slave.yaml file and deploy it with two replicas by running the following command: `kubectl create -f redis-slave.yaml`. This creates the slave storage for Redis, similar to the previous step but with different Docker images.
5. Upload the frontend.yaml file, which contains the code for the UI tier, and deploy it with the following command: `kubectl create -f frontend.yaml`. This creates a Deployment with three replicas and a LoadBalancer type Service that exposes the application on a public IP address.
6. Use `kubectl get service` to retrieve the public IP address assigned to the LoadBalancer Service. The EXTERNAL-IP column in the output will show the IP address. If it shows <none>, repeat the command until the IP address is assigned.
7. Access the application by navigating to the obtained IP address in your browser. You should see the home page of the Guestbook application.
8. After experimenting with the application, remove it from the cluster to avoid unnecessary resource consumption and cost by executing the following commands:
- `kubectl delete deployment frontend redis-master redis-slave`
- `kubectl delete service frontend redis-master redis-slave`
- This will delete the Deployments and Services associated with the Guestbook application.

- By following these steps, you can deploy the Guestbook application on your Kubernetes cluster, access it through the assigned IP address, and clean up the resources when no longer needed to avoid unnecessary costs.

## Advanced Kubernetes Concepts
- This section covers various important features of Kubernetes:
1. Permanent storage for StatefulSets: Explains how to provide persistent storage to StatefulSets, which are used for applications requiring stable network identities and storage.
2. Storing secrets securely: Covers the concept of Kubernetes secrets and how to store sensitive information like passwords and certificates.
3. Container health monitoring: Describes how containers can communicate their health state to Kubernetes using probes, enabling Kubernetes to manage containers based on their health.
4. Managing complex applications with Helm: Introduces Helm, a Kubernetes package manager that simplifies the deployment and management of intricate applications.

### Requiring permanent storage
- When it comes to providing permanent storage for pods in Kubernetes, there are two options: using external databases or utilizing cloud storage. This section focuses on cloud storage as a more flexible and scalable solution.
- Kubernetes introduces PersistentVolumeClaim (PVC), an abstraction of storage that allows pods to request and utilize persistent storage resources. In cloud-based Kubernetes clusters, dynamic allocation is typically used through the cloud provider's dynamic storage provisioner.
- Cloud providers, like Azure, offer different storage classes with varying performance and costs. PVCs can specify the access mode as ReadWriteOnce, ReadOnlyMany, or ReadWriteMany, defining the allowed access to the volume by pods.
- To incorporate permanent storage into StatefulSets, volumeClaimTemplates are added to the StatefulSet's specification, specifying the desired storage requirements and access mode. Each container within the StatefulSet must also specify the volume mount path to attach the permanent storage.
- By leveraging cloud storage and PVCs, Kubernetes allows pods to have reliable and persistent storage, ensuring data availability and scalability.

### Kubernetes secrets
- Kubernetes provides a feature called secrets for securely storing sensitive information. Secrets are key-value pairs that can be encrypted to protect them. There are multiple ways to create secrets using the kubectl command.
- Secrets can be created by specifying the values in files and using the --from-file flag, where the filenames become the keys and the file contents become the values. Alternatively, when the values are strings, they can be directly specified using the --from-literal flag, with keys and values listed one after the other.
- Once secrets are defined, they can be referenced in the spec->volume property of a pod (Deployment or StatefulSet template). Each container within the pod can specify the mount path for the secrets using the spec->containers->volumeMounts property.
- In addition to being mounted as files, secrets can also be passed as environment variables using the spec->containers->env object. This is useful for accessing secrets within applications.
- Secrets can also be used to encode key/certificate pairs for HTTPS certificates. They can be created using the --key and --cert flags, and then referenced in the spec->tls->hosts->secretName properties of an Ingress to enable HTTPS termination.
- By utilizing secrets, Kubernetes allows for secure storage and management of sensitive information such as passwords, connection strings, and certificates.

### Liveness and readiness checks
- Kubernetes monitors the health and resource consumption of containers to ensure they are functioning properly. Containers need to inform Kubernetes about their health status, which can be done in two ways: by declaring a console command that returns the health status or by specifying an endpoint that provides the same information.
- The health status declaration is done in the spec->containers->livenessProbe object. For console command checks, a command is specified, and if it returns a 0 status, the container is considered healthy. The period between checks is defined by periodSeconds, and initialDelaySeconds allows for an initial delay before the first check.
- Endpoint checks are similar, where an HTTP endpoint is specified, and the test is successful if the response contains the declared header with the expected value. Alternatively, a TCP socket check can be used by specifying the port.
- Similar to liveness checks, containers can also have readiness checks to determine if they are ready to receive traffic. The readinessProbe is defined in the same way as the livenessProbe, but it is used to monitor the readiness of containers after they are deployed.
- By implementing liveness and readiness checks, Kubernetes can take appropriate actions such as throttling, restarting containers, or restarting the entire pod instance on a different node when health or resource consumption conditions are violated.

### Autoscaling
- Autoscaling in Kubernetes allows for automatic adjustment of the number of replicas in a Deployment based on the resource consumption. By defining a HorizontalPodAutoscaler object, Kubernetes can dynamically scale the number of replicas to maintain a target resource consumption.
- The HorizontalPodAutoscaler is defined with the target Deployment specified in the scaleTargetRef->name field. The minimum and maximum number of replicas can also be set using the minReplicas and maxReplicas fields.
- The autoscaling behavior is determined by setting the target resource and its utilization percentage. In the example, the target resource is CPU, and the target average utilization is set to 25%.
- When the average resource consumption of each replica exceeds the target utilization, Kubernetes creates a new replica. Conversely, if the average resource consumption falls below the target, a replica is destroyed.
- Autoscaling can help maintain optimal resource utilization and handle fluctuations in load without manual intervention.
