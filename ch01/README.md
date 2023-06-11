# 1 Understanding the Importance of Software Architecture

## What is software architecture?
A software architect's main goal is to ensure a well-implemented software application through good design. The software development life cycle encompasses activities like defining customer requirements, designing a solution, implementing it, validating with the customer, and delivering in a working environment. Regardless of the development model used, performing these tasks is crucial for delivering acceptable software. Designing great solutions involves meeting user requirements, delivering on time and within budget, ensuring good quality, and enabling future evolution. Sustainable software relies on modern tools and environments to meet user needs effectively.
## Software development process models
Dictate how software is produced and delivered by teams. Over time, various models have been proposed for software development. It's important to review both traditional and agile models, which are widely used today.
### Models
1. Waterfall
- The waterfall model follows a sequential approach, consisting of requirements gathering, design, implementation, verification, and maintenance. However, the use of waterfall models can lead to issues such as delayed delivery and user dissatisfaction. Additionally, waiting until after development to start testing can be stressful.
2. Incremental
- Incremental development is an alternative approach to the waterfall model that addresses its main drawback: the user can only test the solution at the end of the project. In incremental development, users have early interactions with the solution to provide valuable feedback throughout the software development process. The incremental model involves running a set of practices for each increment, including communication, planning, modeling, construction, and deployment.
3. Agile
- Agile models emphasize fast delivery of software and increased interaction with customers. However, it's important to note that agile processes require discipline and flexibility. The Agile Manifesto's 12 principles highlight the importance of delivering valuable software, embracing changing requirements, using short timescales, forming collaborative teams, prioritizing face-to-face communication, and continuously improving. 
4. Lean
- Lean software development, inspired by Toyota's manufacturing method, embraces seven principles: eliminating waste, building quality from the start, creating knowledge, deferring commitments, delivering quickly, respecting the team, and optimizing the value cycle. Following these principles improves feature quality, reduces wasted effort, and focuses on delivering software that matters to the customer.
5. Extreme Programming
- Extreme Programming (XP) emerged as a social change in programming, emphasizing simplicity, communication, feedback, respect, and courage. It promotes face-to-face communication, early feedback, team expertise, and truthfulness about progress. XP introduces rules for planning, managing, designing, coding, and testing. Some widely adopted practices include user stories, iterative development, sustainable velocity, simplicity, refactoring, involving the customer, continuous integration, test-driven development, coding standards, pair programming, and acceptance tests. Many of these practices are now integral to methodologies like DevOps and Scrum.
6. Scrum
- Scrum is an agile model used for managing software development projects. It is based on Lean principles and is widely adopted in the industry. The Scrum framework involves having a flexible backlog of user requirements, which are discussed and addressed in iterative cycles called Sprints. The Scrum Team, consisting of the Product Owner, Scrum Master, and Development Team, collaborates to determine the Sprint goal and deliver the required features. Scrum is often combined with Kanban, a visual system for tracking progress. It is important to note that Scrum focuses on project management and does not specify software implementation details. Combining Scrum with a software development process model like DevOps can be beneficial.
## Gathering the right information to design high-quality software
- Two crucial tasks in software development are defining the development process and gathering software requirements. These tasks play a vital role in ensuring project success. Defining a suitable process, which may occur during project planning, sets the foundation for efficient development. Gathering requirements, a continuous and challenging process, helps shape the software architecture by addressing user needs. These tasks are considered essential for achieving success in software development projects, and as a software architect, your role is to facilitate them to minimize potential issues and guide your team effectively.
### Understanding the requirements gathering process
- Requirements can be represented in various ways, but the traditional approach involves writing a detailed specification before analysis. However, agile methods propose using user stories at the start of development cycles. Regardless of the approach chosen, requirements engineering involves specific steps to gather requirements. It is important to ensure the feasibility of the solution during this process, which may include a separate feasibility analysis or be integrated into project planning. The requirements engineering process provides valuable information that informs software architecture decisions.
### Detecting exact user needs
- Common techniques of elicitating user requirements.
1. The power of imagination
2. Questionnaires
3. Interviews
4. Observation
### Analyzing requirements
- Once you have user needs, analyzing the requirement techniques.
1. Prototyping: Prototypes are fantastic to clarify and materialize the system requirements. (Pencil Project, Figma)
2. Use cases: The Unified Modeling Language (UML) use case model is an option if you need
detailed documentation. (LucidCharts)
### Writing the specifications
- After completing the analysis phase, it is crucial to document the findings in a requirements specification. This document serves as a technical contract between the user and the development team. To ensure clarity and understanding, the specification should follow certain guidelines: stakeholders of all technical backgrounds should comprehend it, classification of each requirement is necessary, and the use of simple future tense is recommended. Ambiguity and controversy must be avoided. Additional information, such as an introductory chapter, glossary, user description, and functional/non-functional requirements, can provide context for the project. When using user stories, it is beneficial to write short sentences that outline each user's needs, goals, and reasons for the requested features. These stories aid in prioritization and can guide the creation of automated acceptance tests.
## Using design techniques as a helpful tool

## Common cases where the requirements gathering process impacts system results

