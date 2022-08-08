## ATOS Refactoring Challenge	
- The ClassLibrary "LegacyApp" is used in various Projects to create new SportsPersons
- You are asked to refactor the "SportsPersonService" Class and apply Clean Code principles to it (eg. SOLID, KISS, DRY, YAGNI)
- Assume the code is sound in terms of business logic
- Write a unit test for the happy path
- There will be new suggested and favourite sports added in the future. 

### Limitations
- The Class "MeasurementLoader" retrieves Measurements for a Person based on the name. Usally it is an external Webservice and should not be used in Testing or during CI/CD.
- The Class "PersonRepository" stores SportsPerson in a Database. Usally it writes to an external database and should not be used in Testing or during CI/CD.
- The "Program" class should not be changed (including using statements).
- "PersonRepository" must remain static and the "SavePerson" method must not be changed.
- You can change everything else in the "LegacyApp"
