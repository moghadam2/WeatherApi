# Answers to technical questions

## 1. How much time did you spend on this task?
I spent two days analyzing and implementing the solution.

## 2. If you had more time, what improvements or additions would you make?
I would implement support for fetching data from multiple weather services, so we could easily switch providers (e.g., from OpenWeather to another) if needed.

## 3. What is the most useful feature recently added to your favorite programming language?
Honestly, it's hard to say which is the most useful, but one interesting feature for me is **collection expressions** in C#.  
With this feature, we can easily merge or construct collections that implement `ICollection`.

**New Syntax (C# 12)**:
```csharp
using System.Collections.Generic;

List<int> evens = [2, 4, 6];
List<int> odds = [1, 3, 5];
List<int> all = [..evens, ..odds, 7, 9];
```

**Old Syntax**:
```csharp
var evens = new List<int> { 2, 4, 6 };
var odds = new List<int> { 1, 3, 5 };
var all = new List<int>();
all.AddRange(evens);
all.AddRange(odds);
all.Add(7);
all.Add(9);
```

## 4. How do you identify and diagnose a performance issue in a production environment?
Usually, it starts with unexpected results or errors in tests. If it's not caught there, it should be discovered and fixed during product testing.  
Yes, I’ve encountered cases where specific conditions caused issues in production that were not apparent during development.

## 5. What is the last technical book or conference you attended?
The last technical article I read was about the advancement of IoT in industries. It was presented at the 19th National Conference on Computer Science and Information Technology.  
It explained how real-time data exchange between smart devices in factories can increase efficiency and reduce production costs.

## 6. What do you think of this technical test?
It was a good test, although it seems to require a high evaluation effort from your team.

## 7. Describe yourself in JSON format
```json
{
  "fullName": "Mohammad Norouzi",
  "dateOfBirth": "1993-09-03",
  "location": {
    "city": "Tehran",
    "country": "Iran"
  },
  "jobTitle": "Software Developer",
  "skills": [
    "C#",
    ".NET Core",
    "Metric",
    "Docker",
    "SQL"
  ],
  "languages": {
    "English": "Intermediate"
  },
  "education": {
    "degree": "Bachelor's",
    "field": "Software Engineering"
  },
  "workExperience": [
    {
      "company": "Arzval",
      "position": "Software Developer",
      "duration": "6 months"
    },
    {
      "company": "Afareenesh",
      "position": "Software Developer",
      "duration": "3 years"
    }
  ],
  "contact": {
    "email": "12345Noruzi@Gmail.com"
  },
  "summary": "A passionate software developer with experience in C#, .NET Core, and Docker, interested in building scalable software solutions."
}
```