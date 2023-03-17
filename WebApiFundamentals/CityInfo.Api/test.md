# trying mermaid

A quick demo of using Mermaid t oembed diagrams

## flowchart

```mermaid
graph TB
	A[Start] -.-> B(Process 1)
	A --> C[[Process 2]]
	B ==o D([Stop])
	C --> D
```

## class diagram

```mermaid
classDiagram
	class Person{
		Id : Guid
		FirstName: string
		LastName: string
	}
```

```mermaid
gantt
    title A Gantt Diagram
    dateFormat  YYYY-MM-DD
    section Section
    A task           :a1, 2014-01-01, 30d
    Another task     :after a1  , 20d
    section Another
    Task in sec      :2014-01-12  , 12d
    another task      : 24d
```

```mermaid
pie title Pets adopted by volunteers
    "Dogs" : 386
    "Cats" : 85
    "Rats" : 15
```

```mermaid
classDiagram
    class ILogger {
        <<interface>>
        LogDebug(message: string)
        LogInformation(message: string)
        LogWarning(message: string)
        LogError(e: Exception,  message: string)
    }
```